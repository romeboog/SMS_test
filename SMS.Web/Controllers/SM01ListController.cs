using SMS.BLL.Services;
using Microsoft.Reporting.WebForms;
using Novacode;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.Domain.ViewModels;

namespace SMS.Web.Controllers
{
    [Authorize]
    public class SM01ListController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            //SM01Service SM01S = new SM01Service();
            //var models = SM01S.Get().ToList();
            var models = GetListData();
            return View(models);
        }

        public ActionResult Export(string type,string year,string org, string dept)
        {
            // 讀取.rdlc檔
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Rdlc"), "rdlcSM01List.rdlc");
            lr.ReportPath = path;

            // 取得訂單資料
            //SM01Service SM01S = new SM01Service();
            //var list = SM01S.Get().ToList();
            var models = GetListDataByDept(year,org,dept);
            // 設定資料集
            ReportDataSource rd = new ReportDataSource("DataSet1", models);
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
            string extname = "";
            if (type == "word")
                extname = ".doc";
            else if (type == "Excel")
                extname = ".xls";
            else extname = ".pdf";
            // 回傳檔案
            return File(renderedBytes, mimeType, "Report"+extname);
        }

        public ActionResult ExportByDocx()
        {
            // 樣板路徑
            string TemplatePath = Server.MapPath("~/Templates/TestTemplate.docx");
            // 儲存路徑
            string SavePath = @"D:/test.docx";
            DocX document = DocX.Load(TemplatePath);
            document.ReplaceText("{{OrderNumber}}", "555555");
            document.ReplaceText("{{Name}}", "Kyle");
            document.ReplaceText("{{CurrTime}}", DateTime.Now.ToShortTimeString());
            document.SaveAs(SavePath);
            return File(SavePath, "application/docx", "Report.docx");
        }

        public ActionResult UploadOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadOrder(IEnumerable<HttpPostedFileBase> UploadFile)
        {
            ViewBag.PageName = "批次上傳";

            // 儲存檔案
            foreach (var file in UploadFile)
            {
                if (file == null)
                {
                    ViewBag.Error = "請選擇檔案!";
                    return View();
                }

                if (Path.GetExtension(file.FileName) != ".xlsx")
                {
                    ViewBag.Error = "副檔名錯誤，請下載樣板並上傳資料!";
                    return View();
                }

                MemoryStream ms = new MemoryStream();
                file.InputStream.CopyTo(ms);

                string FilePath = @"D:\";
                file.InputStream.Position = 0;
                string FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(file.FileName);
                file.SaveAs(FilePath + FileName);
                ms.Dispose();
                ms.Close();

                // NPOI讀取
                XSSFWorkbook wb;
                using (FileStream fs = new FileStream(FilePath + FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    wb = new XSSFWorkbook(fs);
                    XSSFSheet MySheet;
                    MySheet = (XSSFSheet)wb.GetSheetAt(0);
                    // 迴圈讀每筆資料，從1開始(跳過標題列)
                    for (int i = 1; i <= MySheet.LastRowNum; i++)
                    {
                        XSSFRow Row = (XSSFRow)MySheet.GetRow(i);

                        // 讀取每欄資料
                        for (int k = 0; i < Row.Cells.Count; i++)
                        {
                            string MyTemp = Row.GetCell(k).ToString();
                        }
                    }
                }

            }

            return View();
        }

        public ActionResult ExportExcelByNPOI()
        {
            SM01Service SM01S = new SM01Service();
            var models = SM01S.Get();
            // 讀取樣板
            string ExcelPath = Server.MapPath("~/Templates/ExportTemplete.xlsx");
            FileStream Template = new FileStream(ExcelPath, FileMode.Open, FileAccess.Read);
            IWorkbook workbook = new XSSFWorkbook(Template);
            Template.Close();

            ISheet _sheet = workbook.GetSheetAt(0);
            // 取得剛剛在Excel設定的字型 (第二列首欄)
            ICellStyle CellStyle = _sheet.GetRow(1).Cells[0].CellStyle;

            int CurrRow = 1; //起始列(跳過標題列)
            foreach (var item in models)
            {
                IRow MyRow = _sheet.CreateRow(CurrRow);
                CreateCell(item.year.ToString(), MyRow, 0, CellStyle); //訂單編號
                CreateCell(item.org.ToString(), MyRow, 1, CellStyle); //訂單日期
                CreateCell(item.dept.ToString(), MyRow, 2, CellStyle); //客戶編號
                CreateCell(item.soft_name.ToString(), MyRow, 3, CellStyle); //客戶名稱
                CurrRow++;
            }

            string SavePath = @"D:/test.xlsx";
            FileStream file = new FileStream(SavePath, FileMode.Create);
            workbook.Write(file);
            file.Close();

            return File(SavePath, "application/excel", "Report.xlsx");
        }

        /// <summary>NPOI新增儲存格資料</summary>
        /// <param name="Word">顯示文字</param>
        /// <param name="ContentRow">NPOI IROW</param>
        /// <param name="CellIndex">儲存格列數</param>
        /// <param name="cellStyleBoder">ICellStyle樣式</param>
        private static void CreateCell(string Word, IRow ContentRow, int CellIndex, ICellStyle cellStyleBoder)
        {
            ICell _cell = ContentRow.CreateCell(CellIndex);
            _cell.SetCellValue(Word);
            _cell.CellStyle = cellStyleBoder;
        }

        private List<SM01DirectoryViewModel> GetListData()
        {
            int c = 1;
            SM01Service SM01Ser = new SM01Service();
            SM02Service SM02Ser = new SM02Service();
            BD04Service BD04Ser = new BD04Service();
            List<SM01DirectoryViewModel> result = new List<SM01DirectoryViewModel>();
            List<SM01ViewModel> SM01 = SM01Ser.Get().ToList();
            foreach(SM01ViewModel s01 in SM01)
            {
                SM01DirectoryViewModel _result = new SM01DirectoryViewModel();
                SM02ViewModel s02 = SM02Ser.Get(s01.year,s01.org,s01.dept,s01.soft_id).OrderBy(x=>x.detail_id).FirstOrDefault();
                if (s02 != null)
                {

                    _result.directory_sn = c;
                    _result.soft_id = s01.soft_id;
                    _result.soft_type_name = BD04Ser.Get("SFTP").Where(x => x.bcc_id2 == int.Parse(s01.soft_type.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_name = s01.soft_name;
                    _result.soft_ver = s02.soft_ver;
                    _result.soft_sn = s01.soft_sn;
                    _result.soft_max_user = s01.soft_max_user;
                    _result.soft_work_on_name = BD04Ser.Get("WKON").Where(x => x.bcc_id2 == int.Parse(s01.soft_work_on.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_max_user = s01.soft_max_user;
                    _result.soft_platform = s01.soft_platform;
                    _result.soft_number = s01.soft_number;
                    _result.soft_from_name = BD04Ser.Get("FROM").Where(x => x.bcc_id2 == int.Parse(s01.soft_from.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_from_unit = s01.soft_from_unit;
                    _result.soft_keeper = s01.soft_keeper;
                    _result.soft_doc = s01.soft_doc;
                    _result.soft_cost = s01.soft_cost;
                    _result.keep_org = s02.keep_org;
                    _result.keep_man = s02.keep_man;
                    _result.use_org = s02.use_org;
                    _result.use_man = s02.use_man;
                    _result.install_date = s01.install_date;
                    _result.soft_for = s01.soft_for;
                    _result.memo = s01.memo;
                    result.Add(_result);
                    c++;
                }
            }
            
            return result;
        }
        private List<SM01DirectoryViewModel> GetListDataByDept(string year, string org, string dept)
        {
            int c = 1;
            SM01Service SM01Ser = new SM01Service();
            SM02Service SM02Ser = new SM02Service();
            BD04Service BD04Ser = new BD04Service();
            List<SM01DirectoryViewModel> result = new List<SM01DirectoryViewModel>();
            List<SM01ViewModel> SM01 = SM01Ser.Get().Where(x=>x.year== year && x.org == org && x.dept == dept).ToList();
            foreach (SM01ViewModel s01 in SM01)
            {
                SM01DirectoryViewModel _result = new SM01DirectoryViewModel();
                SM02ViewModel s02 = SM02Ser.Get(s01.year, s01.org, s01.dept, s01.soft_id).OrderBy(x => x.detail_id).FirstOrDefault();
                if (s02 != null)
                {

                    _result.directory_sn = c;
                    _result.soft_id = s01.soft_id;
                    _result.soft_type_name = BD04Ser.Get("SFTP").Where(x => x.bcc_id2 == int.Parse(s01.soft_type.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_name = s01.soft_name;
                    _result.soft_ver = s02.soft_ver;
                    _result.soft_sn = s01.soft_sn;
                    _result.soft_max_user = s01.soft_max_user;
                    _result.soft_work_on_name = BD04Ser.Get("WKON").Where(x => x.bcc_id2 == int.Parse(s01.soft_work_on.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_max_user = s01.soft_max_user;
                    _result.soft_platform = s01.soft_platform;
                    _result.soft_number = s01.soft_number;
                    _result.soft_from_name = BD04Ser.Get("FROM").Where(x => x.bcc_id2 == int.Parse(s01.soft_from.Trim())).FirstOrDefault().bcc_name;
                    _result.soft_from_unit = s01.soft_from_unit;
                    _result.soft_keeper = s01.soft_keeper;
                    _result.soft_doc = s01.soft_doc;
                    _result.soft_cost = s01.soft_cost;
                    _result.keep_org = s02.keep_org;
                    _result.keep_man = s02.keep_man;
                    _result.use_org = s02.use_org;
                    _result.use_man = s02.use_man;
                    _result.install_date = s01.install_date;
                    _result.soft_for = s01.soft_for;
                    _result.memo = s01.memo;
                    result.Add(_result);
                    c++;
                }
            }

            return result;
        }
    }
}