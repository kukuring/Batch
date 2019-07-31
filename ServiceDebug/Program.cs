using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ServiceDebug
{

    class Program
    {
        private static CheckOption checkOption;
        private static EventLog eventLog = new EventLog();
        [STAThread]
        static void Main(string[] args)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    SystemSounds.Asterisk.Play();
            //    Thread.Sleep(400);
            //    SystemSounds.Beep.Play();
            //    Thread.Sleep(400);
            //    SystemSounds.Exclamation.Play();
            //    Thread.Sleep(400);
            //    SystemSounds.Hand.Play();
            //    Thread.Sleep(400);
            //    SystemSounds.Question.Play();
            //    Thread.Sleep(400);
            //}

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(MessageBox("테스트", i.ToString(), i).ToString());
            //}


            //Console.WriteLine(setEncId("N13220"));
            //Console.WriteLine(setEncId("N16273"));


            SetCheckOption();
            WriteCheckOption();

            //// 공휴일 체크
            //if (checkOption.holiday != null)
            //{                
            //    foreach (DateTime item in checkOption.holiday)
            //    {                    
            //        if (item < DateTime.Now && DateTime.Now < item.AddDays(1))
            //        {
            //            Console.WriteLine(item);
            //        }
            //    }
            //    Console.WriteLine(true);
            //}

            //WriteLog("출근체크", $"테스트로그찍기: \r\n {JsonConvert.SerializeObject(checkOption, Formatting.Indented)}");

        }

        private static void WriteCheckOption()
        {
            CheckOptionSetting setting = new CheckOptionSetting()
            {
                checkInTime = checkOption.checkInTime,
                checkOutTime = checkOption.checkOutTime,
                id = checkOption.id,
                useCheckInAutoCall = checkOption.useCheckInAutoCall,
                holiday = checkOption.holiday,
                myHoliday = checkOption.myHoliday
            };
            
            using (StreamWriter sWriter = new StreamWriter(@"D:\Work\source\Batch\WorkTimeCheckAlarmService\WorkTimeCheckAlarmSetting2.json", false, Encoding.GetEncoding("euc-kr")))
            {
                sWriter.WriteLine(JsonConvert.SerializeObject(setting, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace("\n      \"", " \"").Replace("\n    }", " }"));
            }
        }

        public static void WriteLog(string key, string value)
        {
            if (!System.Diagnostics.EventLog.SourceExists("출퇴근체크"))
            {
                System.Diagnostics.EventLog.CreateEventSource("출퇴근체크", "출퇴근체크");
            }
            eventLog.Source = "출퇴근체크";
            eventLog.WriteEntry($"{key} {value}", System.Diagnostics.EventLogEntryType.Information);
        }


        public static string setEncId(string id)
        {
            string result = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(id);
            result = Convert.ToBase64String(bytes);
            bytes = Encoding.Default.GetBytes(result);
            result = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return result.Replace("-", "").ToLower();
        }


        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSSendMessage(IntPtr hServer, [MarshalAs(UnmanagedType.I4)] int SessionId, String pTitle, [MarshalAs(UnmanagedType.U4)] int TitleLength, String pMessage, [MarshalAs(UnmanagedType.U4)] int MessageLength, [MarshalAs(UnmanagedType.U4)] int Style, [MarshalAs(UnmanagedType.U4)] int Timeout, [MarshalAs(UnmanagedType.U4)] out int pResponse, bool bWait);



        enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType,
            WTSIdleTime,
            WTSLogonTime,
            WTSIncomingBytes,
            WTSOutgoingBytes,
            WTSIncomingFrames,
            WTSOutgoingFrames,
            WTSClientInfo,
            WTSSessionInfo
        };

        [DllImport("Wtsapi32.dll", SetLastError = true)]
        static extern bool WTSQuerySessionInformation(
            IntPtr hServer,
            uint sessionId,
            WTS_INFO_CLASS wtsInfoClass,
            out IntPtr ppBuffer,
            out uint pBytesReturned
        );




        /// <summary>
        /// 메세지 박스 출력
        /// </summary>
        /// <param name="title">제목</param>
        /// <param name="message">메세지</param>
        /// <param name="log">이벤트 로그 기록 여부</param>
        /// <returns></returns>
        public static int MessageBox(string title, string message, int thema)
        {
            bool result = false;
            int tlen = title.Length;
            int mlen = message.Length;
            int resp = 7;
            try
            {
                result = WTSSendMessage(IntPtr.Zero, 1, title, tlen, message, mlen, thema, 50, out resp, true);
                int err = Marshal.GetLastWin32Error();
            }
            catch (Exception ex)
            {

            }
            return resp;
        }


        /// <summary>
        /// 옵션 설정값 가져오기
        /// </summary>
        public static void SetCheckOption()
        {
            //using (StreamReader file = File.OpenText(@"D:\Work\source\Batch\WorkTimeCheckAlarmService\WorkTimeCheckAlarmSetting.json"))
            //{
            //    using (JsonTextReader reader = new JsonTextReader(file))
            //    {
            //        JObject o2 = (JObject)JToken.ReadFrom(reader);
            //        checkOption = o2.ToObject<CheckOption>();
            //    }
            //}
            using (FileStream fStream = new FileStream(@"D:\Work\source\Batch\WorkTimeCheckAlarmService\WorkTimeCheckAlarmSetting.json", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sReader = new StreamReader(fStream, Encoding.GetEncoding("euc-kr"), true))
                {
                    //byte[] euckrByte = Encoding.Default.GetBytes(sReader.ReadToEnd());
                    //string g = Encoding.UTF8.GetString(euckrByte);
                    //checkOption = JsonConvert.DeserializeObject<CheckOption>(g);
                    using (JsonTextReader reader = new JsonTextReader(sReader))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        checkOption = o2.ToObject<CheckOption>();
                    }
                }
            }            
            
        }
    }


    /// <summary>
    /// 체크 옵션
    /// </summary>
    public class CheckOption : CheckOptionSetting
    {
        /// <summary>
        /// 랜덤 출근 시간
        /// </summary>
        public int addMinute { get; set; }

        /// <summary>
        /// 사번(암호화값)
        /// </summary>
        public string cdSabn { get; set; }
        /// <summary>
        /// 출근체크 여부
        /// </summary>
        public bool isCheckInCall { get; set; }

        /// <summary>
        /// 퇴근체크 여부
        /// </summary>
        public bool isCheckOutCall { get; set; }

        /// <summary>
        /// 출근체크하는날 여부
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public MyHolidayType holidayType { get; set; } = MyHolidayType.NOT;
    }

    public class CheckOptionSetting
    {
        /// <summary>
        /// 출근시간
        /// </summary>
        public string checkInTime { get; set; } = "08:00";

        /// <summary>
        /// 퇴근시간
        /// </summary>
        public string checkOutTime { get; set; } = "19:00";

        /// <summary>
        /// 사번
        /// </summary>
        public string id { get; set; }


        /// <summary>
        /// PC 사용여부 (로그온 여부로 감지)
        /// </summary>
        public bool useDesktop { get; set; }

        /// <summary>
        /// true: PC 사용여부 상관없이 주말을 제외하고 자동으로 출근체크 함. 
        /// false: PC 사용여부를 체크하여 출근체크 로직이 작동함.
        /// </summary>
        public bool useCheckInAutoCall { get; set; }

        /// <summary>
        /// 쉬는날
        /// </summary>                
        public List<Holiday> myHoliday { get; set; }

        /// <summary>
        /// 공휴일
        /// </summary>
        public List<Holiday> holiday { get; set; }

    }

    public class Holiday
    {
        public DateTime date { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MyHolidayType type { get; set; } = MyHolidayType.ALL;

        public string name { get; set; }


    }

    public enum MyHolidayType
    {
        /// <summary>
        /// 휴일아님
        /// </summary>        
        NOT = 0,
        /// <summary>
        /// 연차
        /// </summary>        
        ALL = 1,
        /// <summary>
        /// 오전반차
        /// </summary>        
        AM = 2,
        /// <summary>
        /// 오후반차
        /// </summary>        
        PM = 3
    }




}