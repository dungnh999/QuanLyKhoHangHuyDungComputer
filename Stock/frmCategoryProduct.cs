using BusinessLayer;
using DataLayer;
using DevExpress.Utils;
using DevExpress.Xpo.Logger;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraRichEdit.Import.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class frmCategoryProduct : DevExpress.XtraEditors.XtraForm
    {
        SvgImageCollection secondarySvgImageCollection;
        public frmCategoryProduct()
        {
            InitializeComponent();
        }

        CategoryProducts _categoryProducts;
        DialogResult result;
        private void frmCategoryProduct_Load(object sender, EventArgs e)
        {
            _showHide(true);
            loadData();
        }

        bool _actionAdd;
        int _id = 0;
        string _status = "0";

        public void loadData()
        {
            _categoryProducts = new CategoryProducts();
            gvDanhSach.DataSource = _categoryProducts.getAll();
            grvRowData.OptionsBehavior.Editable = false;
        }

        void _showHide(bool kt)
        {
            bbtnSave.Enabled = !kt;
            bbtn_cancel.Enabled = !kt;
            bbtnAdd.Enabled = kt;
            bbtnEdit.Enabled = kt;
            bbtnRemove.Enabled = kt;
            txtName.Enabled = !kt;
            txtCodeCategory.Enabled = !kt;
        }

        void _reset()
        {
            txtName.Text = "";
            txtCodeCategory.Text = "";
        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _actionAdd = true;
            _reset();
            _showHide(false);
        }

        void saveData()
        {
            if(txtName.Text != "")
            {
                if (_actionAdd)
                {
                    try
                    {
                        tb_Category sp = new tb_Category();
                        sp.NAME = txtName.Text;
                        sp.CODE = txtCodeCategory.Text;
                        sp.STATUS = 1;
                        sp.CREATE_BY = Common.UserPublic.ID;
                        sp.CREATE_AT = DateTime.Now;
                        _categoryProducts.Add(sp);
                        _reset();
                        loadData();
                        _showHide(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        tb_Category sp = _categoryProducts.getItem(_id);
                        sp.NAME = txtName.Text;
                        sp.CODE = txtCodeCategory.Text;
                        sp.STATUS = 1;
                        sp.UPDATE_BY = Common.UserPublic.ID;
                        sp.UPDATE_AT = DateTime.Now;
                        _categoryProducts.Update(sp);
                        _reset();
                        loadData();
                        _showHide(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộC", "Thông báo", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }

        private void bbtnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadData();
        }

        private string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveData();
        }

        private void txtCodeCategory_TextChanged(object sender, EventArgs e)
        {
            // Lưu vị trí con trỏ hiện tại
            int selectionStart = txtCodeCategory.SelectionStart;

            // Chuyển đổi văn bản thành chữ hoa không dấu
            txtCodeCategory.Text = RemoveDiacritics(txtCodeCategory.Text).ToUpper();

            // Đặt lại vị trí con trỏ
            txtCodeCategory.SelectionStart = selectionStart;
        }

        private void grvRowData_Click(object sender, EventArgs e)
        {
            if (grvRowData.RowCount > 0)
            {
                if (_id != int.Parse(grvRowData.GetFocusedRowCellValue("ID").ToString()))
                {
                    _id = int.Parse(grvRowData.GetFocusedRowCellValue("ID").ToString());
                    tb_Category dt = _categoryProducts.getItem(_id);
                    _status = (dt.STATUS).ToString();
                    txtName.Text = dt.NAME;
                    txtCodeCategory.Text = dt.CODE;
                    if (dt.STATUS == 1) {
                        bbtnRemove.Caption = "Tắt hoạt động";
                        bbtnRemove.ImageOptions.Image = Properties.Resources.cancel_16x16;
                    }
                    else
                    {
                        bbtnRemove.Caption = "Bật hoạt động";
                        bbtnRemove.ImageOptions.Image = Properties.Resources.cancel_16x16;
                    }
                }
            }
        }

        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtName.Text != "")
            {
                _actionAdd = false;
                _showHide(false);
                txtCodeCategory.Enabled = false;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu muốn sửa ?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bbtn_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _showHide(true);
        }

        private void bbtnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            result = MessageBox.Show(
               "Bạn có chắc chắn muốn tạm ngưng",
               "Xác nhận",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question
           );

            if (result == DialogResult.Yes)
            {
                try
                {
                    tb_Category sp = _categoryProducts.getItem(_id);
                    sp.STATUS = 1;
                    sp.UPDATE_BY = Common.UserPublic.ID;
                    sp.UPDATE_AT = DateTime.Now;
                    _categoryProducts.changeStatus(sp);
                    _reset();
                    loadData();
                    _showHide(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}