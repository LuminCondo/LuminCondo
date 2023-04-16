using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class GestionReservaController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
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
        private SelectList listaUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuarios> lista = _ServiceUsuario.GetUsuarios();
            return new SelectList(lista, "ID", "nombre", idUsuario);
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        public ActionResult Create()
        {
            ViewBag.IDUsuarios = listaUsuarios();
            ViewBag.IDEspacios = listaEspacios(DateTime.Today);
            GestionReservas objReserva = new GestionReservas()
            {
                fecha = DateTime.Today,
                IDEstado = 1
            };
            return View(objReserva);
        }

        // GET: GestionReserva/Create
        public ActionResult ObtenerEspaciosDisponibles(string fecha)
        {
            DateTime fechacovertida;
            DateTime.TryParse(fecha, out fechacovertida);
            IServiceEspacios _ServiceEspacios = new ServiceEspacios();
            IEnumerable<Espacios> lista = _ServiceEspacios.GetEspaciosxFecha(fechacovertida);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        private SelectList listaEspacios(DateTime fecha)
        {
            IServiceEspacios _ServiceEspacios = new ServiceEspacios();
            IEnumerable<Espacios> lista = _ServiceEspacios.GetEspaciosxFecha(fecha);
            return new SelectList(lista, "IDEspacio", "descripcion", 0);
        }

        public ActionResult Guardar(GestionReservas reserva)
        {
            Usuarios usuario = new Usuarios();
            usuario = (Usuarios)Session["User"];
            if (reserva.IDUsuario<1)
            {
                reserva.IDUsuario = usuario.ID;
            }
            reserva.IDEstado = 1;
            IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
            try
            {
                if (ModelState.IsValid)
                {
                    GestionReservas oReserva = _ServiceGestionReservas.Guardar(reserva);
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Reserva Guardada",
                               "La Reserva " + oReserva.IDReserva + " ha sido creada. Pronto será revisada por el administrador", Utils.SweetAlertMessageType.success
                               );
                }
                else
                {
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IDUsuarios = listaUsuarios();
                    ViewBag.IDEspacios = listaEspacios(reserva.fecha);
                    return View("Create", reserva);
                }

                return RedirectToAction("IndexAdmin");
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

                    if (reserva.IDEstado == 2)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Estado de la reserva actualizado", "La reserva fue aprobada", SweetAlertMessageType.success);
                    }
                    if (reserva.IDEstado == 3)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Estado de la reserva actualizado", "La reserva fue rechazada", SweetAlertMessageType.error);
                    }


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
