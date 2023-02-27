using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
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
    public class GestionRubrosCobrosController : Controller
    {
        // GET: GestionRubrosCobros
        public ActionResult Index()
        {
            IEnumerable<GestionRubrosCobros> lista = null;
            try
            {
                IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
                lista = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();
                ViewBag.title = "Lista de Rubros";

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

        // GET: GestionRubrosCobros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GestionRubrosCobros/Create
        public ActionResult Create()
        {
            ViewBag.IDRubroCobro = listaRubrosCobros();
            return View();
        }

        private SelectList listaRubrosCobros(int idRubro = 0)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            IEnumerable<GestionRubrosCobros> lista = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();
            return new SelectList(lista);
        }

        /* POST: GestionRubrosCobros/Create
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

        public ActionResult Guardar(GestionRubrosCobros gestionRubrosCobros)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            MemoryStream stream = new MemoryStream();

            try
            {
                if (ModelState.IsValid)
                {
                    GestionRubrosCobros oGestionRubrosCobros = _ServiceGestionRubrosCobros.Guardar(gestionRubrosCobros);
                }
                else
                {
                    ViewBag.IDRubroCobro = listaRubrosCobros(gestionRubrosCobros.IDRubro);

                    return View("Create", gestionRubrosCobros);
                }

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

        /*****************************************************************************************************************************************/

        // GET: GestionRubrosCobros/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            GestionRubrosCobros gestionRubrosCobros = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                gestionRubrosCobros = _ServiceGestionRubrosCobros.GetGestionRubrosCobrosByID(Convert.ToInt32(id));

                if (gestionRubrosCobros == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDRubroCobro = listaRubrosCobros(gestionRubrosCobros.IDRubro);
                return View(gestionRubrosCobros);

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

        // POST: GestionRubrosCobros/Edit/5
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

        public ActionResult Eliminar(GestionRubrosCobros gestionRubrosCobros)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            MemoryStream stream = new MemoryStream();

            try
            {
               // _ServiceGestionRubrosCobros.BorrarRubroCobros(gestionRubrosCobros.IDRubro);

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

        // GET: GestionRubrosCobros/Delete/5
        public ActionResult Delete(int? id)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            GestionRubrosCobros gestionRubrosCobros = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                gestionRubrosCobros = _ServiceGestionRubrosCobros.GetGestionRubrosCobrosByID(Convert.ToInt32(id));
                if (gestionRubrosCobros == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(gestionRubrosCobros);
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

        // POST: GestionRubrosCobros/Delete/5
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