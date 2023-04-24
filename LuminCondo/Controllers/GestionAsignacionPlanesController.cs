using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;
using Web.Security;
using static Web.ViewModel.GraficoController;

namespace Web.Controllers
{
    public class GestionAsignacionPlanesController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: GestionAsignacionPlanes
        public ActionResult Index()
        {
            IEnumerable<GestionAsignacionPlanes> listaHistorial = null;

            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                listaHistorial = _ServiceGestionAsignacionPlanes.GetHistorialGeneral(DateTime.Now.Month, DateTime.Now.Year, null);


                ViewBag.IDResidencias = listaResidencias();
                ViewBag.listameses = listaMeses(DateTime.Now.Month);
                ViewBag.listaannos = listaannos(DateTime.Now.Year);
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

        public ActionResult Guardar(GestionAsignacionPlanes gestionAsignacionPlanes)
        {
            IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
            gestionAsignacionPlanes.fechaAsignacion=DateTime.Now;
            gestionAsignacionPlanes.estadoPago = false;

            try
            {
                IEnumerable<GestionAsignacionPlanes> lista = null;
                if (ModelState.IsValid)
                {
                    GestionAsignacionPlanes oGestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.Guardar(gestionAsignacionPlanes);
                    if (oGestionAsignacionPlanes==null)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al guardar la asignación",
                            "Ya esta residencia posee una asignación establecida para este mes", Utils.SweetAlertMessageType.error
                            );
                        lista = _ServiceGestionAsignacionPlanes.GetHistorialGeneral(DateTime.Now.Month, DateTime.Now.Year, null);
                    }
                    else
                    {
                        lista = _ServiceGestionAsignacionPlanes.GetHistorialGeneral(DateTime.Now.Month, DateTime.Now.Year, oGestionAsignacionPlanes.IDResidencia);
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Asignación Creada",
                                   "La asignación ya fue creada para el mes de " + DateTime.Now.Month + " del año " + DateTime.Now.Year, Utils.SweetAlertMessageType.success
                                   );
                    }
                    
                }
                return PartialView("_PartialViewListaAsignaciones", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionAsignacionPlanes";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult _PartialViewListaAsignaciones()
        {
            return PartialView("_PartialViewListaAsignaciones");
        }

        public ActionResult _PartialViewDetails(int? id)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros planCobros = null;
            planCobros = _ServiceGestionPlanCobros.GetGestionPlanCobrosByID(Convert.ToInt32(id));
            return PartialView("_PartialViewDetails", planCobros);
        }

        private SelectList listaResidencias(int idResidencia = 0)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            IEnumerable<GestionResidencias> lista = _ServiceGestionResidencias.GetGestionResidencias();
            return new SelectList(lista, "IDResidencia", "IDResidencia", idResidencia);
        }

        private SelectList listaMeses(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            for (int i = 1; i <= 12; i++)
            {
                lista.Add(new SelectListItem { Text = ti.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)), Value = i.ToString() });
            }
            return new SelectList(lista, "Value", "Text", mesActual);
        }

        private SelectList listaannos(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();


            for (int i = 2019; i <= DateTime.Now.Year; i++)
            {
                lista.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return new SelectList(lista, "Value", "Text", mesActual);
        }

        private SelectList listaPlanes(int idPlan = 0)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            IEnumerable<GestionPlanCobros> lista = _ServiceGestionPlanCobros.GetGestionPlanCobros();
            return new SelectList(lista, "IDPlan", "descripcion", idPlan);
        }

        public ActionResult BuscarHistorialGeneral(int? mes, int? anno, int? idResidencia)
        {

            try
            {
                IEnumerable<GestionAsignacionPlanes> lista = null;
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                lista = _ServiceGestionAsignacionPlanes.GetHistorialGeneral(mes, anno, idResidencia);
                
                    return PartialView("_PartialViewListaAsignaciones", lista);

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

        public ActionResult AjaxCrearAsignacion()
        {
            ViewBag.IDResidencias = listaResidencias();
            ViewBag.IDPlanes = listaPlanes();
            return PartialView("_PartialViewCrearAsignacion");
        }

        public ActionResult AjaxModificarAsignacion(int? id)
        {
            try
            {
                ViewBag.IDResidencias = listaResidencias();
                ViewBag.IDPlanes = listaPlanes();
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                GestionAsignacionPlanes gestionAsignacionPlanes = null;

            
                if (id == null)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("No se puede acceder a esta Asignación",
                                   "La asignación no existe dentro de la base de datos", Utils.SweetAlertMessageType.error
                                   );
                    return PartialView("_PartialViewModificarAsignacion");
                }

                gestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.GetGestionAsignacionPlanesByID(Convert.ToInt32(id));

                if (gestionAsignacionPlanes == null)
                {
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("No se puede acceder a esta Asignación",
                                                       "La asignación no existe dentro de la base de datos", Utils.SweetAlertMessageType.error
                                                       );
                    return PartialView("_PartialViewModificarAsignacion");
                }

                ViewBag.IDResidencias = listaResidencias(gestionAsignacionPlanes.IDResidencia);
                ViewBag.IDPlanes = listaPlanes(gestionAsignacionPlanes.IDPlan);
                return PartialView("_PartialViewModificarAsignacion", gestionAsignacionPlanes);

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "GestionAsignacionPlanes";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult graficoOrden()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/
            IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
            ViewModelGraficoController grafico = new ViewModelGraficoController();
            _ServiceGestionAsignacionPlanes.GetGrafico(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ingresos por Mes";
            grafico.tituloEtiquetas = "Ingresos por Mes";
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}
