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
using DataLayer;

namespace Stock
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        SqlConnection GetCon(string server, string username, string pass, string database)
        {
            return new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + username + ";Password=" + pass + ";");
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void frmConnect_Load(object sender, EventArgs e)
        {

        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetCon(txtServer.Text, txtTaiKhoan.Text, txtMatKhau.Text, cboDatabase.Text);
            try
            {
                con.Open();
                MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK , MessageBoxIcon.Information);
                Application.Run(new MainForm());
            }
            catch 
            {
                MessageBox.Show("Kết nối thất bại");
            }
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string enCryptServer = Encryptor.Encrypt(txtServer.Text, "qwertyuiop", true);
            string enCryptUserName = Encryptor.Encrypt(txtTaiKhoan.Text, "qwertyuiop", true);
            string enCryptPass = Encryptor.Encrypt(txtMatKhau.Text, "qwertyuiop", true);
            string enCryptDatabase = Encryptor.Encrypt(cboDatabase.Text, "qwertyuiop", true);
            connect cn = new connect(enCryptServer, enCryptUserName, enCryptPass, enCryptDatabase);
            cn.ConnectData();
            MessageBox.Show("Lưu file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboDatabase_Click(object sender, EventArgs e)
        {
            cboDatabase.Items.Clear();
            try
            {
                string Conn = "server=" + txtServer.Text + ";User Id=" + txtTaiKhoan.Text + ";pwd=" + txtMatKhau.Text + ";";
                SqlConnection con = new SqlConnection(Conn);
                con.Open();
                string sql = "select name from sys.databases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')";
                SqlCommand cmd = new SqlCommand(sql, con);
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) 
                {
                    cboDatabase.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}
