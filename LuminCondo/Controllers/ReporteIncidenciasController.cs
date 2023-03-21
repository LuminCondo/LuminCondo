using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ReporteIncidenciasController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

        // GET: ReporteIncidencias
        public ActionResult Index()
        {
            IEnumerable<ReporteIncidencias> lista = null;
            try
            {
                IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
                lista = _ServiceReporteIncidencias.GetReporteIncidencias();
                ViewBag.title = "Reporte de Incidencias";

                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }




        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: ReporteIncidencias/Create
        public ActionResult Create()
        {
            ViewBag.IDReporteIncidencias = listaReporteIncidencias();
            return View();
        }

        private SelectList listaReporteIncidencias(int idRepInc = 0)
        {
            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
            IEnumerable<ReporteIncidencias> lista = _ServiceReporteIncidencias.GetReporteIncidencias();
            return new SelectList(lista);
        }
        /*****************************************************************************************************************************************/

        public ActionResult Guardar(ReporteIncidencias reporteIncidencias)
        {
                if (Session["User"] != null)
                {
                    Infraestructure.Models.Usuarios usuario = new Infraestructure.Models.Usuarios();
                    usuario = (Infraestructure.Models.Usuarios)Session["User"];
                    reporteIncidencias.IDUsuario = usuario.ID;
            }
            else
            {
                reporteIncidencias.IDUsuario = 1;
            }
            reporteIncidencias.IDEstado = 1;

            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();

            try
            {
                ModelState.Remove("IDIncidencia");
                ModelState.Remove("descripcion");
                if (ModelState.IsValid)
                {
                    ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                }
                else
                {
                    ViewBag.IDReporteIncidencias = listaReporteIncidencias(reporteIncidencias.IDIncidencia);

                    return View("Create", reporteIncidencias);
                }
                return RedirectToAction("Index");
                
              
                
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

        public ActionResult Actualizar(int idIncidencia)
        {
            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
            ReporteIncidencias reporteIncidencias = _ServiceReporteIncidencias.GetReporteIncidenciasByID(idIncidencia);
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
            
            try
            {
                if (ModelState.IsValid)
                {
                    ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                }
                else
                {
                    ViewBag.IDReporteIncidencias = listaReporteIncidencias(reporteIncidencias.IDIncidencia);

                    return View("Create", reporteIncidencias);
                }

                return RedirectToAction("Index");
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

        /*****************************************************************************************************************************************/

        // GET: ReporteIncidencias/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            ServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
            ReporteIncidencias reporteIncidencias;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                reporteIncidencias = _ServiceReporteIncidencias.GetReporteIncidenciasByID(Convert.ToInt32(id));

                if (reporteIncidencias == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDReporteIncidencias = listaReporteIncidencias(reporteIncidencias.IDIncidencia);
                return View(reporteIncidencias);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}