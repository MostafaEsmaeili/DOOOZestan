using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using Excel;
using HtmlAgilityPack;

namespace Framework.Extensions
{
    public static class ExcelExtensions
    {
        ///<summary>
        ///Method to Read XLS/XLSX File
        ///</summary>
        ///<param name="path">Excel File Full Path</param>
        ///<returns></returns>
        public static DataTable ReadExcelToTable(this string path)
        {
            bool isXLSX = false;

            FileInfo fInfo = new FileInfo(path);
            isXLSX = fInfo.Extension.TrimStart('.').ToLower().EndsWith("xlsx");
            return ReadExcelToTable(path, isXLSX);
        }
        public static DataTable ReadExcelToTable(this string path, bool isXLSX)
        {
            FileStream stream = null;
            DataTable dt = null;
            try
            {
                stream = File.Open(path, FileMode.Open, FileAccess.Read);
                stream.Seek(0, SeekOrigin.Begin);

                IExcelDataReader excelReaderXls = ExcelReaderFactory.CreateBinaryReader(stream);
                dt = excelReaderXls.AsDataSet().Tables[0];
            }
            catch
            {
                dt = null;
            }
            finally
            {
                if (stream != null && (stream.CanRead || stream.CanSeek || stream.CanWrite)) stream.Close();
                //stream.Dispose();
            }
            if (dt != null) return dt;
            try
            {
                stream = File.Open(path, FileMode.Open, FileAccess.Read);
                stream.Seek(0, SeekOrigin.Begin);

                IExcelDataReader excelReaderXlsX = ExcelReaderFactory.CreateOpenXmlReader(stream);
                dt = excelReaderXlsX.AsDataSet().Tables[0];
            }
            catch
            {
                dt = null;
            }
            finally
            {
                if (stream != null && (stream.CanRead || stream.CanSeek || stream.CanWrite)) stream.Close();
            }
            if (dt != null) return dt;
            try
            {
                string connstring = isXLSX
                                        ? "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path +
                                          ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';"
                                        : "Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + path +
                                          ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

                using (OleDbConnection conn = new OleDbConnection(connstring))
                {
                    conn.Open();
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                                                    new object[] { null, null, null, "Table" });
                    string firstSheetName = sheetsName.Rows[0][2].ToString();
                    string sql = string.Format("SELECT * FROM [{0}]", firstSheetName);
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    DataSet set = new DataSet();
                    ada.Fill(set);
                    dt = set.Tables[0];
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
            }
            if (dt != null) return dt;
            try
            {
                stream = File.Open(path, FileMode.Open, FileAccess.Read);
                stream.Seek(0, SeekOrigin.Begin);

                XLWorkbook workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);
                DataTable dt2 = new DataTable();
                foreach (IXLColumn xlColumn in worksheet.Columns())
                {
                    dt2.Columns.Add(xlColumn.Cell(1).Value.ToString());
                }
                foreach (IXLRow xlRow in worksheet.Rows())
                {
                    var dr = dt2.NewRow();
                    for (int i = 0; i < dt2.Columns.Count; i++)
                    {
                        dr[i] = xlRow.Cell(i).Value;
                    }
                    dt2.Rows.Add(dr);
                }
                dt = dt2;
            }
            catch
            {
                dt = null;
            }
            finally
            {
                if (stream != null && (stream.CanRead || stream.CanSeek || stream.CanWrite)) stream.Close();
                //stream.Dispose();
            }
            if (dt != null) return dt;
            try
            {
                DataTable dt3 = new DataTable();
                string content = System.IO.File.ReadAllText(path);
                if (!String.IsNullOrWhiteSpace(content))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(content);
                    HtmlNode table = doc.DocumentNode.SelectNodes("//table")[0];
                    {
                        var heads = table.SelectSingleNode("thead");
                        if (heads != null)
                        {
                            foreach (var node in heads.SelectSingleNode("tr").SelectNodes("th"))
                            {
                                dt3.Columns.Add(node.InnerText);
                            }
                        }
                        foreach (HtmlNode row in table.SelectSingleNode("tbody").SelectNodes("tr"))
                        {

                            var dr = dt3.NewRow();
                            int i = 0;
                            foreach (HtmlNode cell in row.SelectNodes("td"))
                            {
                                dr[i++] = cell.InnerText;
                            }
                            dt3.Rows.Add(dr);
                        }
                    }
                    dt = dt3;
                }
            }
            catch
            {
                dt = null;
            }
            finally
            {
                if (stream != null && (stream.CanRead || stream.CanSeek || stream.CanWrite)) stream.Close();
                //stream.Dispose();
            }
            if (dt != null) return dt;
            throw new Exception("Cannot convert file!");
        }

        ///<summary>
        ///Method to Read CSV Format
        ///</summary>
        ///<param name="path">Read File Full Path</param>
        ///<returns></returns>

        public static DataTable ReadCSVToTable(this string path, Encoding encoding = null)
        {
            return ReadCSVToTable(File.Open(path, FileMode.Open));
        }
        public static DataTable ReadCSVToTable(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.Default;
            DataTable dt = new DataTable();
            bool isDtHasColumn = false;   //Mark if DataTable Generates Column
            StreamReader reader = new StreamReader(stream, encoding);  //Data Stream
            while (!reader.EndOfStream)
            {
                string message = reader.ReadLine();
                string[] splitResult = message.Split(new char[] { ',' }, StringSplitOptions.None);  //Read One Row and Separate by Comma, Save to Array
                DataRow row = dt.NewRow();
                for (int i = 0; i < splitResult.Length; i++)
                {
                    if (!isDtHasColumn) //If not Generate Column
                    {
                        dt.Columns.Add("column" + i, typeof(string));
                    }
                    row[i] = splitResult[i];
                }
                dt.Rows.Add(row);  //Add Row
                isDtHasColumn = true;  //Mark the Existed Column after Read the First Row, Not Generate Column after Reading Later Rows
            }
            reader.Close();
            return dt;
        }
    }
}
