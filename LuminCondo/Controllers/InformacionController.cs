using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
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

namespace Web.Controllers
{
    public class InformacionController : Controller
    {
        // GET: Informacion
        [CustomAuthorize((int)Roles.Administrador)]
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
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaTipoInformacion(int idTipoInfo = 0)
        {
            IServiceTipoInformacion _ServiceTipoInformacion = new ServiceTipoInformacion();
            IEnumerable<TipoInformacion> lista = _ServiceTipoInformacion.GetTipoInformacion();
            return new SelectList(lista,"IDTipoInfo","tipoInfo", idTipoInfo);
        }
        /*****************************************************************************************************************************************/

        public ActionResult Guardar(Informacion informacion)
        {
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            informacion.fechaPublicacion = DateTime.Now;
            try
            {
                IEnumerable<Informacion> lista = null;
                ModelState.Remove("fechapublicacion");
                ModelState.Remove("IDInformacion");
                if (ModelState.IsValid)
                {
                    Informacion oInformacion = _ServiceInformacion.Guardar(informacion);
                    lista = _ServiceInformacion.GetInformacion();
                    lista.Reverse();
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Información Guardada",
                               "La Información " + oInformacion.titulo + " de tipo " + oInformacion.TipoInformacion.tipoInfo + " se ha guardado correctamente", Utils.SweetAlertMessageType.success
                               );
                }
                else
                {
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IDTipoInformacion = listaTipoInformacion(informacion.IDTipoInfo);
                    if (informacion.IDTipoInfo > 0)
                    {
                        lista = _ServiceInformacion.GetInformacion();
                        lista.Reverse();
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al Guardar",
                                   "La Información " + informacion.titulo + " de tipo " + informacion.TipoInformacion.tipoInfo + " se ha guardado correctamente", Utils.SweetAlertMessageType.error
                                   );
                        return PartialView("_PartialViewListaInformacion", lista);
                    }
                    else
                    {
                        lista = _ServiceInformacion.GetInformacion();
                        lista.Reverse();
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al Guardar",
                                   "La Información no se ha guardado correctamente", Utils.SweetAlertMessageType.error
                                   );
                        return PartialView("_PartialViewListaInformacion", lista);
                    };
                }

                return PartialView("_PartialViewListaInformacion", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        /*****************************************************************************************************************************************/

        

        public ActionResult _PartialViewListaInformacion()
        {
            return PartialView("_PartialViewListaInformacion");
        }

        public ActionResult AjaxCrearNoticia()
        {
            ViewBag.IDTipoInformacion = listaTipoInformacion();
            return PartialView("_PartialViewCrearInformacion");
        }

        public ActionResult AjaxModificarNoticia(int? id)
        {
            ServiceInformacion _ServiceInformacion = new ServiceInformacion();
            Informacion informacion = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                informacion = _ServiceInformacion.GetInformacionByID(Convert.ToInt32(id));
                if (informacion == null)
                {
                    TempData["Message"] = "No existe la información solicitada";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IdTipoInformacion = listaTipoInformacion(informacion.IDTipoInfo);
                return PartialView("_PartialViewModificarInformacion", informacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}