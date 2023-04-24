using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using static Web.ViewModel.GraficoController;

namespace Web.Controllers
{
    public class ReporteGraficoController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            //Documentación chartjs https://www.chartjs.org/docs/latest/

            IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
            ViewModelGraficoController grafico = new ViewModelGraficoController();
            _ServiceGestionAsignacionPlanes.GetGrafico(out string etiquetas, out string valores);
            grafico.Etiquetas = etiquetas;
            grafico.Valores = valores;
            int cantidadValores = valores.Split(',').Length;
            grafico.Colores = string.Join(",", grafico.GenerateColors(cantidadValores));
            grafico.titulo = "Ingresos por Mes";
            grafico.tituloEtiquetas = "Ingresos por Mes";
            grafico.tipo = "doughnut";
            ViewBag.grafico = grafico;
            return View();
        }
    }
}
