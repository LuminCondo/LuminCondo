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
    public class RepositoryGestionAsignacionPlanes : IRepositoryGestionAsignacionPlanes
    {
        IEnumerable<GestionAsignacionPlanes> lista = null;

        public IEnumerable<GestionAsignacionPlanes> GetEstadodeCuentaByIDResidencia(int id)
        {
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.GestionAsignacionPlanes.
                        Where(l => l.IDResidencia == id).
                        Include("GestionResidencias").
                        Include("GestionResidencias.Usuarios").
                        Include("GestionPlanCobros").
                        Include("GestionPlanCobros.GestionRubrosCobros").
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

        public IEnumerable<GestionAsignacionPlanes> GetGestionAsignacionPlanes()
        {
            try
            {
                
                using(MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled= false;

                    lista=ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
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

        public GestionAsignacionPlanes GetGestionAsignacionPlanesByID(int id)
        {
            GestionAsignacionPlanes oGestionAsignacionPlane = null;
            try
            {
                
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oGestionAsignacionPlane = ctx.GestionAsignacionPlanes.Find(id);
                }
                return oGestionAsignacionPlane;
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

        public GestionAsignacionPlanes Guardar(GestionAsignacionPlanes gestionAsignacionPlanes)
        {
            int retorno = 0;
            int flag = 0;
            GestionAsignacionPlanes oGestionAsignacionPlanes = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                
                lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                foreach (var item in lista)
                {
                    if (item.fechaAsignacion.Month== gestionAsignacionPlanes.fechaAsignacion.Month&& item.fechaAsignacion.Year == gestionAsignacionPlanes.fechaAsignacion.Year&&item.IDResidencia== gestionAsignacionPlanes.IDResidencia)
                    {
                        flag++;
                    }
                }
                if (flag==0)
                {
                    oGestionAsignacionPlanes = GetGestionAsignacionPlanesByID((int)gestionAsignacionPlanes.IDAsignacion);
                    if (oGestionAsignacionPlanes == null)
                    {
                        ctx.GestionAsignacionPlanes.Add(gestionAsignacionPlanes);

                        retorno = ctx.SaveChanges();
                    }
                    else
                    {
                        ctx.GestionAsignacionPlanes.Add(gestionAsignacionPlanes);

                        ctx.Entry(gestionAsignacionPlanes).State = EntityState.Modified;

                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oGestionAsignacionPlanes = GetGestionAsignacionPlanesByID((int)gestionAsignacionPlanes.IDAsignacion);

            return oGestionAsignacionPlanes;
        }
    }
}
