using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryReporteIncidencias : IRepositoryReporteIncidencias
    {
        IEnumerable<ReporteIncidencias> lista = null;
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

        public IEnumerable<ReporteIncidencias> GetHistorial(int? id)
        {
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (id == null) //Buscar sin filtro
                    {
                        lista = ctx.ReporteIncidencias.
                            Include("EstadoIncidencia").
                                                    Include("Usuarios").
                            ToList();
                    }
                    if (id != null) //Buscar por Un único tipo de estado
                    {
                        lista = ctx.ReporteIncidencias.
                            Include("EstadoIncidencia").
                            Include("Usuarios").
                            Where(l => l.IDEstado == id).
                            ToList();
                    }
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

        public IEnumerable<ReporteIncidencias> GetReporteIncidencias()
        {
            try
            {
                
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

                if (oReporteIncidencias == null)
                {
                    reporteIncidencias.IDEstado = 1;
                    ctx.ReporteIncidencias.Add(reporteIncidencias);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    var objetoExistente = ctx.ReporteIncidencias.FirstOrDefault(o => o.IDIncidencia == reporteIncidencias.IDIncidencia);
                    objetoExistente.IDEstado = reporteIncidencias.IDEstado;

                    retorno = ctx.SaveChanges();

                }
            }

            if (retorno >= 0)
                oReporteIncidencias = GetReporteIncidenciasByID((int)reporteIncidencias.IDIncidencia);

            return oReporteIncidencias;
        }
    }
}