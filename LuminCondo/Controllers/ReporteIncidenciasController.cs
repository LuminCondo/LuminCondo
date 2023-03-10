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

namespace Web.Controllers
{
    public class ReporteIncidenciasController : Controller
    {
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

        

        // GET: ReporteIncidencias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

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

        /* POST: ReporteIncidencias/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        /*****************************************************************************************************************************************/

        public ActionResult Guardar(ReporteIncidencias reporteIncidencias)
        {
                if (Session["User"] != null)
                {
                    Infraestructure.Models.Usuarios usuario = new Infraestructure.Models.Usuarios();
                    usuario = (Infraestructure.Models.Usuarios)Session["User"];
                    reporteIncidencias.IDUsuario = usuario.ID;
                }
            

            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();

            try
            {
                ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                /*ModelState.Remove("IDEstado");
                ModelState.Remove("IDUsuario");
                if (ModelState.IsValid)
                {
                    
                }
                else
                {
                    ViewBag.IDReporteIncidencias = listaReporteIncidencias(reporteIncidencias.IDIncidencia);

                    return View("Create", reporteIncidencias);
                }*/
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
                ReporteIncidencias oReporteIncidencias = _ServiceReporteIncidencias.Guardar(reporteIncidencias);
                /*ModelState.Remove("IDEstado");
                ModelState.Remove("IDUsuario");
                if (ModelState.IsValid)
                {
                    
                }
                else
                {
                    ViewBag.IDReporteIncidencias = listaReporteIncidencias(reporteIncidencias.IDIncidencia);

                    return View("Create", reporteIncidencias);
                }*/

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

        // POST: ReporteIncidencias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Eliminar(ReporteIncidencias reporteIncidencias)
        {
            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
            MemoryStream stream = new MemoryStream();

            try
            {
                _ServiceReporteIncidencias.BorrarReporteIncidencias(reporteIncidencias);

                return RedirectToAction("Index");
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

        // GET: ReporteIncidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            IServiceReporteIncidencias _ServiceReporteIncidencias = new ServiceReporteIncidencias();
            ReporteIncidencias reporteIncidencias = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                reporteIncidencias = _ServiceReporteIncidencias.GetReporteIncidenciasByID(Convert.ToInt32(id));
                if (reporteIncidencias == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(reporteIncidencias);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionPlanCobros";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: ReporteIncidencias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}