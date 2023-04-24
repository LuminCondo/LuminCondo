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
                Paragraph appName = new Paragraph("LuminCondo").
                            SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)).
                            SetFontSize(20).
                            SetFontColor(new DeviceRgb(12, 176, 187)).
                            SetTextAlignment(TextAlignment.CENTER);

                doc.Add(appName);
                var nombre = "Historial de deudas al " + DateTime.Today.ToString("dd-MM-yyyy");
                Paragraph header = new Paragraph(nombre).
                            SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)).
                            SetFontSize(14).
                            SetFontColor(new DeviceRgb(12, 176, 187)).
                            SetTextAlignment(TextAlignment.CENTER);

                doc.Add(header);


                Table table = new Table(4, true);

                Cell headerCell = new Cell().Add(new Paragraph("Número de Residencia"))
                             .SetBackgroundColor(new DeviceRgb(12, 176, 187));
                table.AddHeaderCell(headerCell);

                headerCell = new Cell().Add(new Paragraph("Descripción del Plan"))
                                       .SetBackgroundColor(new DeviceRgb(12, 176, 187));
                table.AddHeaderCell(headerCell);

                headerCell = new Cell().Add(new Paragraph("Fecha de Asignación"))
                                       .SetBackgroundColor(new DeviceRgb(12, 176, 187));
                table.AddHeaderCell(headerCell);

                headerCell = new Cell().Add(new Paragraph("Monto Total"))
                                       .SetBackgroundColor(new DeviceRgb(12, 176, 187));
                table.AddHeaderCell(headerCell);
                decimal totalAmount = 0;

                foreach (var item in listaDeudas)
                {
                    table.AddCell(new Paragraph(item.IDResidencia.ToString()));
                    table.AddCell(new Paragraph(item.GestionPlanCobros.descripcion.ToString()));
                    table.AddCell(new Paragraph(item.fechaAsignacion.ToShortDateString()));
                    decimal amount = item.GestionPlanCobros.total;
                    totalAmount += amount;
                    table.AddCell(new Paragraph("CRC " + amount.ToString("N2")));
                }
                doc.Add(table);

                LineSeparator line = new LineSeparator(new SolidLine());
                doc.Add(line);
                // Crear el parrafo para mostrar el total fuera de la tabla
                Paragraph total = new Paragraph("Total: " + totalAmount.ToString("N2")).SetBold().SetTextAlignment(TextAlignment.RIGHT);
                doc.Add(total);

                Paragraph Cierre = new Paragraph("***Fin del Reporte****Gracias por usar LuminCondo***").SetTextAlignment(TextAlignment.CENTER);
                doc.Add(Cierre);

                Paragraph Footer1 = new Paragraph("©Derechos Reservados "+DateTime.Today.Year).SetTextAlignment(TextAlignment.CENTER);
                doc.Add(Footer1);
                Paragraph Footer2 = new Paragraph("LuminCondo").SetTextAlignment(TextAlignment.CENTER);
                doc.Add(Footer2);
                

                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("Página {0} de {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }

                //Close document
                doc.Close();
                // Retorna un File
                
                return File(ms.ToArray(), "application/pdf", nombre + ".pdf" );
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