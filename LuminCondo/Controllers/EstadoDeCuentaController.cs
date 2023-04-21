using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Web.Security;

namespace LuminCondo.Controllers
{
    public class EstadoDeCuentaController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: EstadoDeCuenta
        public ActionResult Index()
        {

            IEnumerable<GestionResidencias> lista = null;
            
            try
            {
                IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
                lista = _ServiceGestionResidencias.GetGestionResidencias();

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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: EstadoDeCuenta/Details/5
        public ActionResult Details(int? id)
        {
            IEnumerable<GestionAsignacionPlanes> listaAsignaciones = null;
            
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                listaAsignaciones = _ServiceGestionAsignacionPlanes.GetEstadodeCuentaByIDResidencia(Convert.ToInt32(id)); 
                return View(listaAsignaciones);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "EstadoDeCuenta";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
    }
}
