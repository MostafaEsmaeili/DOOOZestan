using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace Framework.Utility
{
    public abstract class BaseHttpHandler :IHttpHandler
    {
        #region Implementation of IHttpHandler

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.UrlReferrer != null && (context.Request.UrlReferrer.IsWellFormedOriginalString() &&
                                                        context.Request.UrlReferrer.Host.Equals(context.Request.Url.Host)))
            {
                Context = context;
                this.RunMethod(context);
            }
            else
            {
                return;
            }
        }
        private HttpContext Context;

        public abstract void RunMethod(HttpContext context);
        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
        /// </returns>
        public bool IsReusable
        {
            get { return true; }
        }

        public Dictionary<string, object> DeSerialize(HttpContext context)
        {
            string temp = context.Request.QueryString[0];
            var serializer = new JavaScriptSerializer();
            object obj = serializer.DeserializeObject(temp);
            var items = (Dictionary<string, object>)obj;
            return items;
        }

        protected string Write<T>(T data) where T : class
        {
            //data = HttpUtility.HtmlDecode(data);
            Context.Response.Clear();
            Context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Context.Response.ContentType = "text/html";
            Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Context.Response.Cache.SetLastModified(Feed.LastModified);
            //Context.Response.Cache.SetETag(Feed.Etag);
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(data);

        }
        #endregion
    }
}
