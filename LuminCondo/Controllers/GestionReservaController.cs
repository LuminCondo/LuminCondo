using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class GestionReservaController : Controller
    {
        // GET: GestionReserva
        public ActionResult IndexAdmin()
        {
            IEnumerable<GestionReservas> lista = null;

            try
            {
                IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
                lista = _ServiceGestionReservas.GetReservas();

                ViewBag.title = "Listado de Reservas";

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

        // GET: GestionReserva/Details/5
        public ActionResult Details(int? id)
        {
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            GestionReservas gestionReservas = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                gestionReservas = _ServiceGestionReservas.GetReservaByID((int)id);

                if (gestionReservas == null)
                {
                    {
                        TempData["Message"] = "No existe el Plan solicitado";
                        TempData["Redirect"] = "GestionPlanCobros";
                        TempData["Redirect-Action"] = "Index";
                        // Redireccion a la captura del Error
                        return RedirectToAction("Default", "Error");
                    }
                }
                return View(gestionReservas);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionPlanCobros";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: GestionReserva/Create
        public ActionResult AjaxCrearReserva()
        {
            ViewBag.IDEspacios = listaEspacios();
            return PartialView("_PartialViewCrearReserva");
        }
        
        public ActionResult Guardar(GestionReservas reserva)
        {
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            //reserva.fecha = DateTime.Now;
            try
            {
                IEnumerable<GestionReservas> lista = null;
                //ModelState.Remove("fechapublicacion");
                //ModelState.Remove("IDInformacion");
                if (ModelState.IsValid)
                {
                    GestionReservas oReserva = _ServiceGestionReservas.Guardar(reserva);
                    lista = _ServiceGestionReservas.GetReservas();
                    lista.Reverse();
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Reserva Guardada",
                               "La Reserva " + oReserva.IDReserva + " ha sido creada. Pronto será revisada por el administrador", Utils.SweetAlertMessageType.success
                               );
                }
                else
                {
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IDTipoInformacion = listaEspacios(reserva.IDReserva);
                    if (reserva.IDReserva > 0)
                    {
                        lista = _ServiceGestionReservas.GetReservas();
                        lista.Reverse();
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al Guardar",
                                   "La Reserva " + reserva.IDReserva + " no pudo guardarse", Utils.SweetAlertMessageType.error
                                   );
                        return PartialView("_PartialViewListaInformacion", lista);
                    }
                    else
                    {
                        lista = _ServiceGestionReservas.GetReservas();
                        lista.Reverse();
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al Guardar",
                                   "La Reserva " + reserva.IDReserva + " no pudo guardarse", Utils.SweetAlertMessageType.error
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

        public ActionResult AjaxModificarReserva(int? id)
        {
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            GestionReservas gestionReservas = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                gestionReservas = _ServiceGestionReservas.GetReservaByID(Convert.ToInt32(id));

                if (gestionReservas == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "ListaResidencias";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDEspacios = listaEspacios(gestionReservas.IDEspacio);
                return PartialView("_PartialViewModificarReserva", gestionReservas);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ListaResidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            
        }

        /**/

        public ActionResult actualizarEstado(int id, bool estado)
        {
            IEnumerable<GestionReservas> lista = null;
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            GestionReservas reserva = _ServiceGestionReservas.GetReservaByID(id);

            if (estado)
                reserva.IDEstado = 2;
            else
                reserva.IDEstado = 3;

            try
            {
                if (ModelState.IsValid)
                {
                    GestionReservas oReserva = _ServiceGestionReservas.Guardar(reserva);

                    lista = _ServiceGestionReservas.GetReservas();
                    lista.Reverse();
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("ÉXITO!", "Se modificó correctamente", SweetAlertMessageType.success);

                    return PartialView("_PartialViewListaReserva", lista);
                }
                else
                {
                    TempData["Message"] = "No existe el incidente solicitado";
                    TempData["Redirect"] = "Reservacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Reservacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        /**/

        // POST: GestionReserva/Create


        private SelectList listaEspacios(int id = 0)
        {
            IServiceEspacios _ServiceEspacios = new ServiceEspacios();
            IEnumerable<Espacios> lista = _ServiceEspacios.GetEspacios();
            return new SelectList(lista, "IDEspacio", "descripcion", id);
        }

        // GET: GestionReserva/Edit/5
        public ActionResult Edit(int? id)
        {
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            GestionReservas gestionReservas = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                gestionReservas = _ServiceGestionReservas.GetReservaByID(Convert.ToInt32(id));

                if (gestionReservas == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "ListaResidencias";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDEspacios = listaEspacios(gestionReservas.IDEspacio);
                return View(gestionReservas);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ListaResidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult _PartialViewListaReserva()
        {
            return PartialView("_PartialViewListaReserva");
        }

        public ActionResult _PartialViewDetalleReserva(int? id)
        {

            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            GestionReservas gestionReservas = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }
                gestionReservas = _ServiceGestionReservas.GetReservaByID((int)id);

                if (gestionReservas == null)
                {
                    {
                        TempData["Message"] = "No existe la reserva solicitada";
                        TempData["Redirect"] = "GestionReservas";
                        TempData["Redirect-Action"] = "Index";
                        // Redireccion a la captura del Error
                        return RedirectToAction("Default", "Error");
                    }
                }
                return PartialView("_PartialViewDetalleReserva", gestionReservas);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionPlanCobros";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            
        }
    }
}
