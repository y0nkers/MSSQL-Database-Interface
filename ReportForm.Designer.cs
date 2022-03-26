
namespace CourseWork
{
    partial class ReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.ChessPlayerViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ChessViewsDataSet = new CourseWork.ChessViewsDataSet();
            this.ChessPlayerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ChessDataSet = new CourseWork.ChessDataSet();
            this.RankBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ChessPlayerTableAdapter = new CourseWork.ChessDataSetTableAdapters.ChessPlayerTableAdapter();
            this.RankTableAdapter = new CourseWork.ChessDataSetTableAdapters.RankTableAdapter();
            this.TournamentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TournamentTableAdapter = new CourseWork.ChessDataSetTableAdapters.TournamentTableAdapter();
            this.ChessPlayerViewTableAdapter = new CourseWork.ChessViewsDataSetTableAdapters.ChessPlayerViewTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ChessPlayerViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessViewsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessPlayerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RankBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TournamentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ChessPlayerViewBindingSource
            // 
            this.ChessPlayerViewBindingSource.DataMember = "ChessPlayerView";
            this.ChessPlayerViewBindingSource.DataSource = this.ChessViewsDataSet;
            // 
            // ChessViewsDataSet
            // 
            this.ChessViewsDataSet.DataSetName = "ChessViewsDataSet";
            this.ChessViewsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ChessPlayerBindingSource
            // 
            this.ChessPlayerBindingSource.DataMember = "ChessPlayer";
            this.ChessPlayerBindingSource.DataSource = this.ChessDataSet;
            // 
            // ChessDataSet
            // 
            this.ChessDataSet.DataSetName = "ChessDataSet";
            this.ChessDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // RankBindingSource
            // 
            this.RankBindingSource.DataMember = "Rank";
            this.RankBindingSource.DataSource = this.ChessDataSet;
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ChessPlayerViewDataSet";
            reportDataSource1.Value = this.ChessPlayerViewBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "CourseWork.reports.ChessPlayerReport.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(0, 0);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(0);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1043, 566);
            this.reportViewer.TabIndex = 0;
            // 
            // ChessPlayerTableAdapter
            // 
            this.ChessPlayerTableAdapter.ClearBeforeFill = true;
            // 
            // RankTableAdapter
            // 
            this.RankTableAdapter.ClearBeforeFill = true;
            // 
            // TournamentBindingSource
            // 
            this.TournamentBindingSource.DataMember = "Tournament";
            this.TournamentBindingSource.DataSource = this.ChessDataSet;
            // 
            // TournamentTableAdapter
            // 
            this.TournamentTableAdapter.ClearBeforeFill = true;
            // 
            // ChessPlayerViewTableAdapter
            // 
            this.ChessPlayerViewTableAdapter.ClearBeforeFill = true;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1043, 566);
            this.Controls.Add(this.reportViewer);
            this.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportForm";
            this.Text = "Отчёт";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChessPlayerViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessViewsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessPlayerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChessDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RankBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TournamentBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ChessPlayerBindingSource;
        private ChessDataSet ChessDataSet;
        private System.Windows.Forms.BindingSource RankBindingSource;
        private ChessDataSetTableAdapters.ChessPlayerTableAdapter ChessPlayerTableAdapter;
        private ChessDataSetTableAdapters.RankTableAdapter RankTableAdapter;
        private System.Windows.Forms.BindingSource TournamentBindingSource;
        private ChessDataSetTableAdapters.TournamentTableAdapter TournamentTableAdapter;
        private System.Windows.Forms.BindingSource ChessPlayerViewBindingSource;
        private ChessViewsDataSet ChessViewsDataSet;
        private ChessViewsDataSetTableAdapters.ChessPlayerViewTableAdapter ChessPlayerViewTableAdapter;
    }
}