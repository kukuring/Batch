using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.IO;

namespace WorkTimeCheckAlarm
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : System.Configuration.Install.Installer
    {
        private readonly ServiceProcessInstaller processInstaller;
        private readonly System.ServiceProcess.ServiceInstaller serviceInstaller;
        private WorkTimeCheckAlarmService.Util util = new WorkTimeCheckAlarmService.Util();


        public ServiceInstaller()
        {
            InitializeComponent();
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            // Service will run under system account
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Service will have Start Type of Manual
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "WorkTimeCheckAlarmService";

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

            util.WriteLog("출근체크", "프로그램 설치 시작(ServiceInstaller)");

            serviceInstaller.AfterInstall += ServiceInstaller_AfterInstall;
        }
        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            util.WriteLog("출근체크", "프로그램 설치 시작(ServiceInstaller_AfterInstall)");
            ServiceController sc = new ServiceController("WorkTimeCheckAlarmService");
            sc.Start();
        }

    }
}
