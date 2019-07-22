using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniPCBatch
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string keyPath = "Software\\Policies\\Microsoft\\Windows\\CONTROL PANEL\\DESKTOP";
            System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
            eventLog.Source = "MiniPCBatch";

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(keyPath, true);

            string screenSaveTimeOut = registryKey.GetValue("ScreenSaveTimeOut").ToString();
            string screenSaverIsSecure = registryKey.GetValue("ScreenSaverIsSecure").ToString();
                        
            registryKey.SetValue("ScreenSaveTimeOut", "36000");
            registryKey.SetValue("ScreenSaverIsSecure", "0");
            
            screenSaveTimeOut = $"{screenSaveTimeOut} | {registryKey.GetValue("ScreenSaveTimeOut")}";
            screenSaverIsSecure = $"{screenSaverIsSecure} | {registryKey.GetValue("ScreenSaverIsSecure")}";

            eventLog.WriteEntry($"screenSaveTimeOut: {screenSaveTimeOut} , screenSaverIsSecure: {screenSaverIsSecure}", System.Diagnostics.EventLogEntryType.Information);

            registryKey.Close();

            if (args.Length > 0 && args[0] == "0")
            {
                POINT pt;
                GetCursorPos(out pt);

                mouse_event(RBDOWN, 0, 0, 0, 0);
                mouse_event(RBUP, 0, 0, 0, 0);
                SetCursorPos(pt.x - 10, pt.y - 10);
                mouse_event(LBDOWN, 0, 0, 0, 0);
                mouse_event(LBUP, 0, 0, 0, 0);
                SetCursorPos(pt.x, pt.y);
            }

            
        }

        private const uint LBDOWN = 0x00000002; // 왼쪽 마우스 버튼 눌림
        private const uint LBUP = 0x00000004; // 왼쪽 마우스 버튼 떼어짐
        private const uint RBDOWN = 0x00000008; // 오른쪽 마우스 버튼 눌림
        private const uint RBUP = 0x000000010; // 오른쪽 마우스 버튼 떼어짐
        private const uint MBDOWN = 0x00000020; // 휠 버튼 눌림
        private const uint MBUP = 0x000000040; // 휠 버튼 떼어짐
        private const uint WHEEL = 0x00000800; //휠 스크롤

        [DllImport("user32.dll")]
        static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        static extern int GetCursorPos(out POINT pt);

        

        [DllImport("user32.dll")] // 입력 제어
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, int dwExtraInfo);
        public struct POINT
        {
            public int x;
            public int y;
        }




    }
}
