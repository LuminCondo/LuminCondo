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
    public class GestionReservaController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: GestionReserva
        public ActionResult Index()
        {
            try
            {
                IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
                IEnumerable<GestionReservas> lista = _ServiceGestionReservas.GetReservas();
                ViewBag.IDEstadosDeReserva = ListaEstadosReserva();
                ViewBag.listaReservas = lista;
                return View();
            }
            catch (Exception ex)
            {
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult BuscarHistorial(int? id)
        {
            try
            {
                IEnumerable<GestionReservas> lista = null;
                IServiceGestionReservas _ServiceGestionReservas = new ServiceGestionReservas();
                lista = _ServiceGestionReservas.GetHistorial(id);
                return PartialView("_PartialViewListaReserva", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "ListaResidencias";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: GestionReserva/Details/5
        private SelectList ListaUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuarios> lista = _ServiceUsuario.GetUsuarios();
            return new SelectList(lista, "ID", "nombre", idUsuario);
        }

        private SelectList ListaEstadosReserva(int idEstado = 0)
        {
            IServiceEstadoReserva _ServiceEstadoReserva = new ServiceEstadoReserva();
            IEnumerable<EstadoReserva> lista = _ServiceEstadoReserva.GetEstadoReserva();
            return new SelectList(lista, "IDEstado", "descripcion", idEstado);
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        public ActionResult Create()
        {
            ViewBag.IDUsuarios = ListaUsuarios();
            ViewBag.IDEspacios = ListaEspacios(DateTime.Today);
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

            DateTime.TryParse(fecha, out DateTime fechacovertida);
            IServiceEspacios _ServiceEspacios = new ServiceEspacios();
            IEnumerable<Espacios> lista = _ServiceEspacios.GetEspaciosxFecha(fechacovertida);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        private SelectList ListaEspacios(DateTime fecha)
        {
            IServiceEspacios _ServiceEspacios = new ServiceEspacios();
            IEnumerable<Espacios> lista = _ServiceEspacios.GetEspaciosxFecha(fecha);
            return new SelectList(lista, "IDEspacio", "descripcion", 0);
        }

        public ActionResult Guardar(GestionReservas reserva)
        {
            Usuarios usuario = (Usuarios)Session["User"];
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
                    ViewBag.IDUsuarios = ListaUsuarios();
                    ViewBag.IDEspacios = ListaEspacios(reserva.fecha);
                    return View("Create", reserva);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Infraestructure.Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionReserva";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult ActualizarEstado(int id, bool estado)
        {
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

                    IEnumerable<GestionReservas> lista = _ServiceGestionReservas.GetReservas();

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
                    TempData["Redirect"] = "GestionReserva";
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
                TempData["Redirect"] = "GestionReserva";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult _PartialViewListaReserva()
        {
            return PartialView("_PartialViewListaReserva");
        }
    }
}
