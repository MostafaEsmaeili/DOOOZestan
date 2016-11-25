using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Framework.Globals
{
    public class HtmlXml
    {
      

        public static TextReader GetTextReader(string xmlData)
        {
            TextReader stringReader = new StringReader(xmlData);
            return stringReader;
        }

        public static string GetHtml(string xslPath, XmlReader xmlData)
        {
            return GetHtml(xslPath, null, xmlData);
        }

        public static string GetHtml(string xslPath, TextReader xmlData)
        {
            return GetHtml(xslPath, xmlData, false);
        }

        public static string GetHtml(string xslPath, TextReader xmlData, bool processHtmlNewLine)
        {
            return GetHtml(xslPath, null, xmlData, processHtmlNewLine);
        }

        public static string GetHtml(string xslPath, XsltArgumentList arg, TextReader xmlData)
        {
            return GetHtml(xslPath, arg, xmlData, false);
        }

        public static string GetHtml(string xslPath, XsltArgumentList arg, TextReader xmlData, bool processHtmlNewLine)
        {
            try
            {
                if (xmlData == null)
                    return string.Empty;
                StringBuilder sb = new StringBuilder();
                StringWriter HtmlResult = new StringWriter(sb);

                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xslPath);
                XPathDocument xpathDoc = new XPathDocument(xmlData);
                transform.Transform(xpathDoc, arg, HtmlResult);
                if (processHtmlNewLine)
                    return sb.ToString().Replace("\n", "<br/>");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //EventLogs.Debug(ex.Message, "GetHtml", -5001); TODO LOG
                return null;
            }
        }

        public static string GetHtml(string xslPath, XsltArgumentList arg, XmlReader xmlData)
        {
            try
            {
                if (xmlData == null)
                    return string.Empty;
                StringBuilder sb = new StringBuilder();
                StringWriter HtmlResult = new StringWriter(sb);

                XslCompiledTransform transform = new XslCompiledTransform();
                //XslTransform transform = new XslTransform();
                transform.Load(xslPath);
                XPathDocument xpathDoc = new XPathDocument(xmlData);
                transform.Transform(xpathDoc, arg, HtmlResult);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //EventLogs.Warn(ex.Message, "GetHtml", -5002); TODO LOG
                return null;
            }
        }

        public static string GetHtml(string xslPath, XsltArgumentList arg, XPathDocument xmlData)
        {
            try
            {
                if (xmlData == null)
                    return string.Empty;
                StringBuilder sb = new StringBuilder();
                StringWriter HtmlResult = new StringWriter(sb);
                XslCompiledTransform transform = new XslCompiledTransform();
                transform.Load(xslPath);
                transform.Transform(xmlData, arg, HtmlResult);
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //EventLogs.Warn(ex.Message, "GetHtml", -5002); TODO LOG
                return null;
            }
        }

        public static string GetHtml(string xslPath, XsltArgumentList arg, string xmlData)
        {
            TextReader stringReader = new StringReader(xmlData);
            return GetHtml(xslPath, arg, stringReader);
        }

        public static string GetHtml(string xslPath, string xmlData)
        {
            TextReader stringReader = new StringReader(xmlData);
            return GetHtml(xslPath, null, stringReader);
        }

        public static byte[] GetWord(string xmlData, XsltArgumentList args, string xsltPath)
        {
            TextReader stringReader = new StringReader(xmlData);
            XslCompiledTransform xslt = new XslCompiledTransform();

            using (MemoryStream swResult = new MemoryStream())
            {
                XPathDocument xpathDoc = new XPathDocument(stringReader);
                xslt.Load(xsltPath);
                xslt.Transform(xpathDoc, args, swResult);

                return swResult.ToArray();
            }
        }

        public static byte[] GetWord(XmlReader xmlData, XsltArgumentList args, string xsltPath)
        {
            // Initialize needed variables
            XslCompiledTransform xslt = new XslCompiledTransform();

            using (MemoryStream swResult = new MemoryStream())
            {
                // Load XSLT to reader and perform transformation
                xslt.Load(xsltPath);
                xslt.Transform(xmlData, args, swResult);

                return swResult.ToArray();
            }
        }


    }
    
}
