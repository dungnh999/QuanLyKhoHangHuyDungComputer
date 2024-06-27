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
        Supplier _unit;
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


        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowProgressPanel(this, options);
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();

           // _unit = new Supplier();
        }
    }
}