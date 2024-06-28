using BusinessLayer;
using BusinessLayer.DTO;
using DataLayer;
using DevExpress.XtraEditors;
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
    public partial class frmSupplier : DevExpress.XtraEditors.XtraForm
    {
        public frmSupplier()
        {
            InitializeComponent();
        }

        bool _actionAdd;
        int _id = 0;
        Supplier _supplier;

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            _showHide(true);
            loadData();
        }

        void loadData()
        {
            _supplier = new Supplier();
            gvDanhSach.DataSource = _supplier.getAll();
            gridView3.OptionsBehavior.Editable = false;
        }

        void _showHide(bool kt)
        {
            bbtnSave.Enabled = !kt;
            bbtn_cancel.Enabled = !kt;
            bbtnAdd.Enabled = kt;
            bbtnEdit.Enabled = kt;
            bbtnRemove.Enabled = kt;
            bbtnPrint.Enabled = kt;
            txtName.Enabled = !kt;
            txtCode.Enabled = !kt;
            txtNameBank.Enabled = !kt;
            txtPhone.Enabled = !kt;
            txtEmail.Enabled = !kt;
            txtBanchBank.Enabled = !kt;
            txtTax.Enabled = !kt;
            txtNumberBank.Enabled = !kt;
            txtNameAccount.Enabled = !kt;
            txtCCCD.Enabled = !kt;
            txtAddress.Enabled = !kt;
            txtNote.Enabled = !kt;
            cbbTypeSupplier.Enabled = !kt;
        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _actionAdd = true;
        }


        private void bbtn_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(true);
        }


        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveData();
            _showHide(true);
            loadData();
        }

        void loadOptionType()
        {
            cbbTypeSupplier.Properties.Items.AddRange(["Cá nhân", "1"], 'Doanh Nghiệp');
        }


        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _actionAdd = false;

        }

        void saveData()
        {
            if (_actionAdd)
            {
                tb_Supplier sp = new tb_Supplier();
                sp.NAME = txtName.Text;
                sp.CODE = txtCode.Text;
                sp.NAME_BANK = txtNameBank.Text;
                sp.PHONE = txtPhone.Text;
                sp.EMAIL = txtEmail.Text;   
                sp.BRANCH_BANK = txtBanchBank.Text;   
                sp.TAX = txtTax.Text;
                sp.NUMBER_BANK = txtNumberBank.Text;
                sp.TYPE = 1;
                sp.NAME_ACCOUNT = txtNameAccount.Text;
                sp.ADDRESS = txtAddress.Text;
                sp.NOTE = txtNote.Text;
                sp.STATUS = 1;
                sp.CREATE_BY = Common.UserPublic.ID;
                sp.CREATE_AT = DateTime.Now;
                _supplier.Add(sp);
            }
            else
            {
                tb_Supplier sp = _supplier.getItem(_id);
                sp.NAME = txtName.Text;
                sp.CODE = txtCode.Text;
                sp.NAME_BANK = txtNameBank.Text;
                sp.PHONE = txtPhone.Text;
                sp.EMAIL = txtEmail.Text;
                sp.BRANCH_BANK = txtBanchBank.Text;
                sp.TAX = txtTax.Text;
                sp.NUMBER_BANK = txtNumberBank.Text;
                sp.TYPE = 1;
                sp.NAME_ACCOUNT = txtNameAccount.Text;
                sp.ADDRESS = txtAddress.Text;
                sp.NOTE = txtNote.Text;
                sp.STATUS = 1;
                sp.CREATE_BY = Common.UserPublic.ID;
                sp.CREATE_AT = DateTime.Now;
                _supplier.Update(sp);
            }

        }


        private void bbtnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadData();
        }




        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gridView3.RowCount > 0)
            {
                if(_id != int.Parse(gridView3.GetFocusedRowCellValue("ID").ToString()))
                {
                    _id = int.Parse(gridView3.GetFocusedRowCellValue("ID").ToString());
                    tb_Supplier dt = _supplier.getItem(_id);
                    txtName.Text = dt.NAME;
                    txtCode.Text = dt.CODE;
                    txtNameBank.Text = dt.NAME_BANK;
                    txtPhone.Text = dt.PHONE;
                    txtEmail.Text = dt.EMAIL;
                    txtBanchBank.Text = dt.BRANCH_BANK;
                    txtTax.Text = dt.TAX;
                    txtNumberBank.Text = dt.NUMBER_BANK;
                    txtNameAccount.Text = dt.NAME_ACCOUNT;
                    txtAddress.Text = dt.ADDRESS;
                    txtNote.Text = dt.NOTE;
                    txtCCCD.Text = "111";
                }
            }
        }






        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btnAddSupplier_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barDockControlTop_Click(object sender, EventArgs e)
        {

        }

        private void barDockControlBottom_Click(object sender, EventArgs e)
        {

        }

        private void barDockControlLeft_Click(object sender, EventArgs e)
        {

        }

        private void barDockControlRight_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barDockControl1_Click(object sender, EventArgs e)
        {

        }

        private void barDockControl2_Click(object sender, EventArgs e)
        {

        }

        private void barDockControl3_Click(object sender, EventArgs e)
        {

        }

        private void barDockControl4_Click(object sender, EventArgs e)
        {

        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void groupControl4_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}