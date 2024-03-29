﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;



namespace WorkTimeCheckAlarmService
{
    public partial class WorkTimeCheckAlarm : ServiceBase
    {
        private Util util = new Util();
        private System.Timers.Timer timer;
        private FileSystemWatcher fileWatcher;
        private CheckOption checkOption = new CheckOption();
        private CookieCollection outCookie = new CookieCollection();
        private readonly string checkInOutUrl = "http://iportal.interpark.com/api/common/emp/getWorkTimeTag.do?IP=116.126.127.12";
        private readonly string settingFilePath = @"C:\Program Files\Inpark\WorkTimeCheckAlram\";
        private readonly string settingFileName = "WorkTimeCheckAlarmSetting.json";


        public WorkTimeCheckAlarm()
        {
            InitializeComponent();
            CanHandleSessionChangeEvent = true;
        }

        protected override void OnStart(string[] args)
        {
            util.WriteLog("출근체크", "프로그램 시작1");
            SetCheckOption();

            timer = new System.Timers.Timer();
            timer.Interval = 60 * 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            SettingFileWatcher();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            util.WriteLog("출근체크", $"OnSessionChange: {JsonConvert.SerializeObject(changeDescription, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");

            switch (changeDescription.Reason)
            {
                case SessionChangeReason.SessionLogon:
                case SessionChangeReason.SessionUnlock:
                    checkOption.useDesktop = true;
                    if (!checkOption.isCheckInCall)
                    {
                        CheckIn();
                    }
                    break;
                case SessionChangeReason.SessionLogoff:
                case SessionChangeReason.SessionLock:
                    if (checkOption.isCheckInCall && Convert.ToDateTime(checkOption.checkOutTime) < DateTime.Now)
                    {
                        CheckOut(false);
                    }
                    break;
                default:
                    break;
            }

            base.OnSessionChange(changeDescription);
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WorkTimeCheck();
        }

        /// <summary>
        /// 출퇴근 체크
        /// </summary>
        private void WorkTimeCheck()
        {
            DateTime checkInTime = Convert.ToDateTime(checkOption.checkInTime);
            DateTime checkOutTime = Convert.ToDateTime(checkOption.checkOutTime);

            #region 로직체크
            // 초기화(매일 새벽 1시)
            if (DateTime.Now.Hour == 1 && DateTime.Now.Minute < 2)
            {
                Random rd = new Random();
                checkOption.useDesktop = false;
                checkOption.isCheckInCall = false;
                checkOption.isCheckOutCall = false;
                checkOption.addMinute = rd.Next(0, 20) * -1;
                checkOption.holidayType = CheckHolidayType(DateTime.Now);
                util.WriteLog("출근체크", $"초기화 했음. CheckOption: \r\n{JsonConvert.SerializeObject(checkOption, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");
            }

            // 정각마다 작동여부 로그 남기기
            if (DateTime.Now.Minute == 0)
            {
                util.WriteLog("출근체크", $"정각 로그. CheckOption: \r\n{JsonConvert.SerializeObject(checkOption, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");
            }

            // 출근하는날이 아니면 체크 안함.            
            switch (checkOption.holidayType)
            {
                case MyHolidayType.ALL:
                    return;                    
                case MyHolidayType.AM:
                    checkInTime = checkInTime.AddHours(5); // 8시: 13시, 9시: 14시, 10시: 15시
                    break;
                case MyHolidayType.PM:
                    if (checkInTime < Convert.ToDateTime("09:00"))
                    {
                        checkOutTime = checkOutTime.AddHours(-5); // 17시: 12시
                    }
                    else
                    {
                        checkOutTime = checkOutTime.AddHours(-4); // 18시: 14시, 19시: 15시
                    }
                    break;
                case MyHolidayType.NOT:
                    break;
                default:
                    break;
            }

            #endregion

            #region 출근체크
            try
            {
                // 출근체크 시간: 07:30 <= 07:35 && 07:35 <= 08:00
                if (checkInTime.AddMinutes(-30) <= DateTime.Now && DateTime.Now <= checkInTime.AddMinutes(5))
                {
                    // 출근체크 한번 했으면 출근체크 안함.
                    if (checkOption.isCheckInCall)
                    {
                        return;
                    }

                    if (checkOption.useCheckInAutoCall)
                    {
                        // 랜덤 시간(출근20분전 ~ 출근시간 ex: 07:40~08:00)
                        checkInTime = checkInTime.AddMinutes(checkOption.addMinute);
                        if (checkInTime.Hour == DateTime.Now.Hour && checkInTime.Minute == DateTime.Now.Minute)
                        {
                            CheckIn();
                        }
                    }
                    else
                    {
                        // PC 사용 체크면 PC로그온 체크
                        if (checkOption.useDesktop)
                        {
                            CheckIn();

                            // 3분전부터 비프음
                            if (checkInTime.AddMinutes(-3) <= DateTime.Now)
                            {
                                util.CallBeep();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                util.MessageBox("출근체크", $"오류가 발생됐습니다. 이벤트로그 확인해주세요.\r\n{ex.ToString()}", 1);
            }
            #endregion

            #region 퇴근체크
            try
            {
                // 퇴근체크시간 10분후에 자동 퇴근체크함.
                if (DateTime.Now.Hour == checkOutTime.Hour && DateTime.Now.Minute == checkOutTime.Minute)
                {
                    // 출근체크를 했고 퇴근체크 한번 했으면 더이상 안함.
                    if (checkOption.isCheckInCall && !checkOption.isCheckOutCall)
                    {
                        CheckOut();
                    }
                }
            }
            catch (Exception ex)
            {
                util.MessageBox("퇴근체크", $"오류가 발생됐습니다. 이벤트로그 확인해주세요.\r\n{ex.ToString()}", 1);
            }
            #endregion
        }


        /// <summary>
        /// 근무일 체크
        /// </summary>
        /// <returns></returns>
        private MyHolidayType CheckHolidayType(DateTime nowDate, bool log = true)
        {
            // 주말에는 체크 안함
            if (nowDate.DayOfWeek == DayOfWeek.Saturday || nowDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return MyHolidayType.ALL;
            }

            // 공휴일 체크
            if (checkOption.holiday != null)
            {
                foreach (Holiday item in checkOption.holiday)
                {
                    if (item.date < nowDate && nowDate < item.date.AddDays(1))
                    {
                        if (log)
                        {
                            util.WriteLog("출근체크", $"공휴일!. CheckOption: \r\n{JsonConvert.SerializeObject(checkOption, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");
                        }
                        return MyHolidayType.ALL;
                    }
                }
            }


            // 개인 휴일 체크 안함
            if (checkOption.myHoliday != null)
            {
                foreach (Holiday item in checkOption.myHoliday)
                {
                    if (item.date < nowDate && nowDate < item.date.AddDays(1))
                    {
                        if (log)
                        {
                            util.WriteLog("출근체크", $"쉬는날{item.type.ToString("f")}!. CheckOption: \r\n{JsonConvert.SerializeObject(checkOption, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");
                        }
                        return item.type;
                    }
                }
            }

            return MyHolidayType.NOT;
        }


        /// <summary>
        /// 출근체크
        /// </summary>
        private void CheckIn()
        {
            string result = string.Empty;
            if (checkOption.useCheckInAutoCall || (util.MessageBox("출근체크", "출근체크 하시겠습니까?", 4, false) == 6))
            {
                result = util.GetResponse($"{checkInOutUrl}&cdSabn={checkOption.cdSabn}&fgGubn=On", string.Empty, 10000, "GET", null, null, out outCookie);
            }

            ResultInfo resultInfo = JsonConvert.DeserializeObject<ResultInfo>(result);
            if (resultInfo.code == 200 || resultInfo.message == "OK")
            {
                checkOption.isCheckInCall = true;
                util.MessageBox("출근체크", "출근체크 완료", 0);
            }
            else if (resultInfo.code == 204 || resultInfo.message == "NO_CONTENT")
            {
                checkOption.isCheckInCall = true;
                util.MessageBox("출근체크", "이미 출근체크 함", 0);
            }
            else
            {
                util.MessageBox("출근체크", result, 0);
            }
        }

        /// <summary>
        /// 퇴근체크
        /// </summary>        
        private void CheckOut(bool useMessage = true)
        {
            string result = util.GetResponse($"{checkInOutUrl}&cdSabn={checkOption.cdSabn}&fgGubn=Off", string.Empty, 10000, "GET", null, null, out outCookie);
            ResultInfo resultInfo = JsonConvert.DeserializeObject<ResultInfo>(result);
            if (resultInfo.code == 200 || resultInfo.message == "OK" || resultInfo.code == 204 || resultInfo.message == "NO_CONTENT")
            {
                checkOption.isCheckOutCall = true;
                if (useMessage)
                {
                    DateTime tomorrow = DateTime.Now.AddDays(1);
                    MyHolidayType tomorrowHolidayType = CheckHolidayType(tomorrow, false);
                    if (tomorrowHolidayType != MyHolidayType.NOT)
                    {
                        util.MessageBox("퇴근체크", $"퇴근체크가 완료되었습니다. \r\n\r\n 내일 {tomorrowHolidayType.ToString("f")} 에입니다.", 0);
                    }
                    else
                    {
                        if (util.MessageBox("퇴근체크", "퇴근체크는 되었습니다.\r\n\r\n 내일 연차 또는 반차입니까?", 4, false) == 6)
                        {
                            int tomorrowCheck = util.MessageBox("퇴근체크", "내일 연차입니까?", 3, false);

                            if (tomorrowCheck == 6)
                            {
                                checkOption.myHoliday.Add(new Holiday { date = tomorrow, type = MyHolidayType.ALL, name = "" });
                                WriteCheckOption();
                                return;
                            }
                            else if (tomorrowCheck == 2)
                            {
                                return;
                            }

                            tomorrowCheck = util.MessageBox("퇴근체크", "내일 오전반차입니까?", 3, false);
                            if (tomorrowCheck == 6)
                            {
                                checkOption.myHoliday.Add(new Holiday { date = tomorrow, type = MyHolidayType.AM, name = "" });
                                WriteCheckOption();
                                return;
                            }
                            else if (tomorrowCheck == 2)
                            {
                                return;
                            }

                            tomorrowCheck = util.MessageBox("퇴근체크", "내일 오후반차입니까?", 3, false);
                            if (tomorrowCheck == 6)
                            {
                                checkOption.myHoliday.Add(new Holiday { date = tomorrow, type = MyHolidayType.PM, name = "" });
                                WriteCheckOption();
                                return;
                            }
                            else if (tomorrowCheck == 2)
                            {
                                return;
                            }
                        }
                    }
                }
                else
                {
                    util.WriteLog("퇴근체크", "퇴근체크 완료");
                }
            }
            else
            {
                util.MessageBox("퇴근체크", result, 0);
            }
        }


        /// <summary>
        /// 옵션 설정값 저장
        /// </summary>
        private void WriteCheckOption()
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

            using (StreamWriter sWriter = new StreamWriter(settingFilePath + settingFileName, false, Encoding.GetEncoding("euc-kr")))
            {
                string settingText = JsonConvert.SerializeObject(setting, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" }).Replace("\n      \"", " \"").Replace("\n    }", " }");
                sWriter.WriteLine(settingText);
                util.MessageBox("출근체크", $"설정 파일이 재 셋팅되었습니다.", 0);
            }
        }

        /// <summary>
        /// 옵션 설정값 가져오기
        /// </summary>
        private void SetCheckOption()
        {
            using (FileStream fStream = new FileStream(settingFilePath + settingFileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sReader = new StreamReader(fStream, Encoding.GetEncoding("euc-kr"), true))
                {
                    using (JsonTextReader reader = new JsonTextReader(sReader))
                    {
                        JObject o2 = (JObject)JToken.ReadFrom(reader);
                        checkOption = o2.ToObject<CheckOption>();
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(checkOption.id))
            {
                util.MessageBox("출근체크", "사번을 입력해주세요.", 0, false);
            }

            DateTime checkDt = new DateTime();
            if (!DateTime.TryParse(checkOption.checkInTime, out checkDt))
            {
                util.MessageBox("출근체크", "출근시간이 올바르지 않습니다.", 0, false);
            }

            if (!DateTime.TryParse(checkOption.checkOutTime, out checkDt))
            {
                util.MessageBox("출근체크", "퇴근시간이 올바르지 않습니다.", 0, false);
            }

            // 출근시간 이후 서비스 시작 대비
            if (Convert.ToDateTime(checkOption.checkInTime).AddMinutes(5) < DateTime.Now)
            {
                checkOption.useDesktop = true;
                checkOption.isCheckInCall = true;
            }
            checkOption.cdSabn = setEncId(checkOption.id);
            checkOption.holidayType = CheckHolidayType(DateTime.Now);

            // 출근하는날이 아니면 체크 안함.            
            switch (checkOption.holidayType)
            {
                case MyHolidayType.AM:
                    checkOption.checkInTime = Convert.ToDateTime(checkOption.checkInTime).AddHours(5).ToString("HH:mm"); // 8시: 13시, 9시: 14시, 10시: 15시
                    break;
                case MyHolidayType.PM:
                    if (Convert.ToDateTime(checkOption.checkInTime) < Convert.ToDateTime("09:00"))
                    {
                        checkOption.checkOutTime = Convert.ToDateTime(checkOption.checkOutTime).AddHours(-5).ToString("HH:mm"); // 17시: 12시
                    }
                    else
                    {
                        checkOption.checkOutTime = Convert.ToDateTime(checkOption.checkOutTime).AddHours(-4).ToString("HH:mm"); // 18시: 14시, 19시: 15시
                    }
                    break;                
            }
            util.WriteLog("출근체크", $"설정파일 로드. CheckOption: \r\n{JsonConvert.SerializeObject(checkOption, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd" })}");
        }

        /// <summary>
        /// 사번 암호화
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string setEncId(string id)
        {
            string result = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(id);
            result = Convert.ToBase64String(bytes);     // Base64
            bytes = Encoding.Default.GetBytes(result);
            result = BitConverter.ToString(bytes);      // HEX
            return result.Replace("-", "").ToLower();
        }


        /// <summary>
        /// 셋팅파일 모니터링
        /// </summary>
        private void SettingFileWatcher()
        {
            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = settingFilePath;
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            fileWatcher.Filter = settingFileName;
            fileWatcher.EnableRaisingEvents = true;
            fileWatcher.Changed += FileWatcher_Changed;
        }



        DateTime lastRead = DateTime.MinValue;
        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(settingFilePath + settingFileName);
            if (lastWriteTime != lastRead)
            {
                SetCheckOption();
                lastRead = lastWriteTime;
            }
        }

        protected override void OnStop()
        {
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
        /// PC 사용여부 (로그온 여부로 감지)
        /// </summary>
        public bool useDesktop { get; set; }

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


    /// <summary>
    /// 출퇴근 체크 결과값
    /// </summary>
    public class ResultInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public Result result { get; set; }
        public object error { get; set; }
    }

    public class Result
    {
        public string referrer { get; set; }
        public string cdSabn { get; set; }
    }



}
