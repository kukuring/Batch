using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace RequestBatch
{
    public partial class Service1 : ServiceBase
    {
        public delegate void stCallBackDelegate();

        public List<LoginInfo> loginList = null;
        private static System.Timers.Timer aTimer;
        private System.Diagnostics.EventLog eventLog1 = new System.Diagnostics.EventLog();
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //CallGoldenEvent();     
            loginList = new List<LoginInfo>();
            loginList.Add(new LoginInfo { UserID = "kukuring", UserPW = "qorhvk11", IsSuccess = true });
            loginList.Add(new LoginInfo { UserID = "onionjlove", UserPW = "dnfltjqkd1!", IsSuccess = true });
            loginList.Add(new LoginInfo { UserID = "kimjy4831", UserPW = "dnflwlq1", IsSuccess = true });
            loginList.Add(new LoginInfo { UserID = "somin059", UserPW = "somin6!@", IsSuccess = true });

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Start();

            //ScheduledTimer st = new ScheduledTimer();
            //for (int i = 0; i <= 60; i += 5)
            //{
            //    st.SetTime(new TimeSpan(10, 0, i), CallGoldenEvent);                
            //}
            
            
        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (DateTime.Now.Hour == 10 && 
            //    ((DateTime.Now.Minute == 0 && DateTime.Now.Second > 40) || (DateTime.Now.Minute == 1 && DateTime.Now.Second < 40)))
            if ((DateTime.Now.Hour == 10 && DateTime.Now.Minute == 0) || (DateTime.Now.Hour == 09 && DateTime.Now.Minute == 59))
            {
                writeLog("체크1", DateTime.Now.ToString() + "||" + string.Join(",", loginList.Select(s => s.IsSuccess).ToArray()));
                CallGoldenEvent();
            }

            if (DateTime.Now.Minute == 40 && DateTime.Now.Second < 5)
            {
                foreach (var item in loginList)
                {
                    item.IsSuccess = false;                    
                }                
                writeLog("체크2", DateTime.Now.ToString() + "||"+ string.Join(",", loginList.Select(s => s.IsSuccess).ToArray()));
            }
            
        }

        private void CallGoldenEvent()
        {
            foreach (LoginInfo item in loginList)
            {
                if (!item.IsSuccess)
                {
                    string result1 = string.Empty;
                    string result2 = string.Empty;
                    try
                    {
                        CookieCollection cookieCollection1 = new CookieCollection();
                        CookieCollection cookieCollection2 = new CookieCollection();
                        var strParam = "";
                        strParam += "MemID=" + item.UserID; //encodeURIComponent(MemID);
                        strParam += "&MemPWD=" + item.UserPW; //encodeURIComponent(MemPWD);
                        strParam += "&SaveID=" + "N"; //encodeURIComponent(SaveID);
                        strParam += "&AutoLogin=" + ""; //encodeURIComponent(AutoLogin);
                        result1 = GetResponse("https://tour.interpark.com/Exporter/ws/GateService.asmx/GetLoginInfoAutologin?time=1477968135839", strParam, 30000, "post", null, out cookieCollection1);


                        var strParam2 = "";
                        strParam2 += "?param=" + "golden"; //encodeURIComponent(MemID);
                        strParam2 += "&subParam=" + "time"; //encodeURIComponent(MemPWD);
                        strParam2 += "&Device=" + "Mobile"; //encodeURIComponent(SaveID);
                        strParam2 += "&TypeName=" + "golden"; //encodeURIComponent(AutoLogin);
                        strParam2 += "&couponMNO=" + ""; //encodeURIComponent(AutoLogin);
                                                         //strParam2 += "&reqtime=" + ""; //encodeURIComponent(AutoLogin);

                        cookieCollection1.Add(new Cookie("eventCookie", "N", "/", ".interpark.com"));                    
                        result2 = GetResponse("http://tour.interpark.com/event/web/201608_GoldenTime/main.aspx" + strParam2, strParam2, 30000, "get", cookieCollection1, out cookieCollection2);

                        if (!(result2.Contains("아이포인트_100") || result2.Contains("꽝") || result2.Contains("goldenError")))
                        {
                            item.IsSuccess = true;
                        }

                        writeLog(item.UserID, $"{DateTime.Now.ToString()} \r\n {result1} \r\n {result2}");
                    }
                    catch (Exception ex)
                    {
                        writeLog(item.UserID, $"{ex.Message} \r\n {result1} \r\n {result2} \r\n {ex.StackTrace}");
                    }
                }
                else
                {
                    writeLog(item.UserID, $"{DateTime.Now.ToString()} \r\n {item.IsSuccess}");
                }
            }
        }

        protected override void OnStop()
        {
        }

        public void writeLog(string key, string value)
        {
            if (!System.Diagnostics.EventLog.SourceExists("RequestBatch"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "RequestBatch", "RequestBatch");
            }
            eventLog1.Source = "RequestBatch";
            eventLog1.WriteEntry($"{key} {value}", System.Diagnostics.EventLogEntryType.Information);
        }


        

        public string GetResponse(string interfaceURL, string body, int nTimeout, string Method, CookieCollection cookie, out CookieCollection outCookie)
        {
            string srcHtml = "";
            outCookie = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(interfaceURL);
                request.Method = Method;
                request.Accept = "application/xml";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = nTimeout;
                request.UserAgent = "Interpark/1.0;interparktourcheckinnow";
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

        public class ScheduledTimer
        {
            private System.Threading.Timer _timer;
            public ScheduledTimer() { }
            public static TimeSpan GetDueTime(TimeSpan A, TimeSpan B)
            {
                if (A < B)
                {
                    return B.Subtract(A);
                }
                else
                {
                    return new TimeSpan(24, 0, 0).Subtract(B.Subtract(A));
                }
            }
            public void SetTime(TimeSpan _time, stCallBackDelegate callback)
            {
                if (this._timer != null)
                {
                    // Change 매서드 사용 가능.
                    this._timer = null;
                }
                TimeSpan Now = DateTime.Now.TimeOfDay;
                TimeSpan DueTime = GetDueTime(Now, _time);

                this._timer = new System.Threading.Timer(new TimerCallback(delegate (object _callback)
                {
                    ((stCallBackDelegate)_callback)();
                }), callback, DueTime, new TimeSpan(24, 0, 0));
            }
        }
    }

    public class LoginInfo
    {
        public string UserID { get; set; }

        public string UserPW { get; set; }

        public bool IsSuccess { get; set; }
    }

    
}
