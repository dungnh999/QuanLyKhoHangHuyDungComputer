﻿using BusinessLayer;
using BusinessLayer.DTO;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using Stock.Report;
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
        SupplierType _supplierType;
        private void frmSupplier_Load(object sender, EventArgs e)
        {
            loadOptionType();
            _showHide(true);
            loadData();
        }

        void loadData()
        {
            _supplier = new Supplier();
            gvDanhSach.DataSource = _supplier.getAll();
            gridView3.OptionsBehavior.Editable = false;
        }

        void _reset()
        {
            txtName.Text = "";
            txtCode.Text = "";
            txtNameBank.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtBanchBank.Text = "";
            txtTax.Text = "";
            txtNumberBank.Text = "";
            txtNameAccount.Text = "";
            txtCCCD.Text = "";
            txtAddress.Text = "";
            txtNote.Text = "";
            cboTypeSupplier.Text = "";
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
            cboTypeSupplier.Enabled = !kt;
        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(false);
            _reset();
            _actionAdd = true;
        }


        private void bbtn_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(true);
        }


        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveData();
            _reset();
            _showHide(true);
            loadData();
        }

        void loadOptionType()
        {
            _supplierType = new SupplierType();
            cboTypeSupplier.DataSource = _supplierType.getList();
            cboTypeSupplier.DisplayMember = "NAME";
            cboTypeSupplier.ValueMember = "ID";
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
                sp.TYPE = int.Parse(cboTypeSupplier.SelectedValue.ToString());
                sp.NAME_ACCOUNT = txtNameAccount.Text;
                sp.ADDRESS = txtAddress.Text;
                sp.NOTE = txtNote.Text;
                sp.STATUS = 1;
                sp.CREATE_BY = Common.UserPublic.ID;
                sp.CREATE_AT = DateTime.Now;
                sp.ID_CARD = txtCCCD.Text;
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
                sp.TYPE = int.Parse(cboTypeSupplier.SelectedValue.ToString());
                sp.NAME_ACCOUNT = txtNameAccount.Text;
                sp.ADDRESS = txtAddress.Text;
                sp.NOTE = txtNote.Text;
                sp.ID_CARD = txtCCCD.Text;
                sp.STATUS = 1;
                sp.UPDATE_BY = Common.UserPublic.ID;
                sp.UPDATE_AT = DateTime.Now;
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
                    cboTypeSupplier.SelectedValue = dt.TYPE;
                    txtCCCD.Text = dt.ID_CARD;
                }
            }
        }

        private void bbtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            repSupplier frmRep = new repSupplier();
            DocumentViewer documentViewer = new DocumentViewer();
            var lstSupplier = _supplier.getAll();
            frmRep.InitData(lstSupplier);
            documentViewer.DocumentSource = frmRep;
            frmRep.ShowPreviewDialog();
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