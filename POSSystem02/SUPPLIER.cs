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
    public partial class SUPPLIER : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        
        public SUPPLIER()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadSupplier();
            
        }

        public void LoadSupplier()
        {
            dvgSupplier.Rows.Clear();
            int i = 0;// ORDER BY supplier id,supplier 
            cn.Open();
            cm=new SqlCommand("SELECT * FROM tbSupplier",cn); //"SELECT id, category FROM tbCategory ORDER BY category
            dr =cm.ExecuteReader();
            while(dr.Read()) 
            {
                i++;
                dvgSupplier.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SupplierModule supplierModule=new SupplierModule(this);
            supplierModule.ShowDialog();

        }

        private void dvgSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dvgSupplier.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this supplier ?", "Delete Supplier", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbSupplier WHERE id LIKE '" + dvgSupplier.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cn.Close();
                    MessageBox.Show("Supplier has been successfully deleted.", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (colname == "Edit")
            {
                SupplierModule Supmodule = new SupplierModule(this);
                Supmodule.lblId.Text = dvgSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                Supmodule.txtSupplier.Text = dvgSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                Supmodule.txtAddress.Text = dvgSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                Supmodule.txtContactPerson.Text = dvgSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();
                Supmodule.txtPhoneNo.Text = dvgSupplier.Rows[e.RowIndex].Cells[5].Value.ToString();
                Supmodule.txtFaxNo.Text = dvgSupplier.Rows[e.RowIndex].Cells[6].Value.ToString();
                Supmodule.txtEmailAddress.Text = dvgSupplier.Rows[e.RowIndex].Cells[7].Value.ToString();

            }
            LoadSupplier();
        }
    }
}
