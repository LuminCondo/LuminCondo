using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using System;
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

        // GET: Informacion/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            Informacion informacion = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                informacion = _ServiceInformacion.GetInformacionByID(Convert.ToInt32(id));
                if (informacion == null)
                {
                    TempData["Message"] = "No existe el Plan solicitado";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(informacion);
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
        [CustomAuthorize((int)Roles.Administrador)]
        // GET: Informacion/Create
        public ActionResult Create()
        {
            ViewBag.IDTipoInformacion = listaTipoInformacion();
            return View();
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
                ModelState.Remove("fechapublicacion");
                ModelState.Remove("IDInformacion");
                ModelState.Remove("IDTipoInfo");
                if (ModelState.IsValid)
                {
                    Informacion oInformacion = _ServiceInformacion.Guardar(informacion);
                }
                else
                {
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IDTipoInformacion = listaTipoInformacion(informacion.IDTipoInfo);
                    if (informacion.IDTipoInfo > 0)
                    {
                        return (ActionResult)View("Edit", informacion);
                    }
                    else
                    {
                        return View("Create", informacion);
                    };
                }

                return RedirectToAction("Index");
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

        // GET: Informacion/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
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
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Libro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IdTipoInformacion = listaTipoInformacion(informacion.IDTipoInfo);
                return View(informacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Utils.Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}