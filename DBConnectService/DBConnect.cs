using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace DBConnectService
{
    public partial class DBConnect : ServiceBase
    {
        private Timer timer;
        NLog.Logger logger = NLog.LogManager.GetLogger("test");
        public DBConnect()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogManager.Configuration = new XmlLoggingConfiguration("D:\\Work\\source\\Batch\\DBConnectService\\Nlog.config");
            logger.Trace("OnStart");
            timer = new Timer();
            timer.Interval = 30 * 60 * 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        protected override void OnStop()
        {
        }


        private XmlReader getNlogConfig()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<nlog xmlns=\"http://www.nlog-project.org/schemas/NLog.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            sb.Append("    <targets>");
            sb.Append("        <target name=\"test\" xsi:type=\"File\" encoding=\"utf-8\" fileName=\"D:\\Comlog\\Hotel\\test\\Log_${date:format=yyyy-MM-dd}.log\"/>");
            sb.Append("    </targets>");
            sb.Append("    <rules>");
            sb.Append("        <logger name=\"test\" minlevel=\"Trace\" writeTo=\"test\" />");
            sb.Append("    </rules>");
            sb.Append("</nlog>");
            return XmlReader.Create(new StringReader(sb.ToString()));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            try
            {
                logger.Trace("시작");
                string connectionString = "Data Source=172.25.9.203,2433;Initial Catalog=HOTEL;User ID=tour_front;Password=ckaRp100@(";

                //string queryString ="use hotel";
                string queryString = "dbo.usp_HT_Code_Detail_Get";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CDType", "147"));

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    logger.Trace("조회:" + result.ToString());

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                Console.WriteLine(ex.Message);
            }
            logger.Trace("종료");
        }
    }
}
