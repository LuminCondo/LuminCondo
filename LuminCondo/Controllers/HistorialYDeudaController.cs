using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class HistorialYDeudaController : Controller
    {
        private SelectList ListaResidencias(int idResidencia = 0)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            IEnumerable<GestionResidencias> lista = _ServiceGestionResidencias.GetGestionResidencias();
            return new SelectList(lista, "IDResidencia", "IDResidencia", idResidencia);
        }
        private SelectList ListaMeses(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            for (int i = 1; i <= 12; i++)
            {
                lista.Add(new SelectListItem { Text = ti.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)), Value = i.ToString() });
            }
            return new SelectList(lista,"Value","Text", mesActual);
        }

        private SelectList Listaannos(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            

            for (int i = 2019; i <= DateTime.Now.Year; i++)
            {
                lista.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return new SelectList(lista, "Value", "Text", mesActual);
        }

        public ActionResult _PartialViewListaHistorial()
        {
            return PartialView("_PartialViewListaHistorial");
        }

        public ActionResult _PartialViewListaDeudas()
        {
            return PartialView("_PartialViewListaDeudas");
        }

        // GET: HistorialYDeuda
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexHistorial()
        {
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                IEnumerable<GestionAsignacionPlanes>  listaHistorial = _ServiceGestionAsignacionPlanes.GetHistorial(DateTime.Now.Month, DateTime.Now.Year, null,true);
                

                ViewBag.IDResidencias = ListaResidencias();
                ViewBag.listameses = ListaMeses(DateTime.Now.Month);
                ViewBag.listaannos = Listaannos(DateTime.Now.Year);
                ViewBag.listaHistorial = listaHistorial;


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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult IndexDeudas()
        {
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                IEnumerable<GestionAsignacionPlanes> listaDeudas = _ServiceGestionAsignacionPlanes.GetHistorial(DateTime.Now.Month, DateTime.Now.Year, null,false);


                ViewBag.IDResidencias = ListaResidencias();
                ViewBag.listameses = ListaMeses(DateTime.Now.Month);
                ViewBag.listaannos = Listaannos(DateTime.Now.Year);
                ViewBag.listaHistorial = listaDeudas;


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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult PagarPlan(int idAsignacion, int? mes, int? anno, int? idResidencia)
        {
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                GestionAsignacionPlanes gestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.GetGestionAsignacionPlanesByID(idAsignacion);
                gestionAsignacionPlanes.estadoPago = true;
                gestionAsignacionPlanes.fechaPago = DateTime.Today;
                GestionAsignacionPlanes oGestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.Guardar(gestionAsignacionPlanes);
                if (oGestionAsignacionPlanes == null)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al pagar la asignación",
                        "Por favor volver a intentar", Utils.SweetAlertMessageType.error
                        );
                    bool estado = false;
                    return RedirectToAction("BuscarHistorial", new { mes, anno, idResidencia, estado });

                }
                else
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Pago Realizado con éxito",
                                            "Muchas gracias por usar LuminCondo", Utils.SweetAlertMessageType.success
                                            );
                    bool estado = false;

                    IEnumerable<GestionAsignacionPlanes> lista = null;
                    lista = _ServiceGestionAsignacionPlanes.GetHistorial(mes, anno, idResidencia, estado);
                    return PartialView("_PartialViewListaDeudas", lista);
                }
            }
            catch (Exception ex)
            {
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: HistorialYDeuda/Details/5
        public ActionResult BuscarHistorial(int? mes, int? anno, int? idResidencia,bool estado)
        {
           
            try
            {
                IEnumerable<GestionAsignacionPlanes> lista = null;
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                lista = _ServiceGestionAsignacionPlanes.GetHistorial(mes, anno, idResidencia,estado);
                if (estado==true)
                {
                    return PartialView("_PartialViewListaHistorial", lista);
                }
                else
                {
                    return PartialView("_PartialViewListaDeudas", lista);
                }
                
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
    }
}
