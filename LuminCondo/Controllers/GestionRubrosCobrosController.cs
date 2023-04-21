using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class GestionRubrosCobrosController : Controller
    {
        // GET: GestionRubrosCobros
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();
                IEnumerable<GestionRubrosCobros>  lista = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();

                return View(lista);
            }
            catch (Exception ex)
            {
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
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
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Rubro Guardado",
                               "El rubro se ha guardado correctamente", Utils.SweetAlertMessageType.success
                               );
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Rubro no Guardado",
                               "El rubro se no se ha guardado correctamente, por favor volverlo a intentar", SweetAlertMessageType.error
                    );

                }
                IEnumerable<GestionRubrosCobros> lista = _ServiceGestionRubrosCobros.GetGestionRubrosCobros();
                return PartialView("_PartialViewListaRubros", lista);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionRubrosCobros";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

       

        public ActionResult AjaxCrearRubro()
        {
            return PartialView("_PartialViewCrearRubro");
        }

        public ActionResult AjaxModificarRubro(int? id)
        {
            ServiceGestionRubrosCobros _ServiceGestionRubrosCobros = new ServiceGestionRubrosCobros();

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                GestionRubrosCobros gestionRubrosCobros = _ServiceGestionRubrosCobros.GetGestionRubrosCobrosByID(Convert.ToInt32(id));

                if (gestionRubrosCobros == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "GestionRubrosCobros";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                return PartialView("_PartialViewModificarRubro", gestionRubrosCobros);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Web.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionRubrosCobros";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult _PartialViewListaRubros()
        {
            return PartialView("_PartialViewListaRubros");
        }
    }
}