using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryGestionResidencias : IRepositoryGestionResidencias
    {
        public IEnumerable<GestionResidencias> GetGestionResidencias()
        {
            try
            {
                IEnumerable<GestionResidencias> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionResidencias.
                                                    Include("Usuarios").
                                                    Include("EstadoResidencia").
                                                    ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public GestionResidencias GetGestionResidenciasByID(int id)
        {
            try
            {
                GestionResidencias oGestionResidencia = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionResidencia = ctx.GestionResidencias.
                                                            Where(l => l.IDResidencia == id).
                                                            Include("Usuarios").
                                                            Include("EstadoResidencia").
                                                            Include("GestionAsignacionPlanes").
                                                            FirstOrDefault();
                }
                return oGestionResidencia;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public GestionResidencias Guardar(GestionResidencias gestionResidencias)
        {
            int retorno = 0;
            GestionResidencias oGestionResidencias = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oGestionResidencias = GetGestionResidenciasByID((int)gestionResidencias.IDResidencia);

                if (oGestionResidencias == null)
                {

                    ctx.GestionResidencias.Add(gestionResidencias);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.GestionResidencias.Add(gestionResidencias);
                    ctx.Entry(gestionResidencias).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oGestionResidencias = GetGestionResidenciasByID((int)gestionResidencias.IDResidencia);

            return oGestionResidencias;
        }
    }
}
