using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoCompleteType = Framework.Utility.AutoCompleteType;

namespace Framework.Utility
{
  

    public class BaseAutoComplete : TextBox, IPostBackDataHandler
    {
        protected string formatItem = "function(row, i, max) {return  row.name ;}";
        protected HiddenField hf;

        protected String TempData
        {
            get { return (String) ViewState["Text2"]; }

            set { ViewState["Text2"] = value; }
        }

        //public string Value { get; set; }
        public AutoCompleteType LoadType { get; set; }

        protected string HandlerName { get; set; }
        public string LocalJSName { get; set; }
        public string LocalJSArrayName { get; set; }

        protected override void OnInit(EventArgs e)
        {
            SiteUrls.Instance.AddStylesheetToPage(Page, "autocomplete.css");
            JavaScript.SubmitJsFile(Page, SiteUrls.Instance.JavaScript("autocomplete.js"));
            if (LoadType == Utility.AutoCompleteType.Local )
            {
                JavaScript.SubmitJsFile(Page, SiteUrls.Instance.JavaScript(LocalJSName));
            }
           
            base.OnInit(e);
        }
        protected void LoadHiddenControl()  
        {
            hf = new HiddenField();
            hf.ID = "hid" + ClientID;
            Controls.Add(hf);
        }
        public new event EventHandler TextChanged;
        
        public virtual new void RaisePostDataChangedEvent()
        {
            OnTextChanged(EventArgs.Empty);
        }


        protected virtual new void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }
        protected string HiddenClientID
        {
            get
            {
                return hf.ClientID;
            }
           
        }

        protected override bool LoadPostData(string postDataKey,NameValueCollection postCollection)
        {
            String presentValue = TempData;
            String postedValue = postCollection[hf.UniqueID];
            string postedName = postCollection[this.UniqueID];
            if(string.IsNullOrEmpty(postedName))
            {
                postedValue = null;

            }
            if (presentValue == null || !presentValue.Equals(postedValue))
            {
                TempData = postedValue;
              
              
                return true;
            }

            return false;
        }

        protected void InnerRender(HtmlTextWriter writer)
        {
            string script = string.Empty;

            const string pattern = "$('#{0}').autocomplete({1}, {{autoFill: false,width: 310,formatItem: {2} }});";
            if (LoadType == Utility.AutoCompleteType.Local)
            {
                script = string.Format(pattern, ClientID, LocalJSArrayName, formatItem);
            }
            if (LoadType == Utility.AutoCompleteType.Ajax)
            {
                script = string.Format(pattern, ClientID, "'" + HandlerName + "'", formatItem);
            }
            string temp = "<script type=\"text/javascript\"> $(document).ready(function(){" + script + "});</script>";
           
            hf.RenderControl(writer);
            writer.Write(temp);
        }
    }
}

