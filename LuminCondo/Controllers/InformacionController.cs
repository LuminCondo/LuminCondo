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
    public class InformacionController : Controller
    {
        // GET: Informacion
        public ActionResult Index()
        {
            IEnumerable<Informacion> lista = null;
            try
            {
                IServiceInformacion _ServiceInformacion = new ServiceInformacion();
                lista = _ServiceInformacion.GetInformacion();
                ViewBag.title = "Listado de Informaciones";

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

        // GET: Informacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Informacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Informacion/Create
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

        // GET: Informacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Informacion/Edit/5
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

        // GET: Informacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Informacion/Delete/5
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