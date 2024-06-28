using BusinessLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        Users _users;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _users = new Users();
            bool db = _users.checkLogin(txtLgUsername.Text, txtLgPassword.Text);
            if (db)
            {
                Common.UserPublic = _users.getInfo(txtLgUsername.Text);
                MainForm main = (MainForm) Application.OpenForms["mainForm"];
                main.lblUserName.Caption = Common.UserPublic.USERNAME;
                if (Common.handle != null)
                {
                    SplashScreenManager.CloseOverlayForm(Common.handle);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên hoặc mật khẩu không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}