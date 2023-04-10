using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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
