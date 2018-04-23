namespace JewelShopView
{
    partial class FormHangarsLoad
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
            this.dataGridViewHangars = new System.Windows.Forms.DataGridView();
            this.ColumnHangar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnElement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHangars)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHangars
            // 
            this.dataGridViewHangars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHangars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnHangar,
            this.ColumnElement,
            this.ColumnCount});
            this.dataGridViewHangars.Location = new System.Drawing.Point(12, 42);
            this.dataGridViewHangars.Name = "dataGridViewHangars";
            this.dataGridViewHangars.Size = new System.Drawing.Size(364, 309);
            this.dataGridViewHangars.TabIndex = 0;
            // 
            // ColumnHangar
            // 
            this.ColumnHangar.HeaderText = "Склад";
            this.ColumnHangar.Name = "ColumnHangar";
            // 
            // ColumnElement
            // 
            this.ColumnElement.HeaderText = "Элемент";
            this.ColumnElement.Name = "ColumnElement";
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.Name = "ColumnCount";
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(28, 12);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(148, 23);
            this.buttonSaveToExcel.TabIndex = 1;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.buttonSaveToExcel_Click);
            // 
            // FormHangarsLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 363);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Controls.Add(this.dataGridViewHangars);
            this.Name = "FormHangarsLoad";
            this.Text = "FormHangarsLoad";
            this.Load += new System.EventHandler(this.FormHangarsLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHangars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHangars;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHangar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnElement;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
        private System.Windows.Forms.Button buttonSaveToExcel;
    }
}