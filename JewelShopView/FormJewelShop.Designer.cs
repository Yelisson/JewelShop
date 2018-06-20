namespace JewelShopView
{
    partial class FormJewelShop
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.элементыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.украшенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.складыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сотрудникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьКомпонентыНаСкладToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonGiveOrderInWork = new System.Windows.Forms.Button();
            this.buttonOrderIsReady = new System.Windows.Forms.Button();
            this.buttonOrderIsPayed = new System.Windows.Forms.Button();
            this.buttonNewList = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.добавитьКомпонентыНаСкладToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(829, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.покупателиToolStripMenuItem,
            this.элементыToolStripMenuItem,
            this.украшенияToolStripMenuItem,
            this.складыToolStripMenuItem,
            this.сотрудникиToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // покупателиToolStripMenuItem
            // 
            this.покупателиToolStripMenuItem.Name = "покупателиToolStripMenuItem";
            this.покупателиToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.покупателиToolStripMenuItem.Text = "Покупатели";
            this.покупателиToolStripMenuItem.Click += new System.EventHandler(this.покупателиToolStripMenuItem_Click_1);
            // 
            // элементыToolStripMenuItem
            // 
            this.элементыToolStripMenuItem.Name = "элементыToolStripMenuItem";
            this.элементыToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.элементыToolStripMenuItem.Text = "Элементы";
            this.элементыToolStripMenuItem.Click += new System.EventHandler(this.элементыToolStripMenuItem_Click_1);
            // 
            // украшенияToolStripMenuItem
            // 
            this.украшенияToolStripMenuItem.Name = "украшенияToolStripMenuItem";
            this.украшенияToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.украшенияToolStripMenuItem.Text = "Украшения";
            this.украшенияToolStripMenuItem.Click += new System.EventHandler(this.украшенияToolStripMenuItem_Click_1);
            // 
            // складыToolStripMenuItem
            // 
            this.складыToolStripMenuItem.Name = "складыToolStripMenuItem";
            this.складыToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.складыToolStripMenuItem.Text = "Склады";
            this.складыToolStripMenuItem.Click += new System.EventHandler(this.складыToolStripMenuItem_Click_1);
            // 
            // сотрудникиToolStripMenuItem
            // 
            this.сотрудникиToolStripMenuItem.Name = "сотрудникиToolStripMenuItem";
            this.сотрудникиToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.сотрудникиToolStripMenuItem.Text = "Сотрудники";
            this.сотрудникиToolStripMenuItem.Click += new System.EventHandler(this.сотрудникиToolStripMenuItem_Click_1);
            // 
            // добавитьКомпонентыНаСкладToolStripMenuItem
            // 
            this.добавитьКомпонентыНаСкладToolStripMenuItem.Name = "добавитьКомпонентыНаСкладToolStripMenuItem";
            this.добавитьКомпонентыНаСкладToolStripMenuItem.Size = new System.Drawing.Size(194, 20);
            this.добавитьКомпонентыНаСкладToolStripMenuItem.Text = "Добавить компоненты на склад";
            this.добавитьКомпонентыНаСкладToolStripMenuItem.Click += new System.EventHandler(this.добавитьКомпонентыНаСкладToolStripMenuItem_Click);
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 27);
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.Size = new System.Drawing.Size(623, 290);
            this.dataGridViewOrders.TabIndex = 1;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(667, 44);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(150, 23);
            this.buttonCreateOrder.TabIndex = 2;
            this.buttonCreateOrder.Text = "Создать заказ";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // buttonGiveOrderInWork
            // 
            this.buttonGiveOrderInWork.Location = new System.Drawing.Point(667, 105);
            this.buttonGiveOrderInWork.Name = "buttonGiveOrderInWork";
            this.buttonGiveOrderInWork.Size = new System.Drawing.Size(150, 23);
            this.buttonGiveOrderInWork.TabIndex = 3;
            this.buttonGiveOrderInWork.Text = "Отдать на выполнение";
            this.buttonGiveOrderInWork.UseVisualStyleBackColor = true;
            this.buttonGiveOrderInWork.Click += new System.EventHandler(this.buttonGiveOrderInWork_Click);
            // 
            // buttonOrderIsReady
            // 
            this.buttonOrderIsReady.Location = new System.Drawing.Point(667, 163);
            this.buttonOrderIsReady.Name = "buttonOrderIsReady";
            this.buttonOrderIsReady.Size = new System.Drawing.Size(150, 23);
            this.buttonOrderIsReady.TabIndex = 4;
            this.buttonOrderIsReady.Text = "Заказ готов";
            this.buttonOrderIsReady.UseVisualStyleBackColor = true;
            this.buttonOrderIsReady.Click += new System.EventHandler(this.buttonOrderIsReady_Click);
            // 
            // buttonOrderIsPayed
            // 
            this.buttonOrderIsPayed.Location = new System.Drawing.Point(667, 223);
            this.buttonOrderIsPayed.Name = "buttonOrderIsPayed";
            this.buttonOrderIsPayed.Size = new System.Drawing.Size(150, 23);
            this.buttonOrderIsPayed.TabIndex = 5;
            this.buttonOrderIsPayed.Text = "Заказ оплачен";
            this.buttonOrderIsPayed.UseVisualStyleBackColor = true;
            this.buttonOrderIsPayed.Click += new System.EventHandler(this.buttonOrderIsPayed_Click);
            // 
            // buttonNewList
            // 
            this.buttonNewList.Location = new System.Drawing.Point(667, 283);
            this.buttonNewList.Name = "buttonNewList";
            this.buttonNewList.Size = new System.Drawing.Size(150, 23);
            this.buttonNewList.TabIndex = 6;
            this.buttonNewList.Text = "Обновить список";
            this.buttonNewList.UseVisualStyleBackColor = true;
            this.buttonNewList.Click += new System.EventHandler(this.buttonNewList_Click);
            // 
            // FormJewelShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 329);
            this.Controls.Add(this.buttonNewList);
            this.Controls.Add(this.buttonOrderIsPayed);
            this.Controls.Add(this.buttonOrderIsReady);
            this.Controls.Add(this.buttonGiveOrderInWork);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.dataGridViewOrders);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormJewelShop";
            this.Text = "Ювелирная лавка";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem элементыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem украшенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem складыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сотрудникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьКомпонентыНаСкладToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonGiveOrderInWork;
        private System.Windows.Forms.Button buttonOrderIsReady;
        private System.Windows.Forms.Button buttonOrderIsPayed;
        private System.Windows.Forms.Button buttonNewList;
    }
}

