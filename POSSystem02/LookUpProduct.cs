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
    public partial class LookUpProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        Cashier cashier;
        string stitle = "Point Of Sales";
        public LookUpProduct(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadProduct();
            cashier = cash;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dvgProduct.Rows.Clear();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.bid INNER JOIN tbCategory AS c ON c.id = p.cid WHERE CONCAT(p.pcode, p.pdesc, b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            //cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.recorder FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.bid INNER JOIN tbCategory AS c ON c.id = p.cid WHERE CONCAT(p,pdesc,b.brand,c.category) LIKE '%'" +txtSearch.Text + "'%'", cn);
            //cm=new SqlCommand("SELECT p.pcode,p.barcode,p.pdesc,b.brand,c.category,p.price,p.recorder FROM tbProduct AS p INNER JOIN thBrand AS b ON b.id - p.bid INNER JOIN thCategory AS c on c.id -p.cid",cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();

        }

        private void dvgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dvgProduct.Columns[e.ColumnIndex].Name;
            if(colName=="Select")
            {
                Qty qyt = new Qty(cashier);
                qyt.ProductDetails(dvgProduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dvgProduct.Rows[e.RowIndex].Cells[6].Value.ToString()),cashier.lblTranNo.Text, int.Parse(dvgProduct.Rows[e.RowIndex].Cells[7].Value.ToString()));
                qyt.ShowDialog();
            }
        }
    }
}
