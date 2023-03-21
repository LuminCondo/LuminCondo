﻿using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
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
            IEnumerable<GestionAsignacionPlanes> lista = null;

            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                lista = _ServiceGestionAsignacionPlanes.GetGestionAsignacionPlanes();
                ViewBag.title = "Listado de Planes Asignados";

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
    }
}