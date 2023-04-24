using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
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

        public IEnumerable<GestionAsignacionPlanes> GetHistorial(int? mes, int? anno, int? idResidencia, bool estado)
        {
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (idResidencia == null&& mes == null && anno == null) //Buscar sin filtro, solo las pagadas
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.estadoPago == estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia==null&& mes != null && anno != null) //Buscar por mes y año sin residencia en especifico
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.fechaAsignacion.Month == mes && l.fechaAsignacion.Year == anno && l.estadoPago== estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia == null && mes == null && anno != null) //Buscar por año sin residencia en especifico
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.fechaAsignacion.Year == anno && l.estadoPago == estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia != null && mes == null && anno == null  || idResidencia != null && mes != null && anno == null) //Buscar por Residencia, sin mes o sin año
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.IDResidencia == idResidencia && l.estadoPago == estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia != null && mes == null && anno != null) //Buscar por Residencia, sin mes y con año
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.IDResidencia == idResidencia && l.estadoPago == estado && l.fechaAsignacion.Year==anno).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia != null && mes != null && anno != null) //Buscar con Mes, año y Residencia
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                             Where(l => l.IDResidencia == idResidencia && l.fechaAsignacion.Month == mes && l.fechaAsignacion.Year == anno && l.estadoPago == estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
                    }
                    if (idResidencia == null && mes != null && anno == null) //Buscar por año sin residencia en especifico
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.fechaAsignacion.Month == mes && l.estadoPago == estado).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").Include("GestionPlanCobros.GestionRubrosCobros").ToList();
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

        public IEnumerable<GestionAsignacionPlanes> GetHistorialGeneral(int? mes, int? anno, int? idResidencia)
        {
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    if (idResidencia == null && mes == null && anno == null) //Buscar sin filtro, solo las pagadas
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                    }
                    if (idResidencia == null && mes != null && anno != null) //Buscar por mes y año sin residencia en especifico
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.fechaAsignacion.Month == mes && l.fechaAsignacion.Year == anno ).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                    }
                    if (idResidencia == null && mes == null && anno != null) //Buscar por año sin residencia en especifico
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.fechaAsignacion.Year == anno ).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                    }
                    if (idResidencia != null && mes == null && anno == null || idResidencia != null && mes != null && anno == null) //Buscar por Residencia, sin mes o sin año
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.IDResidencia == idResidencia ).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                    }
                    if (idResidencia != null && mes == null && anno != null) //Buscar por Residencia, sin mes y con año
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                            Where(l => l.IDResidencia == idResidencia && l.fechaAsignacion.Year == anno).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                    }
                    if (idResidencia != null && mes != null && anno != null) //Buscar con Mes, año y Residencia
                    {
                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                             Where(l => l.IDResidencia == idResidencia && l.fechaAsignacion.Month == mes && l.fechaAsignacion.Year == anno).
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
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

        public GestionAsignacionPlanes Guardar(GestionAsignacionPlanes gestionAsignacionPlanes)
        {
            int retorno = 0;
            int flag = 0;
            GestionAsignacionPlanes oGestionAsignacionPlanes = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                
                
                
                    oGestionAsignacionPlanes = GetGestionAsignacionPlanesByID((int)gestionAsignacionPlanes.IDAsignacion);
                    if (oGestionAsignacionPlanes == null)
                    {

                        lista = ctx.GestionAsignacionPlanes.Include("GestionResidencias").
                        Include("GestionResidencias.Usuarios").Include("GestionPlanCobros").ToList();
                        foreach (var item in lista)
                        {
                            if (item.fechaAsignacion.Month == gestionAsignacionPlanes.fechaAsignacion.Month && item.fechaAsignacion.Year == gestionAsignacionPlanes.fechaAsignacion.Year && item.IDResidencia == gestionAsignacionPlanes.IDResidencia)
                            {
                                flag++;
                            }
                        }
                    if (flag == 0)
                    {
                        ctx.GestionAsignacionPlanes.Add(gestionAsignacionPlanes);

                        retorno = ctx.SaveChanges();
                    }
                    }
                    else
                    {

                        ctx.Entry(gestionAsignacionPlanes).State = EntityState.Modified;

                        retorno = ctx.SaveChanges();
                    }
            }

            if (retorno >= 0)
                oGestionAsignacionPlanes = GetGestionAsignacionPlanesByID((int)gestionAsignacionPlanes.IDAsignacion);

            return oGestionAsignacionPlanes;
        }
        public void GetGrafico(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    var resultado = ctx.GestionAsignacionPlanes
                        .Where(x => x.estadoPago)
                        .GroupBy(x => x.fechaAsignacion.Month)
                        .Select(o => new
                        {
                            Total = o.Sum(x => x.IDPlan),
                            Month = o.Key
                        });


                    foreach (var item in resultado)
                    {
                        varEtiquetas += new DateTime(2023, item.Month, 1) // create a DateTime object with the given month number
                            .ToString("MMMM", new CultureInfo("es-ES")) + ","; // format the date as the full month name in Spanish and append to varEtiquetas
                        varValores += item.Total + ",";

                    }


                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
            }
        }
    }
}