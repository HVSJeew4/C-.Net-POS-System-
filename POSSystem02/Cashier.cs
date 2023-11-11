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
    public partial class Cashier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Cashier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
        }
        public void Slide( Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top  = button.Top;
        
        }
        public void LoadCart()
        {
            int i=0;
            double total = 0;
            double discount = 0;
            dvgCash.Rows.Clear();
            cn.Open();
            cm=new SqlCommand("Select c.id,c.pcode,c.pdesc,c.price,c.qty,c.disc,c.total from tbCart As  c Inner Join tbProduct As p on c.pcode*p.pcode where c.transno like @transno and c.status like 'Pending'",cn);
            cm.Parameters.AddWithValue("@transno",lblTranNo.Text);
            dr=cm.ExecuteReader();
            while(dr.Read()) 
            {
                i++;
                total += Convert.ToDouble(dr["total"].ToString());
                discount += Convert.ToDouble(dr["disc"].ToString());
                dvgCash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(),dr["pdesc"].ToString(), dr["price"].ToString(), dr["qyt"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()));
            }
            dr.Close();
            cn.Close(); 
            lblSalesTotal.Text = total.ToString("#,##0.00");  
            lblDiscount.Text = discount.ToString("#,##0.00");
            GetCartTotal();  

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #region Button
        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            Slide(btnNewTransaction);
            GetTraNo();
        }

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            Slide(btnSearchProduct);
            LookUpProduct lookup=new LookUpProduct(this);
            lookup.LoadProduct();   
            lookup.ShowDialog(); 
        }

        private void btnAddDiscount_Click(object sender, EventArgs e)
        {
            Slide(btnAddDiscount);
        }

        private void btnSettlePayment_Click(object sender, EventArgs e)
        {
            Slide(btnSettlePayment);
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            Slide(btnClearCart);
        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            Slide(btnDailySales);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            Slide(btnChangePassword);
        }

        private void btnLogoutCachierForm_Click(object sender, EventArgs e)
        {
            Slide(btnLogoutCachierForm);
        }

        #endregion Button End 

        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblSalesTotal.Text) - discount;
            double vat=sales*0.12;
            double vatable = sales - vat;
            lblTax.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            txtDisplayTotal.Text = sales.ToString("#,##0.00"); 
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        public void GetTraNo()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");
            string transno = sdate + "1001";
            lblTranNo.Text = transno;
                
        }
    }
}
