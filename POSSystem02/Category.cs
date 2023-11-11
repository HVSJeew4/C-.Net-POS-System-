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
    public partial class Category : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        public Category()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            LoadCategory();
        }
        //data retrieve form tbCategory to dgvCategory on Category Form

        public void LoadCategory()
        {
            int i = 0;
            dvgCategory.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT id, category FROM tbCategory ORDER BY category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dvgCategory.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            CategoryModule module = new CategoryModule(this);
            module.ShowDialog();
             
        }

        private void dvgCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dvgCategory.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record ?", "Delete Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tbCategory WHERE id LIKE '" + dvgCategory[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Category has been successfully deleted.", "Point Of Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (colname == "Edit")
            {
                CategoryModule module = new CategoryModule(this);
                module.lblId.Text = dvgCategory[1, e.RowIndex].Value.ToString();
                module.txtCategory.Text = dvgCategory[2, e.RowIndex].Value.ToString();
                module.btnUpdate.Enabled = true;
                module.btnSave.Enabled = false;
                module.ShowDialog();
            }
            LoadCategory();


        }
    }
}
