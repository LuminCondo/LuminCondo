﻿using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Reflection;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            try
            {
                Usuarios oUsuario = null;
                ModelState.Remove("nombre");
                ModelState.Remove("IDTipoUsuario");
                ModelState.Remove("telefono");
                //Verificar las credenciales
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.email, usuario.contrasenna);
                    
                    if (oUsuario != null && oUsuario.estado == true)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Inicio sesion: {usuario.email}");
                        TempData["mensaje"] = Utils.SweetAlertHelper.Mensaje("Login",
                            "Usuario autenticado", Utils.SweetAlertMessageType.success
                            );
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (oUsuario != null&&oUsuario.estado == false)
                        {
                            Log.Warn($"Intento de inicio: {usuario.email}");
                            ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login",
                                "El usuario se encuentra inactivo", Utils.SweetAlertMessageType.error
                                );
                        }
                        else
                        {
                            Log.Warn($"Intento de inicio: {usuario.email}");
                            ViewBag.NotificationMessage = Utils.SweetAlertHelper.Mensaje("Login",
                                "Usuario no válido", Utils.SweetAlertMessageType.error
                                );
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("Index");
        }

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuarios usuario = Session["User"] as Usuarios;
                Log.Warn($"No autorizado {usuario.email}");
            }
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                //Eliminar variable de sesion
                Session["User"] = null;
                Session.Remove("User");

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}
