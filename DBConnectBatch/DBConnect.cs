using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DBConnectBatch
{
    public partial class DBConnect : ServiceBase
    {
        
        public DBConnect()
        {
            InitializeComponent();
                        
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }


        private void DBConnectDAO()
        {
            string connectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=true";            
            string queryString = "USE HOTEL";            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {   
                SqlCommand command = new SqlCommand(queryString, connection);                
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
