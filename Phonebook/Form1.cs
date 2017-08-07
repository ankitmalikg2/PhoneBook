using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;

namespace PhonebookFoxLearn
{
    public partial class Form1 : Form
    {
        dbPhoneLinqDataContext db = new dbPhoneLinqDataContext();
        int idp=0;
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.phoneSelect(0);
            dataGridView1.Columns["id"].Visible = false;
            btnSave.Enabled = false;

        }


        private void btnNew_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            txtAddress.Text = "";
            txtName.Text = string.Empty;
            txtEmail.Text = "";
            txtPhone.Text = "";
            btnSave.Enabled = false;
            btnNew.Enabled = true;
            idp = 0;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((txtName.Text!=string.Empty) && (txtPhone.Text!=string.Empty)) {
                db.phoneinsert(txtPhone.Text, txtName.Text, txtEmail.Text, txtAddress.Text);
                dataGridView1.DataSource = db.phoneSelect(0);

                txtAddress.Text = "";
                txtName.Text = string.Empty;
                txtEmail.Text = "";
                txtPhone.Text = "";
                panel1.Enabled = false;
                btnSave.Enabled = false;
                btnNew.Enabled = true;
            }
            else
            { MessageBox.Show("you cannot leave name or phone Number Empty"); }
        }

     

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Enabled = true;
            idp= (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            txtPhone.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtName.Text =  dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtEmail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (idp != 0)
            {
                db.phoneDelete(idp);
                dataGridView1.DataSource = db.phoneSelect(0);
                dataGridView1.Columns["id"].Visible = false;

                idp = 0;
                txtAddress.Text = "";
                txtName.Text = string.Empty;
                txtEmail.Text = "";
                txtPhone.Text = "";
                panel1.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (idp != 0)
            {

                db.phoneEdit(idp, txtPhone.Text, txtName.Text, txtEmail.Text, txtAddress.Text);
                dataGridView1.DataSource = db.phoneSelect(0);
                dataGridView1.Columns["id"].Visible = false;

                idp = 0;
                txtAddress.Text = "";
                txtName.Text = string.Empty;
                txtEmail.Text = "";
                txtPhone.Text = "";
                panel1.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            txtAddress.Text = "";
            txtName.Text = string.Empty;
            txtEmail.Text = "";
            txtPhone.Text = "";
            idp = 0;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != String.Empty)
            {
                var x = db.phoneSelect(Convert.ToInt64(txtSearch.Text));
                if (x.Count() != 0)
                {
                     x = db.phoneSelect(Convert.ToInt64(txtSearch.Text));
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = x;
                    dataGridView1.Columns["id"].Visible = false;
                }
                else
                {
                    dataGridView1.Visible = false;
                    txtSearch.Text = string.Empty;
                    
                }
            }
            else
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = db.phoneSelect(0);
                dataGridView1.Columns["id"].Visible = false;
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
