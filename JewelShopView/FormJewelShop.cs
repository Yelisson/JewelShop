using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace JewelShopView
{
    public partial class FormJewelShop : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public FormJewelShop(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<ProdOrderViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewOrders.DataSource = list;
                    dataGridViewOrders.Columns[0].Visible = false;
                    dataGridViewOrders.Columns[1].Visible = false;
                    dataGridViewOrders.Columns[3].Visible = false;
                    dataGridViewOrders.Columns[5].Visible = false;
                    dataGridViewOrders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonGiveOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormOrderInWork>();
                form.Id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonOrderIsReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.FinishOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOrderIsPayed_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);
                try
                {
                    service.PayOrder(id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonNewList_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void покупателиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void элементыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormElements>();
            form.ShowDialog();
        }

        private void украшенияToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAdornments>();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormManyHangars>();
            form.ShowDialog();
        }

        private void сотрудникиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCustomers>();
            form.ShowDialog();
        }

        private void добавитьКомпонентыНаСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPutOnHangar>();
            form.ShowDialog();
        }
    }
}
