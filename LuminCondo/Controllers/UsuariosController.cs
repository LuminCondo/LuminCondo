using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [CustomAuthorize((int)Roles.Administrador)]

        // GET: ListaResidencias
        public ActionResult Index()
        {
            IEnumerable<Usuarios> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();

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

        private SelectList listaTiposUsuarios(int idTipoUsuario = 0)
        {
            IServiceTiposUsuarios _ServiceTiposUsuarios = new ServiceTiposUsuarios();
            IEnumerable<TiposUsuarios> lista = _ServiceTiposUsuarios.GetTiposUsuarios();
            return new SelectList(lista, "ID", "tipoUsuario", idTipoUsuario);
        }

        public ActionResult Guardar(Usuarios usuarios)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                IEnumerable<Usuarios> lista = null;
                if (ModelState.IsValid)
                {
                    Usuarios oUsuarios = _ServiceUsuario.Guardar(usuarios);
                    lista = _ServiceUsuario.GetUsuarios();
                    ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Usuario Guardado",
                               "El usuario " + oUsuarios.nombre+ " se ha guardado correctamente", Utils.SweetAlertMessageType.success
                               );
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.IDTipodeUsuarios = listaTiposUsuarios(usuarios.IDTipoUsuario);

                    return View("Create", usuarios);
                }

                return PartialView("_PartialViewListaUsuarios", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuarios";
                TempData["Redirect-Action"] = "Create";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult AjaxCrearUsuario()
        {
            Usuarios usuarios = new Usuarios();
            usuarios.estado = true;
            ViewBag.IDTipodeUsuarios = listaTiposUsuarios();
            return PartialView("_PartialViewCrearUsuario", usuarios);
        }

        public ActionResult AjaxModificarUsuario(int? id)
        {
            ServiceUsuario _ServiceUsuarios = new ServiceUsuario();
            Usuarios usuarios = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                usuarios = _ServiceUsuarios.GetUsuarioByID(Convert.ToInt32(id));


                if (usuarios == null)
                {
                    TempData["Message"] = "No existe el usuario solicitado";
                    TempData["Redirect"] = "Usuarios";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.IDTipodeUsuarios = listaTiposUsuarios(usuarios.IDTipoUsuario);
                return PartialView("_PartialViewModificarUsuario", usuarios);

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

        public ActionResult _PartialViewListaUsuarios()
        {
            return PartialView("_PartialViewListaUsuarios");
        }
    }
}
