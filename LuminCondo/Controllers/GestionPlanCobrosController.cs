using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;
namespace LuminCondo.Controllers
{
    public class GestionPlanCobrosController : Controller
    {
        // GET: GestionPlanCobros
        public ActionResult Index()
        {
            IEnumerable<GestionPlanCobros> lista = null;
            try
            {
                IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
                lista = _ServiceGestionPlanCobros.GetGestionPlanCobros();
                ViewBag.title = "Lista de Planes";
                IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
                ViewBag.listaRubros = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();

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

        public ActionResult IndexAdmin()
        {
            IEnumerable<GestionPlanCobros> lista = null;
            try
            {
                IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
                lista = _ServiceGestionPlanCobros.GetGestionPlanCobros();

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

        // GET: GestionPlanCobros/Details/5
        public ActionResult Details(int? id)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros planCobros = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                planCobros=_ServiceGestionPlanCobros.GetGestionPlanCobrosByID(Convert.ToInt32(id));
                if (planCobros == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "GestionPlanCobros";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(planCobros);
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

        // GET: GestionPlanCobros/Create
        public ActionResult Create()
        {
            ViewBag.IDRubrosCobros = listaRubrosCobros();
            return View();
        }

        private MultiSelectList listaRubrosCobros(ICollection<GestionRubrosCobros> gestionRubrosCobros=null)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
            IEnumerable<GestionRubrosCobros> lista = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();
            //Seleccionar Rubros
            int[] listaRubrosCobrosSelect = null;
            if (gestionRubrosCobros != null)
            {
                listaRubrosCobrosSelect = gestionRubrosCobros.Select(c => c.IDRubro).ToArray();
            }
            return new MultiSelectList(lista, "IDRubro", "descripcion", listaRubrosCobrosSelect);
        }

        /* POST: GestionPlanCobros/Create
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

        public ActionResult Guardar(GestionPlanCobros gestionPlanCobros, string[] selectedRubrosCobros)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();

            try
            {
                if (ModelState.IsValid)
                {
                    GestionPlanCobros oGestionPlanCobros = _ServiceGestionPlanCobros.Guardar(gestionPlanCobros, selectedRubrosCobros);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.IDRubrosCobros = listaRubrosCobros(gestionPlanCobros.GestionRubrosCobros);
                    if (gestionPlanCobros.IDPlan > 0)
                    {
                        return (ActionResult)View("Edit", gestionPlanCobros);
                    }
                    else
                    {
                        return View("Create", gestionPlanCobros);
                    }
                }
                return RedirectToAction("IndexAdmin");
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

        // GET: GestionPlanCobros/Edit/5
        public ActionResult Edit(int? id)
        {
            ServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros gestionPlanCobros = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                gestionPlanCobros = _ServiceGestionPlanCobros.GetGestionPlanCobrosByID(Convert.ToInt32(id));
                if (gestionPlanCobros == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "GestionPlanCobros";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDRubrosCobros = listaRubrosCobros(gestionPlanCobros.GestionRubrosCobros);
                return View(gestionPlanCobros);

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

        // POST: GestionPlanCobros/Edit/5
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

        public ActionResult Eliminar(GestionPlanCobros gestionPlanCobros)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            MemoryStream stream = new MemoryStream();

            try
            {
                _ServiceGestionPlanCobros.BorrarPlanCobros(gestionPlanCobros);

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

        // GET: GestionPlanCobros/Delete/5
        public ActionResult Delete(int? id)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros gestionPlanCobros = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                gestionPlanCobros = _ServiceGestionPlanCobros.GetGestionPlanCobrosByID(Convert.ToInt32(id));
                if (gestionPlanCobros == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(gestionPlanCobros);
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

        // POST: GestionPlanCobros/Delete/5
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
