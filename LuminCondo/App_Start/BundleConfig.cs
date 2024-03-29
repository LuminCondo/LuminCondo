﻿using System.Web;
using System.Web.Optimization;

namespace LuminCondo
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                      "~/Scripts/sweetalert.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/fontAwesome").Include(
                      "~/Scripts/FontAwesome/all.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Mybootstrap.min.css",
                      "~/Content/FontAwesome/all.min.css",
                      "~/Content/sweetalert.css",
                      "~/Content/SwitchStyle.css",
                      "~/Content/LightDarkStyle.css",
                      "~/Content/StylesLuminCondo.css"));
            bundles.Add(new StyleBundle("~/Content/Login").Include(
                      "~/Content/StyleLogin.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery-ui.js"));

        }
    }
}
