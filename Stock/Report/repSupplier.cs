using BusinessLayer.DTO;
using DataLayer;
using DevExpress.Office.Utils;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Stock.Report
{
    public partial class repSupplier : DevExpress.XtraReports.UI.XtraReport
    {
        public repSupplier()
        {
            InitializeComponent();
        }

        public void InitData(List<SupplierDTO> data)
        {
            objectDataSource1.DataSource = data;
        }
    }
}
