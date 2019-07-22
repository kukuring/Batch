using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kibana
{
    class Program
    {
        static void Main(string[] args)
        {
        }




        public string GetResponse(string interfaceURL, string body, int nTimeout, string Method, CookieCollection cookie, out CookieCollection outCookie)
        {
            string srcHtml = "";
            outCookie = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(interfaceURL);
                request.Method = Method;
                request.Accept = "text/xml";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = nTimeout;
                request.UserAgent = "http_communication_sample";
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.CookieContainer = new CookieContainer();
                if (cookie != null)
                {

                    request.CookieContainer.Add(cookie);
                }

                if (Method.ToUpper().Equals("POST"))
                {

                    byte[] buffer;

                    buffer = Encoding.Default.GetBytes(body);
                    request.ContentLength = buffer.Length;

                    using (Stream sendStream = request.GetRequestStream())
                    {
                        sendStream.Write(buffer, 0, buffer.Length);
                        sendStream.Close();
                    }


                }

                using (HttpWebResponse result = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream ReceiveStream = result.GetResponseStream())
                    {

                        Encoding encode = System.Text.Encoding.UTF8;
                        StreamReader sr = new StreamReader(ReceiveStream, encode);

                        srcHtml = sr.ReadToEnd();


                        sr.Close();
                        ReceiveStream.Close();
                    }
                    outCookie = result.Cookies;
                    result.Close();
                }
            }
            catch (Exception ex)
            {
                srcHtml = ex.ToString();
            }
            return srcHtml;
        }
    }
}
