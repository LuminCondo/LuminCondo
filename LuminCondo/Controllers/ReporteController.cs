using ApplicationCore.Services;
using Infraestructure.Models;
using Infraestructure.Repository;
using Infraestructure.Utils;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            try
            {
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                IEnumerable<GestionAsignacionPlanes> listaHistorial = _ServiceGestionAsignacionPlanes.GetHistorial(DateTime.Now.Month, DateTime.Now.Year, null, false);

                ViewBag.IDResidencias = ListaResidencias();
                ViewBag.listameses = ListaMeses(DateTime.Now.Month);
                ViewBag.listaannos = Listaannos(DateTime.Now.Year);
                ViewBag.listaHistorial = listaHistorial;

                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public  ActionResult _PartialViewListaDeudas()
        {
            return PartialView("_PartialViewListaDeudas");
        }

        public ActionResult BuscarHistorial(int? mes, int? anno, int? idResidencia, bool estado)
        {
            try
            {
                IEnumerable<GestionAsignacionPlanes> lista = null;
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                lista = _ServiceGestionAsignacionPlanes.GetHistorial(mes, anno, idResidencia, estado);
                
                return PartialView("_PartialViewListaDeudas", lista);
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

        private SelectList ListaResidencias(int idResidencia = 0)
        {
            IServiceGestionResidencias _ServiceGestionResidencias = new ServiceGestionResidencias();
            IEnumerable<GestionResidencias> lista = _ServiceGestionResidencias.GetGestionResidencias();
            return new SelectList(lista, "IDResidencia", "IDResidencia", idResidencia);
        }
        private SelectList ListaMeses(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            for (int i = 1; i <= 12; i++)
            {
                lista.Add(new SelectListItem { Text = ti.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i)), Value = i.ToString() });
            }
            return new SelectList(lista, "Value", "Text", mesActual);
        }

        private SelectList Listaannos(int mesActual)
        {
            List<SelectListItem> lista = new List<SelectListItem>();


            for (int i = 2019; i <= DateTime.Now.Year; i++)
            {
                lista.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return new SelectList(lista, "Value", "Text", mesActual);
        }

        public ActionResult CreatePDFDeudas(int? mes, int? anno, int? idResidencia)
        {
            IEnumerable<GestionAsignacionPlanes> listaDeudas = null;
            try
            {
                // Extraer informacion
                IServiceGestionAsignacionPlanes _ServiceGestionAsignacionPlanes = new ServiceGestionAsignacionPlanes();
                listaDeudas = _ServiceGestionAsignacionPlanes.GetHistorial(mes, anno, idResidencia, false);

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Initialize document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                Paragraph header = new Paragraph("Historial de Deudas").
                                                SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA)).
                                                SetFontSize(14).
                                                SetFontColor(ColorConstants.BLUE);

                doc.Add(header);

                Table table = new Table(5, true);

                table.AddHeaderCell("Número de Asignación");
                table.AddHeaderCell("Número de Residencia");
                table.AddHeaderCell("Descripción del Plan");
                table.AddHeaderCell("Fecha de Asignación");
                table.AddHeaderCell("Monto Total");

                foreach (var item in listaDeudas)
                {
                    decimal total = 0;
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.IDAsignacion.ToString()));
                    table.AddCell(new Paragraph(item.IDResidencia.ToString()));
                    table.AddCell(new Paragraph(item.GestionPlanCobros.descripcion.ToString()));
                    table.AddCell(new Paragraph(item.fechaAsignacion.ToShortDateString()));
                    
                    foreach (var i in item.GestionPlanCobros.GestionRubrosCobros)
                    {
                        total += i.monto;
                    }

                    table.AddCell(new Paragraph("₡ " + total.ToString("F2")));
                }
                doc.Add(table);

                LineSeparator line = new LineSeparator(new SolidLine());
                doc.Add(line);

                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }

                //Close document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}