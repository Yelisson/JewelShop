namespace JewelShopView
{
    partial class FormBuyerOrders
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
            this.BuyerOrdersModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.labelFrom = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.buttonMakeReport = new System.Windows.Forms.Button();
            this.buttonPDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BuyerOrdersModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BuyerOrdersModelBindingSource
            // 
            this.BuyerOrdersModelBindingSource.DataSource = typeof(JewelShopService.ViewModels.BuyerOrdersModel);
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSetOrders";
            reportDataSource1.Value = this.BuyerOrdersModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "JewelShopView.ReportBuyerOrders.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(3, 39);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(576, 320);
            this.reportViewer.TabIndex = 0;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(29, 13);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(142, 20);
            this.dateTimePickerFrom.TabIndex = 1;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(212, 13);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(142, 20);
            this.dateTimePickerTo.TabIndex = 2;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(9, 19);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(14, 13);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "С";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(187, 19);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 13);
            this.labelTo.TabIndex = 4;
            this.labelTo.Text = "по";
            // 
            // buttonMakeReport
            // 
            this.buttonMakeReport.Location = new System.Drawing.Point(370, 10);
            this.buttonMakeReport.Name = "buttonMakeReport";
            this.buttonMakeReport.Size = new System.Drawing.Size(95, 23);
            this.buttonMakeReport.TabIndex = 5;
            this.buttonMakeReport.Text = "Сформировать";
            this.buttonMakeReport.UseVisualStyleBackColor = true;
            this.buttonMakeReport.Click += new System.EventHandler(this.buttonMakeReport_Click);
            // 
            // buttonPDF
            // 
            this.buttonPDF.Location = new System.Drawing.Point(471, 9);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(95, 23);
            this.buttonPDF.TabIndex = 6;
            this.buttonPDF.Text = "В pdf";
            this.buttonPDF.UseVisualStyleBackColor = true;
            this.buttonPDF.Click += new System.EventHandler(this.buttonPDF_Click);
            // 
            // FormBuyerOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 361);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.buttonMakeReport);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Controls.Add(this.reportViewer);
            this.Name = "FormBuyerOrders";
            this.Text = "FormBuyerOrders";
            this.Load += new System.EventHandler(this.FormBuyerOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BuyerOrdersModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource BuyerOrdersModelBindingSource;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Button buttonMakeReport;
        private System.Windows.Forms.Button buttonPDF;
    }
}