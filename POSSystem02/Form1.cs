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
    public partial class Form1 : Form
    {
        SqlConnection cn=new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        public Form1()
        {
            InitializeComponent();
            customizedDesign();
            cn=new SqlConnection(dbcon.myConnection());
            cn.Open();
        }

 #region PanelSlide
        private void customizedDesign()
        {
            panelSubInStock.Visible = false;
            panelSubRecord.Visible = false;
            panelSubSettings.Visible = false;
            panelSubProduct.Visible = false;

        }
        private void hideSubmenu()
        {
            if (panelSubInStock.Visible == true)
                panelSubInStock.Visible = false;
            if (panelSubRecord.Visible == true)
                panelSubRecord.Visible = false;
            if (panelSubSettings.Visible == true)
                panelSubSettings.Visible = false;
            if (panelSubProduct.Visible == true)
                panelSubProduct.Visible = false;

        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }



        #endregion panelSlide

        private Form activeForm = null;
        public void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblTitleName.Text = childForm.Text;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnDashBorad_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubProduct);
        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Product());
            hideSubmenu();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Category());
            hideSubmenu();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Brand());
            hideSubmenu();
        }

        private void btnInStock_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubInStock);
        }

        private void btnStockEntry_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnStockAdjustment_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnSuppiler_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SUPPLIER());
            hideSubmenu();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubRecord);
        }

        private void btnSaleHistory_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

       
        private void btnSettings_Click(object sender, EventArgs e)
        {
            showSubmenu(panelSubSettings);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserAccount());
            hideSubmenu();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }
    }

}


