using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
namespace Web.Controllers
{
    public class ReporteIncidenciasController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

        // GET: ReporteIncidencias
        public ActionResult Index()
        {
            try
            {
                IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
                IEnumerable<ReporteIncidencias> lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                ViewBag.title = "Reporte de Incidencias";
                ViewBag.listaIncidencias = lista;
                ViewBag.IDEstadosDeIncidencia = ListaEstadosIncidencia();
                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList ListaEstadosIncidencia(int idEstado = 0)
        {
            IServiceEstadoIncidencia _ServiceEstadoIncidencia  = new ServiceEstadoIncidencia();
            IEnumerable<EstadoIncidencia> lista = _ServiceEstadoIncidencia.GetEstadoIncidencia();
            return new SelectList(lista, "IDEstado", "TipoEstado", idEstado);
        }

        public ActionResult BuscarHistorial(int? id)
        {
            try
            {
                IEnumerable<ReporteIncidencias> lista = null;
                IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
                lista = _ServiceReporteIncidencias.GetHistorial(id);
                return PartialView("_PartialViewListaIncidencias", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ListaResidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }




        /*****************************************************************************************************************************************/

        public ActionResult Guardar(ReporteIncidencias reporteIncidencias)
        {
            Usuarios usuario = (Usuarios)Session["User"];
            reporteIncidencias.IDUsuario = usuario.ID;
            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();

            try
            {
                IEnumerable<ReporteIncidencias> lista = null;
                ModelState.Remove("IDIncidencia");
                ModelState.Remove("descripcion");
                if (ModelState.IsValid)
                {
                    ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                    lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Reporte Creado",
                               "La incidencia se atenderá a la brevedad", Utils.SweetAlertMessageType.success
                               );
                    return PartialView("_PartialViewListaIncidencias", lista);

                }
                else
                {
                    lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                    return PartialView("_PartialViewListaIncidencias", lista);
                }



            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ReporteIncidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult Actualizar(int id)
        {
            try
            {
                IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
                ReporteIncidencias reporteIncidencias = _ServiceReporteIncidencias.GetReporteIncidenciasByID(id);
                if (reporteIncidencias.IDEstado == 1)
                {
                    reporteIncidencias.IDEstado = 2;
                }
                else
                {
                    if (reporteIncidencias.IDEstado == 2)
                    {
                        reporteIncidencias.IDEstado = 3;
                    }
                }
                IEnumerable<ReporteIncidencias> lista = null;


                if (ModelState.IsValid)
                {
                    ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                    lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                    return PartialView("_PartialViewListaIncidencias", lista);
                }
                else
                {

                    lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                    return PartialView("_PartialViewListaIncidencias", lista);
                }

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ReporteIncidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }



        public ActionResult _PartialViewListaIncidencias()
        {
            return PartialView("_PartialViewListaIncidencias");
        }

        public ActionResult AjaxCrearIncidencia()
        {
            return PartialView("_PartialViewCrearIncidencia");
        }
    }
}