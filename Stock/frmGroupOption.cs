using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Wizards.Native;
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
    public partial class frmGroupOption : DevExpress.XtraEditors.XtraForm
    {
        public frmGroupOption()
        {
            InitializeComponent();
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
        GroupOption _groupOption;
        DialogResult result;
        bool _actionAdd;
        int _id = 0;
        string _status = "0";

        private void frmGroupOption_Load(object sender, EventArgs e)
        {
            _showHide(true);
            loadData();
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

        void _reset()
        {
            txtName.Text = "";
        }

        public void loadData()
        {
            _groupOption = new GroupOption();
            gvDanhSach.DataSource = _groupOption.getAll();
            grvDanhSach.OptionsBehavior.Editable = false;
        }

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _actionAdd = true;
            _reset();
            _showHide(false);
        }

        void saveData()
        {
            if (txtName.Text != "")
            {
                if (_actionAdd)
                {
                    try
                    {
                        tb_Group_Option sp = new tb_Group_Option();
                        sp.NAME = txtName.Text;
                        sp.STATUS = 1;
                        sp.CREATE_BY = Common.UserPublic.ID;
                        sp.CREATE_AT = DateTime.Now;
                        _groupOption.Add(sp);
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
                        tb_Group_Option sp = _groupOption.getItem(_id);
                        sp.NAME = txtName.Text;
                        sp.STATUS = 1;
                        sp.UPDATE_BY = Common.UserPublic.ID;
                        sp.UPDATE_AT = DateTime.Now;
                        _groupOption.Update(sp);
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
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộC", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bbtnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadData();
        }

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveData();
        }

        private void grvRowData_Click(object sender, EventArgs e)
        {
            if (grvDanhSach.RowCount > 0)
            {
                if (_id != int.Parse(grvDanhSach.GetFocusedRowCellValue("ID").ToString()))
                {
                    _id = int.Parse(grvDanhSach.GetFocusedRowCellValue("ID").ToString());
                    tb_Group_Option dt = _groupOption.getItem(_id);
                    _status = (dt.STATUS).ToString();
                    txtName.Text = dt.NAME;
                    if (dt.STATUS == 1)
                    {
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
                    tb_Group_Option sp = _groupOption.getItem(_id);
                    sp.STATUS = 1;
                    sp.UPDATE_BY = Common.UserPublic.ID;
                    sp.UPDATE_AT = DateTime.Now;
                    _groupOption.changeStatus(sp);
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