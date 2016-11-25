using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using Framework.Globals;
using Framework.Utility;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;

namespace Framework.Excel
{
    public class MvcExcel : Controller
    {
        private IEnumerable<string> _headers;
        private List<object[]> _listValues;

        public MvcExcel(IEnumerable<string> headers, List<object[]> listValues)
        {
            _headers = headers;
            _listValues = listValues;
        }
        //public ActionResult CreateExcel(IEnumerable<string> headers, List<object[]> listValues)
        public ActionResult CreateExcel(string reportName)
        {
            string xlsPath = AppPath.PhysicalPath(SiteUrls.Instance.Paths["ExcelTemplate"]);
            return CreateExcel(xlsPath, reportName);
        }



        private void GenerateHeader(HSSFSheet sheet, HSSFWorkbook workbook, int startRow, int cellIndex)
        {
            FontRecord rec = new FontRecord() { FontHeight = 16 };
            rec.BoldWeight = short.MaxValue;
            HSSFFont font = new HSSFFont(10, rec);
            //font.Index = 16;
            var style = workbook.CreateCellStyle();
            SetCellStyle(style);
            style.SetFont(font);
            foreach (var header in _headers)
            {
                HSSFRow row = sheet.CreateRow(startRow);
                var cell = row.CreateCell(cellIndex++);
                cell.SetCellValue(Resource.GetString(header));
                cell.CellStyle = style;
            }
        }

        private void SetCellStyle(HSSFCellStyle style)
        {
            style.Alignment = HSSFCellStyle.ALIGN_CENTER;


        }

        private void GenerateReportName(HSSFSheet sheet, string reportName)
        {
            DateTime d = DateTime.Now;
            CellRangeAddress rangeAddress = new CellRangeAddress(1, 1, 1, 2);
            sheet.AddMergedRegion(rangeAddress);
            CellRangeAddress Address = new CellRangeAddress(1, 1, 3, 4);
            sheet.AddMergedRegion(Address);
            HSSFRow reportRow = sheet.CreateRow(1);
            reportRow.CreateCell(1).SetCellValue(Resource.GetString("ReportName") + Resource.GetString(reportName));
            reportRow.CreateCell(3).SetCellValue(Resource.GetString("ReportDate") + d.ConvertMiladiToJalali());
        }

        public ActionResult CreateExcel(string xlsPath, string reportName)
        {
            FileStream fs =
                new FileStream(xlsPath, FileMode.Open, FileAccess.Read);

            HSSFWorkbook templateWorkbook = new HSSFWorkbook(fs, true);

            var style = templateWorkbook.CreateCellStyle();

            SetCellStyle(style);
            HSSFSheet sheet = templateWorkbook.GetSheet("Sheet1");
            //HSSFAnchor a

            sheet.DefaultColumnWidth = 20;
            int cellIndex = 1;
            const int startRow = 3;
            GenerateReportName(sheet, reportName);
            GenerateHeader(sheet, templateWorkbook, startRow, cellIndex);
            int i = 0;
            foreach (var listValue in _listValues)
            {
                cellIndex = 1;
                HSSFRow row = sheet.CreateRow(startRow + ++i);
                foreach (var o in listValue)
                {

                    var cell = row.CreateCell(cellIndex++);
                    if (o != null)
                    {
                        cell.SetCellValue(o.ToString());
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }
                    cell.CellStyle = style;
                }

            }


            //sheet.ForceFormulaRecalculation = true;

            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);

            DateTime d = DateTime.Now;
            string filename = string.Format("ExcelReport{0}_{1}_{2}_{3}_{4}.xls", d.Year, d.Month, d.Day, d.Hour, d.Minute);
            return File(ms.ToArray(), "application/vnd.ms-excel", filename);
        }
        public static byte[] GenerateCompressBtye(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress);
            ds.Write(data, 0, data.Length);
            ds.Flush();
            ds.Close();
            return ms.ToArray();
        }

        private static void ToCompress(HttpContext context, byte[] buffer)
        {
            //string srcFile = "C:\\temp\\file-to-compress.txt";
            buffer = GenerateCompressBtye(buffer);
            GZipStream gzip = null;


            try
            {

                // compress to the destination file
                //gzip.Write(buffer, 0, buffer.Length);
                if (buffer.Length > 0)
                {
                    context.Response.Clear();
                    context.Response.ClearHeaders();
                    context.Response.CacheControl = "public";
                    context.Response.ContentType = "application/octet-stream"; // This is real Hero!
                    DateTime d = DateTime.Now;
                    string filename = string.Format("ExcelReport{0}_{1}_{2}_{3}_{4}.zip", d.Year, d.Month, d.Day, d.Hour, d.Minute);
                    context.Response.AddHeader("Content-disposition", "attachment; filename=" + filename);
                    context.Response.Flush();
                    context.Response.BinaryWrite(buffer);
                    context.Response.End();

                }

            }
            catch (Exception ex)
            {
                // handle or display the error
                //System.Diagnostics.Debug.Assert(false, ex.ToString());
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }
            }
        }
        public void ExportToExcel(HttpContext context, string reportName, bool Compress)
        {

            string xlsPath = AppPath.PhysicalPath(SiteUrls.Instance.Paths["ExcelTemplate"]);
            FileStream fs =
                new FileStream(xlsPath, FileMode.Open, FileAccess.Read);

            HSSFWorkbook templateWorkbook = new HSSFWorkbook(fs, true);

            var style = templateWorkbook.CreateCellStyle();

            SetCellStyle(style);
            HSSFSheet sheet = templateWorkbook.GetSheet("Sheet1");
            //HSSFAnchor a

            sheet.DefaultColumnWidth = 20;
            int cellIndex = 1;
            const int startRow = 3;
            GenerateReportName(sheet, reportName);
            GenerateHeader(sheet, templateWorkbook, startRow, cellIndex);
            int i = 0;
            foreach (var listValue in _listValues)
            {
                cellIndex = 1;
                HSSFRow row = sheet.CreateRow(startRow + ++i);
                foreach (var o in listValue)
                {

                    var cell = row.CreateCell(cellIndex++);
                    if (o != null)
                    {
                        cell.SetCellValue(o.ToString());
                    }
                    else
                    {
                        cell.SetCellValue("");
                    }
                    cell.CellStyle = style;
                }

            }

            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);
            if (Compress)
            {
                ToCompress(context, templateWorkbook.GetBytes());
                return;
            }
            //ToCompress(templateWorkbook.GetBytes());
            DateTime d = DateTime.Now;
            string filename = string.Format("ExcelReport{0}_{1}_{2}_{3}_{4}.xls", d.Year, d.Month, d.Day, d.Hour, d.Minute);
            context.Response.AddHeader("Content-disposition", "attachment; filename=" + filename);
            context.Response.ContentType = "application/vnd.ms-excel";
            context.Response.BinaryWrite(ms.GetBuffer());
            context.Response.End();


        }
    }
}
