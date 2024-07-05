using BusinessLayer;
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
    public partial class frmOptionProducts : DevExpress.XtraEditors.XtraForm
    {
        public frmOptionProducts()
        {
            InitializeComponent();
        }

        OptionProduct _optionProduct;
        GroupOption _groupOption;
        CategoryProducts _categoryProducts;
        private void frmOptionProducts_Load(object sender, EventArgs e)
        {
            _showHide(true);
            getGroupOption();
            getCategory();
            loadData();
        }

        public void loadData()
        {
            _optionProduct = new OptionProduct();
            gvDanhSach.DataSource = _optionProduct.getAll();
            grvDanhSach.OptionsBehavior.Editable = false;
        }

        void getGroupOption()
        {
            _groupOption = new GroupOption();
            cboGroupOption.DataSource = _groupOption.getAll();
            cboGroupOption.DisplayMember = "NAME";
            cboGroupOption.ValueMember = "ID";
        }

        void getCategory()
        {
            _categoryProducts = new CategoryProducts();
            cboCategory.DataSource = _categoryProducts.getAll();
            cboCategory.DisplayMember = "NAME";
            cboCategory.ValueMember = "ID";
        }

        void _showHide(bool kt)
        {
            bbtnSave.Enabled = !kt;
            bbtn_cancel.Enabled = !kt;
            bbtnAdd.Enabled = kt;
            bbtnEdit.Enabled = kt;
            bbtnRemove.Enabled = kt;
            txtName.Enabled = !kt;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbtnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void bbtn_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}