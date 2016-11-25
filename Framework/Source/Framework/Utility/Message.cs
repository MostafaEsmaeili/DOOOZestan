using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Xml;
using Framework.Globals;

namespace Framework.Utility
{
    public class Message
    {

        #region Field Members

        #region Variable Members

        private string body;
        private NameValueCollection extraData = null;
        private int messageID = -1;
        private string name = "";
        private bool processHtmlNewLine = false;
        private int state;
        private string title;
        private string xslPath = "";

        #endregion

        #endregion

        #region Ctors

        public Message(XmlNode node)
        {
            messageID = int.Parse(node.Attributes["id"].Value);
            title = node.SelectSingleNode("title").InnerText;
            body = node.SelectSingleNode("body").InnerText;
            extraData = new NameValueCollection();
            if (node.Attributes["xslFilePath"] != null)
            {
                xslPath = node.Attributes["xslFilePath"].InnerText;
            }
            name = node.Attributes["Name"].Value;

            if (node.Attributes["State"] != null)
            {
                state = Convert.ToInt32(node.Attributes["State"].Value);
            }
            if (node.Attributes["processHtmlNewLine"] != null)
            {
                processHtmlNewLine = Convert.ToBoolean(node.Attributes["processHtmlNewLine"].Value);
            }
        }

        #endregion

        #region Method Members

        #region ToHtml
        public override string ToString()
        {
            return this.Title + "-" + this.body;
        }

        public string ToHtml()
        {
            if (xslPath == "")
                return body;
            return ToHtml(xslPath);
        }

        public string ToHtml(string xslPath)
        {
            if (xslPath == "")
                return "";
            return HtmlXml.GetHtml(XslPath(xslPath), ToXml(), processHtmlNewLine);
        }

        private static string XslPath(string xslPath)
        {
            return xslPath;
        }

        #endregion

        #region ToXml

        public TextReader ToXml()
        {
            return ToXml(this);
        }

        public TextReader ToXml(Message msg)
        {
            StringBuilder SB = new StringBuilder();
            StringWriter SW = new StringWriter(SB);
            HtmlTextWriter W = new HtmlTextWriter(SW);
            W.WriteFullBeginTag("Message");

            W.WriteFullBeginTag("Body");
            W.Write(msg.Body);
            W.WriteEndTag("Body");

            W.WriteFullBeginTag("messageID");
            W.Write(msg.messageID);
            W.WriteEndTag("messageID");

            W.WriteFullBeginTag("Title");
            W.Write(msg.Title);
            W.WriteEndTag("Title");

            W.WriteEndTag("Message");
            TextReader stringReader = new StringReader(SW.ToString());
            return stringReader;
        }

        #endregion

        #endregion

        #region Public Property

        public bool ProcessHtmlNewLine
        {
            get { return processHtmlNewLine; }
            set { processHtmlNewLine = value; }
        }

        public int MessageID
        {
            get { return messageID; }
        }

        public int State
        {
            get { return state; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string HTML
        {
            get
            {
                if (xslPath == "")
                    return title;
                string html = HtmlXml.GetHtml(XslPath(xslPath), ToXml(), processHtmlNewLine);
                //object[] parameter = MFContext.Current.Parameters; 
                //if (parameter != null)
                //{

                //    html = string.Format(html, parameter);

                //}
                return html;
            }
        }

        public void SetExtraData(string key, string value)
        {
            if (extraData[key] == null)
            {
                extraData.Add(key, value);
            }
            else
            {
                extraData[key] = value;
            }
        }

        public NameValueCollection ExtraData
        {
            get { return extraData; }
        }

        #endregion

    }
}
