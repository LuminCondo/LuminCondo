using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;
namespace LuminCondo.Controllers
{
    public class GestionPlanCobrosController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
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

                // Direccional a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexAdmin()
        {
            IEnumerable<GestionPlanCobros> lista = null;
            try
            {
                IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
                lista = _ServiceGestionPlanCobros.GetGestionPlanCobros();
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: GestionPlanCobros/Details/5
        
        private MultiSelectList listaRubrosCobros(ICollection<GestionRubrosCobros> gestionRubrosCobros = null)
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
        /*****************************************************************************************************************************************/

        public ActionResult Guardar(GestionPlanCobros gestionPlanCobros, string[] selectedRubrosCobros)
        {
            IEnumerable<GestionPlanCobros> lista = null;
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();

            try
            {
                if (ModelState.IsValid)
                {
                    GestionPlanCobros oGestionPlanCobros = _ServiceGestionPlanCobros.Guardar(gestionPlanCobros, selectedRubrosCobros);
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Plan Guardado",
                               "El plan se ha guardado correctamente", SweetAlertMessageType.success
                               );
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Plan no Guardado",
                               "El plan se no se ha guardado correctamente, por favor volverlo a intentar", SweetAlertMessageType.error
                               );

                }
                lista = _ServiceGestionPlanCobros.GetGestionPlanCobros();
                return PartialView("_PartialViewListaPlanes", lista);
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

        public ActionResult AjaxCrearPlan()
        {
            ViewBag.IDRubrosCobros = listaRubrosCobros();
            return PartialView("_PartialViewCrearPlan");
        }

        public ActionResult AjaxModificarPlan(int? id)
        {
            ServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros gestionPlanCobros = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
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
                return PartialView("_PartialViewModificarPlan", gestionPlanCobros);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionPlanCobros";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult _PartialViewListaPlanes()
        {
            return PartialView("_PartialViewListaPlanes");
        }

    }
}
