using System;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ReportForm : Form
    {
        string reportEmbeddedResource = "";
        string dataSet = "";
        BindingSource bindingSource = null;
        public ReportForm(string resource_, string dataset_, BindingSource binding_)
        {
            InitializeComponent();
            reportEmbeddedResource = resource_;
            dataSet = dataset_;
            bindingSource = binding_;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            ChessPlayerViewTableAdapter.Fill(ChessViewsDataSet.ChessPlayerView);
            reportViewer.LocalReport.ReportEmbeddedResource = reportEmbeddedResource;
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource(dataSet, bindingSource));
            reportViewer.RefreshReport();
        }
    }
}
