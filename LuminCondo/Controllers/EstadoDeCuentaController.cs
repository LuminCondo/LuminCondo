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

namespace LuminCondo.Controllers
{
    public class EstadoDeCuentaController : Controller
    {
        // GET: EstadoDeCuenta
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

        // GET: EstadoDeCuenta/Details/5
        public ActionResult Details(int? id)
        {
            IEnumerable<GestionAsignacionPlanes> listaAsignaciones = null;
            
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                listaAsignaciones = _ServiceGestionAsignacionPlanes.GetEstadodeCuentaByIDResidencia(Convert.ToInt32(id)); 
                return View(listaAsignaciones);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "EstadoDeCuenta";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }

        // GET: EstadoDeCuenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoDeCuenta/Create
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

        // GET: EstadoDeCuenta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadoDeCuenta/Edit/5
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

        // GET: EstadoDeCuenta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadoDeCuenta/Delete/5
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
