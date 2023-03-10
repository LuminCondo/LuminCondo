using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuminCondo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IServiceInformacion _ServiceInformacion=new ServiceInformacion();

            var model = _ServiceInformacion.GetInformacion();
            return View("~/Views/Home/Index.cshtml", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}