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
    public partial class Brand : Form
    {
        SqlConnection cn=new SqlConnection();
        SqlCommand cm=new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Brand()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            Loaddata();
        }
        //data retrieve form tbBrand to dgvBrand on Brand 
        public void Loaddata()
        {
            int i = 0;
            dvgBrand.Rows.Clear();
            cn.Open();
            //cm=new SqlCommand("SELECT id FROM tbBrand1 ORDER BY brand",cn);
            cm = new SqlCommand("SELECT id, brand FROM tbBrand ORDER BY brand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgBrand.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BrandModule moduleForm = new BrandModule(this);
            moduleForm.ShowDialog();
        }

        private void dvgBrand_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //For Update and Delete brand by  cell click from tbBrand
            string colname = dvgBrand.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbBrand WHERE id LIKE '" + dvgBrand[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (colname == "Edit")
            {
                BrandModule brandModule = new BrandModule(this);
                brandModule.lblId.Text = dvgBrand[1, e.RowIndex].Value.ToString();
                brandModule.txtBrand.Text = dvgBrand[2, e.RowIndex].Value.ToString();
                brandModule.btnUpdate.Enabled = true;
                brandModule.btnSave.Enabled = false;
                brandModule.ShowDialog();
            }
            Loaddata();
        }
    }
}
