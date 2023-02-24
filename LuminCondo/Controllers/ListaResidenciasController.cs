using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ListaResidenciasController : Controller
    {
        // GET: ListaResidencias
        public ActionResult Index()
        {
            IEnumerable<GestionResidencias> lista = null;
            try
            {
                IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
                lista = _ServiceGestionResidencias.GetGestionResidencias();
                ViewBag.title = "Listado de Residencias";

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

        // GET: ListaResidencias/Details/5
        public ActionResult Details(int? id)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            GestionResidencias gestionResidencias = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                gestionResidencias = _ServiceGestionResidencias.GetGestionResidenciasByID(Convert.ToInt32(id));
                if (gestionResidencias == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "GestionPlanCobros";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(gestionResidencias);
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

        // GET: ListaResidencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaResidencias/Create
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
        }

        // GET: ListaResidencias/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaResidencias/Edit/5
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

        // GET: ListaResidencias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaResidencias/Delete/5
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
