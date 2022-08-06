using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk
{
    class DbClass
    {
        public string ServerName = System.Configuration.ConfigurationSettings.AppSettings["ServerName"];
        public string DBName = System.Configuration.ConfigurationSettings.AppSettings["DbName"];
        public string UserName = System.Configuration.ConfigurationSettings.AppSettings["UserName"];
        public string Password = System.Configuration.ConfigurationSettings.AppSettings["Password"];

        public static string cnString = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
        public SqlConnection Connection = new SqlConnection(cnString);

        public void connect()
        {
            try
            {
                Connection.Open();
                //cn = new SqlConnection("Data Source=KARAD-BA38C4ED2;Initial Catalog=Inventory1;User ID=sa;Password=123");
                //cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public DataTable GetTable(string SelectQuery)
        {
            SqlDataAdapter da = new SqlDataAdapter(SelectQuery, Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public SqlDataAdapter GetReport(String str)
        {
            connect();
            SqlDataAdapter da = new SqlDataAdapter(str, Connection);
            return da;
        }

        public DataTable Gettable(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
