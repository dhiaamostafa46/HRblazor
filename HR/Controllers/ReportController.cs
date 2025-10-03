using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HR.Controllers
{
    public class ReportController : Controller
    {
        private readonly IConverter _converter;

        public ReportController(IConverter converter)
        {
            _converter = converter;
        }

        public IActionResult GeneratePDF()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = "<h1>This is a PDF Report</h1>",
                    }
                }
            };

            var file = _converter.Convert(doc);
            return File(file, "application/pdf", "Report.pdf");
        }
    }
}
