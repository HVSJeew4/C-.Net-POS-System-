using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSSystem02
{
    internal class DBConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();

        private string con;
        public string myConnection()
        {
            con = @"Data Source=DESKTOP-UV92TIS;Initial Catalog=DBPOSales;Integrated Security=True";
            return con;
        }
        public DataTable getTable(string query)
        {  
            cn.ConnectionString=myConnection();
            cm=new SqlCommand(query,cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cm);
            DataTable dt = new DataTable(); 
            adapter.Fill(dt);
            return dt;
        }
    }
}
