namespace JewelShopView
{
    partial class FormOrder
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
            this.labelBuyer = new System.Windows.Forms.Label();
            this.labelAdornment = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelSum = new System.Windows.Forms.Label();
            this.comboBoxBuyer = new System.Windows.Forms.ComboBox();
            this.comboBoxAdornment = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBuyer
            // 
            this.labelBuyer.AutoSize = true;
            this.labelBuyer.Location = new System.Drawing.Point(22, 39);
            this.labelBuyer.Name = "labelBuyer";
            this.labelBuyer.Size = new System.Drawing.Size(46, 13);
            this.labelBuyer.TabIndex = 0;
            this.labelBuyer.Text = "Клиент:";
            // 
            // labelAdornment
            // 
            this.labelAdornment.AutoSize = true;
            this.labelAdornment.Location = new System.Drawing.Point(22, 62);
            this.labelAdornment.Name = "labelAdornment";
            this.labelAdornment.Size = new System.Drawing.Size(54, 13);
            this.labelAdornment.TabIndex = 1;
            this.labelAdornment.Text = "Изделие:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(22, 86);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 2;
            this.labelCount.Text = "Количество:";
            // 
            // labelSum
            // 
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(22, 109);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(44, 13);
            this.labelSum.TabIndex = 3;
            this.labelSum.Text = "Сумма:";
            // 
            // comboBoxBuyer
            // 
            this.comboBoxBuyer.FormattingEnabled = true;
            this.comboBoxBuyer.Location = new System.Drawing.Point(91, 32);
            this.comboBoxBuyer.Name = "comboBoxBuyer";
            this.comboBoxBuyer.Size = new System.Drawing.Size(170, 21);
            this.comboBoxBuyer.TabIndex = 4;
            // 
            // comboBoxAdornment
            // 
            this.comboBoxAdornment.FormattingEnabled = true;
            this.comboBoxAdornment.Location = new System.Drawing.Point(91, 59);
            this.comboBoxAdornment.Name = "comboBoxAdornment";
            this.comboBoxAdornment.Size = new System.Drawing.Size(170, 21);
            this.comboBoxAdornment.TabIndex = 5;
            this.comboBoxAdornment.SelectedIndexChanged += new System.EventHandler(this.comboBoxAdornment_SelectedIndexChanged);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(91, 83);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(170, 20);
            this.textBoxCount.TabIndex = 6;
            this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_TextChanged);
            // 
            // textBoxSum
            // 
            this.textBoxSum.Location = new System.Drawing.Point(91, 109);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.ReadOnly = true;
            this.textBoxSum.Size = new System.Drawing.Size(170, 20);
            this.textBoxSum.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(96, 135);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(177, 135);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 179);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxAdornment);
            this.Controls.Add(this.comboBoxBuyer);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelAdornment);
            this.Controls.Add(this.labelBuyer);
            this.Name = "FormOrder";
            this.Text = "Сделать заказ";
            this.Load += new System.EventHandler(this.FormOrder_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBuyer;
        private System.Windows.Forms.Label labelAdornment;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.ComboBox comboBoxBuyer;
        private System.Windows.Forms.ComboBox comboBoxAdornment;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}