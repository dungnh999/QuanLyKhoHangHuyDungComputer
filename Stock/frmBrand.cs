using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Wizards.Native;
using DevExpress.XtraRichEdit.Import.Html;
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
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace Stock
{
    public partial class frmBrand : DevExpress.XtraEditors.XtraForm
    {
        public frmBrand()
        {
            InitializeComponent();
        }

        DialogResult result;
        Brand _brand;
        bool _actionAdd;
        int _id = 0;
        string _status = "0";

        void _showHide(bool kt)
        {
            bbtnSave.Enabled = !kt;
            bbtn_cancel.Enabled = !kt;
            bbtnAdd.Enabled = kt;
            bbtnEdit.Enabled = kt;
            bbtnRemove.Enabled = kt;
            txtName.Enabled = !kt;
            txtCodeBrand.Enabled = !kt;
            txtNote.Enabled = !kt;
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
            _showHide(true);
            loadData();
        }

        public void loadData()
        {
            _brand = new Brand();
            gvDanhSach.DataSource = _brand.getAll();
            grvDanhSach.OptionsBehavior.Editable = false;
        }

        void _reset()
        {
            txtName.Text = "";
            txtCodeBrand.Text = "";
            txtNote.Text = "";
        }



        void saveData()
        {
            if (txtName.Text != "")
            {
                if (_actionAdd)
                {
                    try
                    {
                        tb_Brand sp = new tb_Brand();
                        sp.NAME = txtName.Text;
                        sp.CODE = txtCodeBrand.Text;
                        sp.STATUS = 1;
                        sp.AVATAR = ImageToBase64(picBrand.Image, picBrand.Image.RawFormat);
                        sp.CREATE_BY = Common.UserPublic.ID;
                        sp.CREATE_AT = DateTime.Now;
                        _brand.Add(sp);
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
                        tb_Brand sp = _brand.getItem(_id);
                        sp.NAME = txtName.Text;
                        sp.CODE = txtCodeBrand.Text;
                        sp.STATUS = 1;
                        sp.UPDATE_BY = Common.UserPublic.ID;
                        sp.AVATAR = ImageToBase64(picBrand.Image, picBrand.Image.RawFormat);
                        sp.UPDATE_AT = DateTime.Now;
                        _brand.Update(sp);
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

        private void bbtnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _actionAdd = true;
            _reset();
            _showHide(false);
        }



        private void grvDanhSach_Click(object sender, EventArgs e)
        {
            if (grvDanhSach.RowCount > 0)
            {
                if (_id != int.Parse(grvDanhSach.GetFocusedRowCellValue("ID").ToString()))
                {
                    _id = int.Parse(grvDanhSach.GetFocusedRowCellValue("ID").ToString());
                    tb_Brand dt = _brand.getItem(_id);
                    _status = (dt.STATUS).ToString();
                    txtName.Text = dt.NAME;
                    txtCodeBrand.Text = dt.CODE;
                    if (dt.AVATAR != null && dt.AVATAR.Length != 0)
                    {
                        picBrand.Image = Base64ToImage(dt.AVATAR);
                    }
                    else
                    {
                        // Set a default image or handle the empty case
                        picBrand.Image = null; // Or use picBrand.Image = Image.FromFile("path_to_default_image.jpg");
                    }



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

        private void bbtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            saveData();
        }

        private byte[] GetAvatarEmpty()
        {
            return null; 
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
                    tb_Brand sp = _brand.getItem(_id);
                    sp.STATUS = 1;
                    sp.UPDATE_BY = Common.UserPublic.ID;
                    sp.UPDATE_AT = DateTime.Now;
                    _brand.changeStatus(sp);
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

        private void bbtnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtName.Text != "")
            {
                _actionAdd = false;
                _showHide(false);
                txtCodeBrand.Enabled = false;
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

        private void bbtnReload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadData();
        }

        private void btnUpdateImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select an Image";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picBrand.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }


        public byte[] ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                return imageBytes;
            }
        }

        public Image Base64ToImage(byte[] imageBytes) { 
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0 , imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
    }
}