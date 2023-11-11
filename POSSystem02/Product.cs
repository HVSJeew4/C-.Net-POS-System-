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
    public partial class Product : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dvgProduct.Rows.Clear();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.recorder FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.bid INNER JOIN tbCategory AS c ON c.id = p.cid WHERE CONCAT(p.pcode, p.pdesc, b.brand, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            //cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.recorder FROM tbProduct AS p INNER JOIN tbBrand AS b ON b.id = p.bid INNER JOIN tbCategory AS c ON c.id = p.cid WHERE CONCAT(p,pdesc,b.brand,c.category) LIKE '%'" +txtSearch.Text + "'%'", cn);
            //cm=new SqlCommand("SELECT p.pcode,p.barcode,p.pdesc,b.brand,c.category,p.price,p.recorder FROM tbProduct AS p INNER JOIN thBrand AS b ON b.id - p.bid INNER JOIN thCategory AS c on c.id -p.cid",cn);
            cn.Open();
            dr= cm.ExecuteReader();
            while (dr.Read()) 
            {
                i++;
                dvgProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModule productModule = new ProductModule(this);  
            productModule.ShowDialog();
        }

        private void dvgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dvgProduct.Columns[e.ColumnIndex].Name;
            if (colname == "Edit")
            {
                ProductModule product = new ProductModule(this);
                product.txtPCode.Text = dvgProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                product.txtBarCode.Text = dvgProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                product.txtDescription.Text = dvgProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                product.cbBrand.Text = dvgProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                product.cbCategory.Text = dvgProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                product.txtPrice.Text = dvgProduct.Rows[e.RowIndex].Cells[6].Value.ToString();
                //product.nudReOrderLevel.Value = int.Parse(dvgProduct.Rows[e.RowIndex].Cells[7].Value.ToString());

                product.txtPCode.Enabled = false;
                product.btnSave.Enabled = false;
                product.btnUpdate.Enabled = true;
                product.ShowDialog();

            }
             else if (colname == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE pcode LIKE '" + dvgProduct[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully deleted.", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }               
            }
            LoadProduct();

        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
