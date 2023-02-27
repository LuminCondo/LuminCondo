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
    public class RepositoryReporteIncidencias : IRepositoryReporteIncidencias
    {
        public void BorrarReporteIncidencias(ReporteIncidencias reporteIncidencias)
        {
            int retorno = 0;
            ReporteIncidencias oReporteIncidencias = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oReporteIncidencias = GetReporteIncidenciasByID((int)reporteIncidencias.IDIncidencia);
                IRepositoryReporteIncidencias _RepositoryReporteIncidencias = new RepositoryReporteIncidencias();

                if (oReporteIncidencias == null)
                {
                    ctx.ReporteIncidencias.Remove(reporteIncidencias);

                    retorno = ctx.SaveChanges();
                }
            }
        }

        public IEnumerable<ReporteIncidencias> GetReporteIncidencias()
        {
            try
            {
                IEnumerable<ReporteIncidencias> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.ReporteIncidencias.
                                                    Include("EstadoIncidencia").
                                                    Include("Usuarios").
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

        public ReporteIncidencias GetReporteIncidenciasByID(int id)
        {
            try
            {
                ReporteIncidencias oReporteIncidencias = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oReporteIncidencias = ctx.ReporteIncidencias.
                                                            Where(l => l.IDIncidencia == id).
                                                            Include("Usuarios").
                                                            Include("EstadoIncidencia").
                                                            FirstOrDefault();
                }
                return oReporteIncidencias;
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

        public ReporteIncidencias Guardar(ReporteIncidencias reporteIncidencias)
        {
            int retorno = 0;
            ReporteIncidencias oReporteIncidencias = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oReporteIncidencias = GetReporteIncidenciasByID((int)reporteIncidencias.IDIncidencia);
                IRepositoryInformacion _RepositoryInformacion = new RepositoryInformacion();

                if (oReporteIncidencias == null)
                {
                    ctx.ReporteIncidencias.Add(reporteIncidencias);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.ReporteIncidencias.Add(reporteIncidencias);

                    ctx.Entry(reporteIncidencias).State = EntityState.Modified;

                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oReporteIncidencias = GetReporteIncidenciasByID((int)reporteIncidencias.IDIncidencia);

            return oReporteIncidencias;
        }
    }
}