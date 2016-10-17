using SMS.Domain.ViewModels;
using SMS.BLL.Services;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SMS.Web.Controllers
{
    [Authorize]
    public class SM01Controller : Controller
    {
        //
        // GET: /SM01/
        public ActionResult Index()
        {
            //WebClient client = new WebClient();
            //client.Headers["Accept"] = "application/json";
            //byte[] rvl = client.DownloadData(new Uri("http://localhost:42289/api/SM01"));
            //string rvlt = Encoding.UTF8.GetString(rvl);
            //IList<SM01ViewModel> models = JsonConvert.DeserializeObject<IList<SM01ViewModel>>(rvlt);
            //return View(models);
            return View();
        }
        public ActionResult Export(string type)
        {
            // 讀取.rdlc檔
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "rdlcSM01List.rdlc");
            lr.ReportPath = path;
            SM01Service SM01Serv = new SM01Service();
            // 取得訂單資料
            var list = SM01Serv.Get();
            // 設定資料集
            ReportDataSource rd = new ReportDataSource("DataSet1", list);
            lr.DataSources.Add(rd);
            // 檔案類型，從參數指定:Excel.pdf.word
            string reportType = type;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + type + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            // 回傳檔案
            return File(renderedBytes, mimeType, "Report");
        }
	}
}