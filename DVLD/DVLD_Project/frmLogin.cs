using Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            
            clsUserinfo user = clsUserinfo.Find(userName, password);

            if (user == null)
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string path = @"C:\Users\Public\rememberme.txt";
            if (chkRememberMe.Checked)
            {
                File.WriteAllLines(path, new string[] { userName, password });
            }
            else
            {
              
                if (File.Exists(path))
                    File.Delete(path);
            }

            this.DialogResult = DialogResult.OK; 
    this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string path = @"C:\Users\Public\rememberme.txt"; 

            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                if (lines.Length >= 2)
                {
                    txtUserName.Text = lines[0];
                    txtPassword.Text = lines[1];
                    chkRememberMe.Checked = true;
                }
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
        }
    }
}
