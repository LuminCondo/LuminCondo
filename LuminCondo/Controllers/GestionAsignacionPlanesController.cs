﻿using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

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
                if (ModelState.IsValid)
                {
                    GestionAsignacionPlanes oGestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.Guardar(gestionAsignacionPlanes);
                    if (oGestionAsignacionPlanes==null)
                    {
                        ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Fallo al guardar la asignación",
                            "Ya esta residencia posee una asignación establecida para este mes", Utils.SweetAlertMessageType.error
                            );
                        ViewBag.IDResidencias = listaResidencias(gestionAsignacionPlanes.IDResidencia);
                        ViewBag.IDPlanes = listaPlanes(gestionAsignacionPlanes.IDPlan);
                        return View("Create");

                    }
                }
                else
                {
                    
                    ViewBag.IDResidencias = listaResidencias(gestionAsignacionPlanes.IDResidencia);
                    ViewBag.IDPlanes = listaPlanes(gestionAsignacionPlanes.IDPlan);
                    if (gestionAsignacionPlanes.IDPlan > 0)
                    {
                        return (ActionResult)View("Edit", gestionAsignacionPlanes);
                    }
                    else
                    {
                        return View("Create", gestionAsignacionPlanes);
                    }
                }
                return RedirectToAction("Index");
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
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: GestionAsignacionPlanes/Details/5
        public ActionResult Details(int? id)
        {
            IServiceGestionPlanCobros _ServiceGestionPlanCobros = new ServiceGestionPlanCobros();
            GestionPlanCobros planCobros = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                planCobros = _ServiceGestionPlanCobros.GetGestionPlanCobrosByID(Convert.ToInt32(id));
                if (planCobros == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "GestionPlanCobros";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(planCobros);
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
        // GET: GestionAsignacionPlanes/Create
        public ActionResult Create()
        {
            ViewBag.IDResidencias = listaResidencias();
            ViewBag.IDPlanes = listaPlanes();
            return View();
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

        [CustomAuthorize((int)Roles.Administrador)]
        // GET: GestionAsignacionPlanes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.IDResidencias = listaResidencias();
            ViewBag.IDPlanes = listaPlanes();
            


            IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
            GestionAsignacionPlanes gestionAsignacionPlanes = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                gestionAsignacionPlanes = _ServiceGestionAsignacionPlanes.GetGestionAsignacionPlanesByID(Convert.ToInt32(id));

                if (gestionAsignacionPlanes == null)
                {
                    TempData["Message"] = "No existe la asignacion solicitada";
                    TempData["Redirect"] = "GestionAsignacionPlanes";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDResidencias = listaResidencias(gestionAsignacionPlanes.IDResidencia);
                ViewBag.IDPlanes = listaPlanes(gestionAsignacionPlanes.IDPlan);
                return View(gestionAsignacionPlanes);

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
    }
}
