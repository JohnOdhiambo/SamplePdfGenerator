using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SamplePdfGenerator.Models;

namespace SamplePdfGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Chart()
        {
            return View();
        }
        //public JsonResult ExportReport(string imageData)
        //{
        //    string fileName = Path.Combine(Server.MapPath("~/ExportImage"), DateTime.Now.ToString("ddMMyyyyhhmmsstt") + ".png");
        //    using (FileStream fs = new FileStream(fileName, FileMode.Create))
        //    {
        //        using (BinaryWriter bw = new BinaryWriter(fs))
        //        {
        //            byte[] data = Convert.FromBase64String(imageData);
        //            bw.Write(data);
        //            bw.Close();
        //        }
        //    }
        //    return new JsonResult { Data = "Image saved successfully" };
        //}
        //public ActionResult Report()
        //{
        //    SelectPdf.GlobalProperties.HtmlEngineFullPath = HttpContext.ApplicationInstance.Server.MapPath("~/App_Data/Select.Html.dep");
        //    try
        //    {
        //        string pdf_page_size = "A4";
        //        PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
        //            pdf_page_size, true);

        //        string pdf_orientation = "Portrait";
        //        PdfPageOrientation pdfOrientation =
        //            (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
        //                pdf_orientation, true);

        //        int webPageWidth = 1600;

        //        int webPageHeight = 0;

        //        // instantiate a html to pdf converter object
        //        HtmlToPdf converter = new HtmlToPdf();

        //        // set converter options
        //        converter.Options.PdfPageSize = pageSize;
        //        converter.Options.PdfPageOrientation = pdfOrientation;
        //        converter.Options.WebPageWidth = webPageWidth;
        //        converter.Options.WebPageHeight = webPageHeight;

        //        // create a new pdf document converting an url
        //        PdfDocument doc =
        //            converter.ConvertUrl(string.Format("http://{0}:{1}/Home/Index", HttpContext.Request.Url.Host,
        //                HttpContext.Request.Url.Port));

        //        // save pdf document
        //        doc.Save(HttpContext.ApplicationInstance.Response, false, "Sample.pdf");

        //        // close pdf document
        //        doc.Close();

        //        return Content("");
        //    }
        //    catch (Exception e)
        //    {
        //        return (e);
        //    }

        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
