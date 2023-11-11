using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSSystem02
{
    public partial class Qty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        private String pcode;
        private double price;
        private String transno;
        private int qyt;
        Cashier cashier;

        public Qty(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier=cash;
        }

        internal void ProductDetails(string v1, double v2, string text, int v3)
        {
            throw new NotImplementedException();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar==13)&&(txtQty.Text!=string.Empty)) 
            {
                cn.Open();
                cm=new SqlCommand("Insert into tbCart(transno,pcode,price,qyt,sdate,cashier)Valuse(@transno,@pcode,@price,@qyt,@sdate,@cashier)", cn);
                cm.Parameters.AddWithValue("@transno", transno);
                cm.Parameters.AddWithValue("@pcode", pcode);
                cm.Parameters.AddWithValue("@price", price);
                cm.Parameters.AddWithValue("@qyt", int.Parse(txtQty.Text));
                cm.Parameters.AddWithValue("@sdate",DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", cashier.usernamecash);
                cm.ExecuteNonQuery();
                cn.Close();
                cashier.LoadCart();
                this.Dispose();

            }
        }
    }
}
