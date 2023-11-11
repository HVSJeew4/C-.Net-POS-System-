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
    public partial class UserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        public UserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
        }

        public void clear()
        {
            txtUsername.Clear();
            txtPass.Clear();
            txtReTypePass.Clear();
            txtFullName.Clear();
            cbRole.Text = "";
            txtUsername.Focus();

        }
        private void btnAccSave_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtPass.Text!=txtReTypePass.Text)
                {
                    MessageBox.Show("Password did not Match","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm=new SqlCommand("Insert into  tbUser(username,password,role,name) Values(@username,@password,@role,@name)",cn);
                cm.Parameters.AddWithValue("@username",txtUsername.Text);
                cm.Parameters.AddWithValue("@password",txtPass.Text);
                cm.Parameters.AddWithValue("@role",  cbRole.Text);
                cm.Parameters.AddWithValue("@name", txtFullName.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New Account has been successfully Saved!","Saved Record",MessageBoxButtons.OK,MessageBoxIcon.Information);
                clear();
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Warning");
            }

        }

        private void btnAccCancel_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
