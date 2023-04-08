using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamplePdfGenerator.Controllers
{
    public class PdfController : Controller
    {
        public ActionResult Index()
        {
            byte[] bytes = PopulateChart();
            ViewBag.ChartImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes, 0, bytes.Length);
            return View();
        }

        [HttpPost]
        public FileResult Export()
        {
            byte[] bytes = PopulateChart();
            ViewBag.ChartImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes, 0, bytes.Length);
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //Initialize the PDF document object.
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    //Add the Image file to the PDF document object.
                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bytes);
                    pdfDoc.Add(img);
                    pdfDoc.Close();

                    //Download the PDF file.
                    return File(stream.ToArray(), "application/pdf", "Chart.pdf");
                }
            }
        }

        private static byte[] PopulateChart()
        {
            string query = "SELECT TOP 5 ShipCity, COUNT(orderid) TotalOrders";
            query += " FROM Orders WHERE ShipCountry = 'USA' GROUP BY ShipCity";
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            List<OrderModel> chartData = new List<OrderModel>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            chartData.Add(new OrderModel
                            {
                                ShipCity = sdr["ShipCity"].ToString(),
                                TotalOrders = Convert.ToInt32(sdr["TotalOrders"])
                            });
                        }
                    }

                    con.Close();
                }
            }

            Chart chart = new Chart(width: 500, height: 500, theme: ChartTheme.Blue);
            chart.AddTitle("USA City Distribution");
            chart.AddSeries("Default", chartType: "Pie", xValue: chartData, xField: "ShipCity", yValues: chartData, yFields: "TotalOrders");
            return chart.GetBytes(format: "jpeg");
        }

    }
}
