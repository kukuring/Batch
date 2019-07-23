using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkTimeCheckAlarmService
{
    public class Util
    {
        private EventLog eventLog = new EventLog();

        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSSendMessage(IntPtr hServer, [MarshalAs(UnmanagedType.I4)] int SessionId, String pTitle, [MarshalAs(UnmanagedType.U4)] int TitleLength, String pMessage, [MarshalAs(UnmanagedType.U4)] int MessageLength, [MarshalAs(UnmanagedType.U4)] int Style, [MarshalAs(UnmanagedType.U4)] int Timeout, [MarshalAs(UnmanagedType.U4)] out int pResponse, bool bWait);

        /// <summary>
        /// 메세지 박스 출력
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">메세지</param>
        /// <param name="log">이벤트 로그 기록 여부</param>
        /// <returns></returns>
        public int MessageBox(string title, string message, int style = 4, bool log = true)
        {
            bool result = false;
            int tlen = title.Length;
            int mlen = message.Length;
            int resp = 7;
            try
            {


                Task task = Task.Run(() => SystemSounds.Question.Play());
                result = WTSSendMessage(IntPtr.Zero, 1, title, tlen, message, mlen, style, 50, out resp, true);

                int err = Marshal.GetLastWin32Error();
                if (log)
                {
                    WriteLog(title, message);
                }
            }
            catch (Exception ex)
            {
                WriteLog(title, ex.ToString());
            }
            return resp;
        }

        /// <summary>
        /// 이벤트 로그 작성
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteLog(string key, string value)
        {
            if (!System.Diagnostics.EventLog.SourceExists("출퇴근체크"))
            {
                System.Diagnostics.EventLog.CreateEventSource("출퇴근체크", "출퇴근체크");
            }
            eventLog.Source = "출퇴근체크";
            eventLog.WriteEntry($"{key} {value}", System.Diagnostics.EventLogEntryType.Information);
        }


        public void CallBeep()
        {
            for (int i = 0; i < 3; i++)
            {
                SystemSounds.Asterisk.Play();
                Thread.Sleep(400);
                SystemSounds.Beep.Play();
                Thread.Sleep(400);
                SystemSounds.Exclamation.Play();
                Thread.Sleep(400);
                SystemSounds.Hand.Play();
                Thread.Sleep(400);
                SystemSounds.Question.Play();
                Thread.Sleep(400);
            }
        }


        public string GetResponse(string interfaceURL, string body, int nTimeout, string Method, WebHeaderCollection headers, CookieCollection cookie, out CookieCollection outCookie)
        {
            string srcHtml = "";
            outCookie = null;
            //return string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(interfaceURL);
                request.Method = Method;
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = nTimeout;
                request.Proxy = null;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.Headers.Add("Accept-Language", "ko-KR");
                //request.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)");
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";
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
            finally
            {
                WriteLog("통신로그", $"url:{interfaceURL} || result: {srcHtml}");
            }
            return srcHtml;
        }
    }
}
