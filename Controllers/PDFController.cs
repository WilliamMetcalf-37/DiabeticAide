using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

namespace DiabeticAide.Controllers
{
    public class PDFController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
        public PDFController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public ActionResult ConvertToPDF(string userId)
        {
            var converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
            converter.ConverterSettings = settings;

            PdfDocument document = converter.Convert($"https://localhost:5001/Data/Details/{userId}");

            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close(true);

            ms.Position = 0;
            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            fileStreamResult.FileDownloadName = "Data.pdf";

            
            return RedirectToAction("Index", "Data");
        }

    }
}
