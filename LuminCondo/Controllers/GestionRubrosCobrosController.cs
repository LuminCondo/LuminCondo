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
using Web.Security;

namespace Web.Controllers
{
    public class GestionRubrosCobrosController : Controller
    {
        // GET: GestionRubrosCobros
        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: GestionRubrosCobros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador)]
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
        /*****************************************************************************************************************************************/

        public ActionResult Guardar(GestionRubrosCobros gestionRubrosCobros)
        {
            IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();

            try
            {
                if (ModelState.IsValid)
                {
                    GestionRubrosCobros oGestionRubrosCobros = _ServiceGestionRubrosCobros.Guardar(gestionRubrosCobros);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
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
                TempData["Redirect"] = "GestionRubrosCobros";
                TempData["Redirect-Action"] = "Create";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        /*****************************************************************************************************************************************/
        [CustomAuthorize((int)Roles.Administrador)]
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
                    TempData["Redirect"] = "GestionRubrosCobros";
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
    }
}