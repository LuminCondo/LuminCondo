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
using Web.Security;

namespace Web.Controllers
{
    public class ListaResidenciasController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

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

        [CustomAuthorize((int)Roles.Administrador)]
        // GET: ListaResidencias/Create
        public ActionResult Create()
        {
            return View();
        }
        // GET: ListaResidencias/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int id)
        {
            return View();
        }
    }
}
