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
    public class ListaResidenciasController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

        // GET: ListaResidencias
        public ActionResult Index()
        {
            try
            {
                IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
                IEnumerable<GestionResidencias> lista = _ServiceGestionResidencias.GetGestionResidencias();
                ViewBag.title = "Listado de Residencias";

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


       
        private SelectList ListaUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuarios> lista = _ServiceUsuario.GetUsuarios();
            return new SelectList(lista, "ID", "nombre", idUsuario);
        }
        private SelectList ListaEstadosResidencia(int idEstado = 0)
        {
            IServiceEstadoResidencia _ServiceEstadoResidencia = new ServiceEstadoResidencia();
            IEnumerable<EstadoResidencia> lista = _ServiceEstadoResidencia.GetEstadoResidencia();
            return new SelectList(lista, "IDEstadoResidencia", "estado", idEstado);
        }

        public ActionResult _PartialViewListaCarros()
        {
            return PartialView("_PartialViewListaCarros");
        }

        public ActionResult _PartialViewListaPersonas()
        {
            return PartialView("_PartialViewListaPersonas");
        }

        public ActionResult _PartialViewDetalleResidencia(int? id)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            GestionResidencias gestionResidencias = _ServiceGestionResidencias.GetGestionResidenciasByID(Convert.ToInt32(id));
            return PartialView("_PartialViewDetalleResidencia", gestionResidencias);
        }

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]
        // GET: ListaResidencias/Create
        public ActionResult Administrar(int idResidencia)
        {
            try
            {
                IServicePersonas _ServicePersonas = new ServicePersonas();
                IEnumerable<Personas> listapersonas = _ServicePersonas.GetPersonasxIDResidencia(idResidencia);
                IServiceCarros _ServiceCarros = new ServiceCarros();
                IEnumerable<Carros> listacarros = _ServiceCarros.GetCarrosxIDResidencia(idResidencia);

                ViewBag.IdResidencia = idResidencia;
                ViewBag.CarrosResidentes = listacarros;
                ViewBag.PersonasResidentes = listapersonas;


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

        public ActionResult AjaxModificarResidencia(int id)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            try
            {
                GestionResidencias gestionResidencias = _ServiceGestionResidencias.GetGestionResidenciasByID(Convert.ToInt32(id));

                if (gestionResidencias == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "ListaResidencias";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDUsuarios = ListaUsuarios(gestionResidencias.IDUsuario);
                ViewBag.IDEstadoResidencias = ListaEstadosResidencia(gestionResidencias.IDEstadoResidencia);
                return PartialView("_PartialViewModificarResidencia",gestionResidencias);

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

        public ActionResult Guardar(GestionResidencias gestionResidencias)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();

            try
            {
                ModelState.Remove("IDResidencia");
                ModelState.Remove("cantPersonas");
                ModelState.Remove("cantCarros");
                if (ModelState.IsValid)
                {
                    GestionResidencias oGestionResidencias = _ServiceGestionResidencias.Guardar(gestionResidencias);
                }
                else
                {
                    Util.ValidateErrors(this);
                    ViewBag.IDUsuarios = ListaUsuarios(gestionResidencias.IDUsuario);
                    ViewBag.IDEstadoResidencias = ListaEstadosResidencia(gestionResidencias.IDEstadoResidencia);
                    if (gestionResidencias.IDResidencia > 0)
                    {
                        return (ActionResult)View("Edit", gestionResidencias);
                    }
                    else
                    {
                        return View("Create", gestionResidencias);
                    }
                }
                return RedirectToAction("Administrar", new { idResidencia = gestionResidencias.IDResidencia });
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

        public ActionResult AjaxCrearCarro(int id)
        {
            Carros carro = new Carros
            {
                IDResidencia = id
            };
            return PartialView("_PartialViewCrearCarro", carro);
        }

        public ActionResult AjaxCrearResidencia()
        {
            ViewBag.IDUsuarios = ListaUsuarios();
            ViewBag.IDEstadoResidencias = ListaEstadosResidencia();
            return PartialView("_PartialViewCrearResidencia");
        }
            

        public ActionResult AjaxModificarCarro(string id)
        {
            IServiceCarros _ServiceCarros = new ServiceCarros();
            Carros carro = _ServiceCarros.GetCarrosByID(id);
            return PartialView("_PartialViewModificarCarro", carro);
        }

        public ActionResult GuardarCarro(Carros carro)
        {
            IServiceCarros _ServiceCarros = new ServiceCarros();
            try
            {
                IEnumerable<Carros> lista = null;
                if (ModelState.IsValid)
                {
                    Carros oCarros = _ServiceCarros.GetCarrosByID(carro.IDPlaca);
                    Carros oCarro = _ServiceCarros.Guardar(carro);
                     lista = _ServiceCarros.GetCarrosxIDResidencia(oCarro.IDResidencia);
                    if (oCarros==null)
                    {
                        IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
                        GestionResidencias gestionResidencias = null;
                        gestionResidencias = _ServiceGestionResidencias.GetGestionResidenciasByID(Convert.ToInt32(oCarro.IDResidencia));
                        gestionResidencias.cantCarros++;
                        GestionResidencias oGestionResidencias = _ServiceGestionResidencias.Guardar(gestionResidencias);
                    }
                }
                else
                {
                    lista = _ServiceCarros.GetCarrosxIDResidencia(carro.IDResidencia);
                }
                return PartialView("_PartialViewListaCarros", lista);
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

        public ActionResult AjaxCrearPersona(int id)
        {
            Personas personas = new Personas
            {
                IDResidencia = id
            };
            return PartialView("_PartialViewCrearPersona", personas);
        }

        public ActionResult AjaxModificarPersona(int id)
        {
            IServicePersonas _ServicePersonas = new ServicePersonas();
            Personas personas = _ServicePersonas.GetPersonasByID(id);
            return PartialView("_PartialViewModificarPersona", personas);
        }

        public ActionResult GuardarPersona(Personas personas)
        {
            IServicePersonas _ServicePersonas = new ServicePersonas();
            
            try
            {
                IEnumerable<Personas> lista = null;
                if (ModelState.IsValid)
                {

                    Personas oPersonas = _ServicePersonas.Guardar(personas);
                    IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
                    GestionResidencias gestionResidencias = null;
                    gestionResidencias = _ServiceGestionResidencias.GetGestionResidenciasByID(Convert.ToInt32(oPersonas.IDResidencia));
                    gestionResidencias.cantPersonas++;
                    GestionResidencias oGestionResidencias = _ServiceGestionResidencias.Guardar(gestionResidencias);
                    lista = _ServicePersonas.GetPersonasxIDResidencia(oPersonas.IDResidencia);
                }
                else
                {
                    lista = _ServicePersonas.GetPersonasxIDResidencia(personas.IDResidencia);
                }
                return PartialView("_PartialViewListaPersonas", lista);
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
