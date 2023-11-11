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
    public partial class SupplierModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        string stitle = "Point Of Salese";
        SUPPLIER supplier;
        public SupplierModule(SUPPLIER sup)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            supplier = sup;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear()
        {
            txtAddress.Clear();
            txtContactPerson.Clear();
            txtPhoneNo.Clear();
            txtEmailAddress.Clear();
            txtFaxNo.Clear();
            txtSupplier.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = true;
            txtSupplier.Focus();


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if (MessageBox.Show("Are You Sure to save this Supplier ?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbSupplier (supplier,address,contactperson,phone,email,fax)VALUES(@supplier,@address,@contactperson,@phone,@email,@fax) ", cn);
                    cm.Parameters.AddWithValue("@supplier", txtSupplier.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContactPerson.Text);
                    cm.Parameters.AddWithValue("@phone",txtPhoneNo.Text);
                    cm.Parameters.AddWithValue("@email",txtEmailAddress.Text);
                    cm.Parameters.AddWithValue("@fax",txtFaxNo.Text);
                    cn.Open();
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved", stitle);
                    clear();
                    supplier.LoadSupplier();
                   
                }
                
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure to save this Supplier ?", "Save Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbSupplier SET supplier=@supplier,address=@address,contactperson=@contactperson,phone=@phone,email=@email,fax=@fax WHERE id=@id ", cn);
                    cm.Parameters.AddWithValue("@id",lblId.Text);
                    cm.Parameters.AddWithValue("@supplier", txtSupplier.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContactPerson.Text);
                    cm.Parameters.AddWithValue("@phone", txtPhoneNo.Text);
                    cm.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    cm.Parameters.AddWithValue("@fax", txtFaxNo.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Supplier has been successfully saved","Update Supplier",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    clear();
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Warning");
            }
        }
    }
}
