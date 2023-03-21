using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace LuminCondo.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

        public ActionResult Index()
        {
            IServiceInformacion _ServiceInformacion=new ServiceInformacion();

            if (TempData.ContainsKey("mensaje"))
            {
                ViewBag.NotificationMessage = TempData["mensaje"];
            }

            var model = _ServiceInformacion.GetInformacion();
            return View("~/Views/Home/Index.cshtml", model);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Residente)]


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}