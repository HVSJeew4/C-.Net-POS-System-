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
    public partial class ProductModule : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        string stitle = "Point Of Sales";
        Product product;
        public ProductModule(Product pr)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadCategory();
            LoadBrand();
            product = pr;
            
        }
        //Loading the Data for Product Module Design to from the thBrand Table 
        public void LoadBrand()
        {
            cbBrand.Items.Clear();
            cbBrand.DataSource = dbcon.getTable("SELECT * FROM tbBrand");
            cbBrand.DisplayMember = "brand";
            cbBrand.ValueMember = "id";
        }
        //Loading the Data for Product Module Design to from the thCategory Table   
        public void LoadCategory()
        {
            cbCategory.Items.Clear();
            cbCategory.DataSource = dbcon.getTable("SELECT *  FROM tbCategory");
            cbCategory.DisplayMember = "category";
            cbCategory.ValueMember = "id";
        }
        //Close Buttion
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //Clear Function
        public void clear()
        { 
            txtPCode.Clear();
            txtBarCode.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            cbBrand.SelectedIndex =0;
            cbCategory.SelectedIndex = 0;
            nudReOrderLevel.Value = 1;
            txtPCode.Enabled = true;
            txtPCode.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                if(MessageBox.Show("Are You Sure to save this product ?","Save Product",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    cm=new SqlCommand("INSERT INTO tbProduct (pcode,barcode,pdesc,bid,cid,price,recorder)VALUES(@pcode,@barcode,@pdesc,@bid,@cid,@price,@recorder) ",cn);
                    cm.Parameters.AddWithValue("@pcode",txtPCode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarCode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                    cm.Parameters.AddWithValue("@bid", cbBrand.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", cbCategory.SelectedValue);
                    cm.Parameters.AddWithValue("@price",double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@recorder", txtPCode.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved",stitle);
                    clear();
                    product.LoadProduct();
                }
                
            }
            catch(Exception ex)
                 
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure to save this product ?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET barcode=@barcode,pdesc=@pdesc,bid=@bid,cid=@cid,price=price,recorder=@recorder WHERE pcode LIKE @pcode ", cn);
                    cm.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarCode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                    cm.Parameters.AddWithValue("@bid", cbBrand.SelectedValue);
                    cm.Parameters.AddWithValue("@cid", cbCategory.SelectedValue);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@recorder", txtPCode.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved", stitle);
                    clear();
                    product.LoadProduct();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
            
           
        }
    }
}
