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
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {   
        frmSupplier _unit;
        public MainForm()
        {
            InitializeComponent();
        }

        IOverlaySplashScreenHandle ShowProgressPanel(Control control, OverlayWindowOptions option)
        {
            return SplashScreenManager.ShowOverlayForm(control, option);
        }

        OverlayWindowOptions options = new OverlayWindowOptions(
            backColor: Color.Black,
            opacity: 0.5,
            fadeIn: false,
            fadeOut: false
        );

        void openForm(Type TypeForm)
        {
            foreach(var frm in MdiChildren)
            {
                frm.Activate();
                return;
            }
            Form f = (Form)Activator.CreateInstance(TypeForm);
            f.MdiParent = this;
            f.Show();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            Common.handle =  ShowProgressPanel(this, options);
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
           // _unit = new Supplier();
        }

        private void mnSupplier_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openForm(typeof(frmSupplier));
        }

        private void mnCategoryProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openForm(typeof(frmCategoryProduct));
        }

        private void mnBrand_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openForm(typeof(frmBrand));
        }
    }
}