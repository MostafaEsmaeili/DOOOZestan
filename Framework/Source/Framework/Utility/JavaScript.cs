using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Framework.Utility
{
    public class JavaScript
    {


        #region Ctors

        private JavaScript()
        {
        }

        #endregion

        #region Nested Classes

        public class JavaScriptResponseFilter : Stream
        {

            #region Field Members

            private string baseUrl;
            private long position;
            private Stream stream;
            private StreamWriter streamWriter;

            #endregion

            #region Ctors

            public JavaScriptResponseFilter(HttpContext context)
            {
                context.Response.Clear();
                context.Response.ContentType = "text/javascript";

                stream = context.Response.Filter;
                streamWriter = new StreamWriter(stream, Encoding.UTF8);

                Uri uri = context.Request.Url;
                baseUrl = uri.Scheme + "://" + uri.Host + (uri.IsDefaultPort ? "" : ":" + uri.Port.ToString());
            }

            #endregion

            #region Method Members

            public virtual string ResolveUrls(string html)
            {
                Match m = findUrls.Match(html);
                int lastIndex = 0;
                StringBuilder newHtml = new StringBuilder();

                while (m != null && m.Success)
                {
                    if (m.Index != lastIndex)
                        newHtml.Append(html.Substring(lastIndex, m.Index - lastIndex));

                    newHtml.Append(m.Groups[1].Value);
                    newHtml.Append("=\"");

                    if (m.Groups[2].Value.Substring(0, 1) == "/")
                    {
                        newHtml.Append(baseUrl);
                        newHtml.Append(m.Groups[2].Value);
                    }
                    else
                        newHtml.Append(m.Groups[2].Value);

                    newHtml.Append("\"");

                    lastIndex = m.Index + m.Length;
                    m = findUrls.Match(html, lastIndex + 1);
                }

                if (lastIndex != html.Length - 1)
                    newHtml.Append(html.Substring(lastIndex));

                return newHtml.ToString();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                MemoryStream ms = new MemoryStream(buffer, offset, count, false);
                StreamReader sr = new StreamReader(ms, Encoding.UTF8);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    streamWriter.Write("document.write(\"");
                    streamWriter.Write(Encode(ResolveUrls(s)));
                    streamWriter.Write("\");\n");
                }
                streamWriter.Flush();
            }

            #endregion

            #region Static Members

            #region Field Members

            private static Regex findUrls =
                new Regex("(href|src)=\"?([^ \"]{1,})\"?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            #endregion

            #endregion

            #region Stream overrides

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanSeek
            {
                get { return true; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override void Close()
            {
                streamWriter.Close();
                stream.Close();
            }

            public override void Flush()
            {
                streamWriter.Flush();
                stream.Flush();
            }

            public override long Length
            {
                get { return 0; }
            }

            public override long Position
            {
                get { return position; }
                set { position = value; }
            }

            public override long Seek(long offset, SeekOrigin direction)
            {
                return stream.Seek(offset, direction);
            }

            public override void SetLength(long length)
            {
                stream.SetLength(length);
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return stream.Read(buffer, offset, count);
            }

            #endregion

        }

        #endregion

        #region Static Members

        #region Method Members

        #region Encode

        public static string Encode(string text)
        {
            return
                text.Replace("\\", "\\\\").Replace("\n", "\\n").Replace("\r", "\\r").Replace("\'", "\\\'").Replace(
                    "\"", "\\\"");
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Returns the javascript code blocks to render the 3 levels of refreshes
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public static string Refresh(Uri pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.AppendFormat("\nvar sURL = '{0};'", pageUrl.ToString());
            sb.Append("\nfunction refresh() {window.location.href = sURL;}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            sb.Append("\n<script language=\"JavaScript1.1\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refresh(){window.location.replace( sURL );}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            sb.Append("\n<script language=\"JavaScript1.2\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refresh() { window.location.reload( false );}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            sb.Append("\n<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refreshCallback(res) { if(res){refresh();} }");
            sb.Append("\n//-->");
            sb.Append("\n</script>");

            return sb.ToString();
        }

        #endregion

        #region RefreshURL

        /// <summary>
        /// Returns the javascript code blocks to render the 3 levels of refreshes
        /// The URL will be supplied at run-time
        /// </summary>
        /// <returns></returns>
        public static string RefreshURL()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refreshURL(sURL) {window.location.href = sURL;}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            sb.Append("\n<script language=\"JavaScript1.1\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refreshURL(sURL){window.location.replace( sURL );}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");
            sb.Append("\n<script language=\"JavaScript1.2\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refreshURL(sURL) {window.location.replace( sURL );}");
            sb.Append("\n//-->");
            sb.Append("\n</script>");

            sb.Append("\n<script language=\"JavaScript\">");
            sb.Append("\n<!--");
            sb.Append("\nfunction refreshCallback(res) { if(res){refresh();} }");
            sb.Append("\n//-->");
            sb.Append("\n</script>");

            return sb.ToString();
        }

        #endregion

        #region RegisterRefresh

        public static void RegisterRefresh(Page page)
        {
            page.RegisterClientScriptBlock("CS.Refresh", Refresh(page.Request.Url));
        }

        #endregion

        #region RegisterRefreshURL

        /// <summary>
        /// Enables a single javascript method refresh(sURL) to be used to refresh the page
        /// </summary>
        /// <param name="page"></param>
        public static void RegisterRefreshURL(Page page)
        {
            page.RegisterClientScriptBlock("CS.RefreshURL", RefreshURL());
        }

        #endregion

        #region RegisterVariable

        public static void RegisterVariable(Page page, string varName, string varValue)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(varName))
                page.ClientScript.RegisterStartupScript(typeof(String), varName,
                                                        string.Format(
                                                            "<script type=\"text/javascript\"> var {0} ='{1}';</script>",
                                                            varName, varValue));
        }

        #endregion

        #region RenderAsJavaScript

        public static void RenderAsJavaScript(HttpContext context)
        {
            context.Response.Filter = new JavaScriptResponseFilter(context);
        }

        #endregion

        #region RunFunction

        public static void RunFunction(Page page, string functionName)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(functionName))
                page.ClientScript.RegisterStartupScript(typeof(String), functionName,
                                                        string.Format("<script  type=\"text/javascript\">{0}</script>",
                                                                      functionName));
        }

        #endregion

        #region SubmitArray

        public static void SubmitArray(Page page, string arrayName, string arrayValue)
        {
            if (!page.ClientScript.IsClientScriptBlockRegistered(arrayName))
                page.ClientScript.RegisterStartupScript(typeof(String), arrayName,
                                                        string.Format(
                                                            "<script type=\"text/javascript\"> var {0} =  new Array({1});</script>",
                                                            arrayName, arrayValue));
        }

        #endregion

        #region SubmitJsFile

        public static void SubmitJsFile(Page page, string url)
        {
            if (!page.ClientScript.IsClientScriptIncludeRegistered(url))
                page.ClientScript.RegisterClientScriptInclude(url, url);
        }

        #endregion

        #endregion

        #endregion

    }
}
