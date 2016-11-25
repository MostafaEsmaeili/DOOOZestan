using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Framework.Crawler
{
    public class WebBrowser
    {
        WebBrowser()
        {
            
        }

        public static string Browse(string url, string post, BrowserType browserType, Encoding encoding, out string status)
        {
            try
            {
                switch (browserType)
                {
                    case BrowserType.Ajax:
                        return WebResponse(url, post, encoding, out status);

                    case BrowserType.Normal:
                        status = string.Empty;
                        return WebClient(url, encoding);
                    case BrowserType.Gzip:
                        status = string.Empty;
                        return HttpWebRequest(url, post, true);
                    case BrowserType.NormalWithCookieSetup :
                        status = string.Empty;
                        return GetHtml(url);
                    default:
                        status = string.Empty;
                        return string.Empty;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string GetHtml(string urlAddr)
        {
            if (urlAddr == null || string.IsNullOrEmpty(urlAddr))
            {
                throw new ArgumentNullException("urlAddr");
            }
            else
            {
                string result;

                //1.Create the request object
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddr);
                //request.AllowAutoRedirect = true;
                //request.MaximumAutomaticRedirections = 200;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                request.Proxy = null;
                request.UseDefaultCredentials = true;

                //2.Add the container with the active
                CookieContainer cc = new CookieContainer();


                //3.Must assing a cookie container for the request to pull the cookies
                request.CookieContainer = cc;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    //Close and clean up the StreamReader
                    sr.Close();
                }
                return result;
            }
        }

        public static string WebClient(string url, Encoding encoding)
        {
            WebClient client = new WebClient();
            byte[] bytes = client.DownloadData(url);
            return encoding.GetString(bytes);

        }
        private static string HttpWebRequest(string url, string postData, bool GZip)
        {
            Stream responseStream = null;
            HttpWebResponse webResponse = null;
            try
            {
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(url);

                if (GZip)
                    http.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                if (!string.IsNullOrEmpty(postData))
                {
                    http.Method = "POST";
                    byte[] lbPostBuffer = Encoding.Default.GetBytes(postData);

                    http.ContentLength = lbPostBuffer.Length;

                    Stream postStream = http.GetRequestStream();
                    postStream.Write(lbPostBuffer, 0, lbPostBuffer.Length);
                    postStream.Close();
                }

                webResponse = (HttpWebResponse)http.GetResponse();

                responseStream = webResponse.GetResponseStream();
                if (webResponse.ContentEncoding.ToLower().Contains("gzip"))
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                else if (webResponse.ContentEncoding.ToLower().Contains("deflate"))
                    responseStream = new DeflateStream(responseStream, CompressionMode.Decompress);

                if (responseStream != null)
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (webResponse != null)
                {
                    webResponse.Close();
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream.Dispose();
                }
            }
            return null;
        }
        private static string WebResponse(string url, string post, Encoding encoding, out string status)
        {
            Stream dataStream = null;
            StreamReader reader = null;
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = encoding.GetBytes(post);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                if (dataStream != null) reader = new StreamReader(dataStream);
                status = ((HttpWebResponse)response).StatusDescription;
                if (reader != null) return reader.ReadToEnd();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
                if (dataStream != null)
                {
                    dataStream.Close();
                    dataStream.Dispose();
                }
            }

            return null;
        }
    }
}
