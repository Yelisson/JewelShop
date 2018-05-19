using JewelShopService.BindingModels;
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

namespace JewelShopView
{
    public partial class FormJewelShop : Form
    {
        public FormJewelShop()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            try
            {
                List<ProdOrderViewModel> list = Task.Run(() => APIClient.GetRequestData<List<ProdOrderViewModel>>("api/Main/GetList")).Result;
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
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = new FormOrder();
            form.ShowDialog();
        }

        private void buttonGiveOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                var form = new FormOrderInWork
                {
                    Id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value)
                };
                form.ShowDialog();
            }
        }

        private void buttonOrderIsReady_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);

                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/FinishOrder", new ProdOrderBindingModel
                {
                    id = id
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Статус заказа изменен. Обновите список", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);

                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void buttonOrderIsPayed_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewOrders.SelectedRows[0].Cells[0].Value);

                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PayOrder", new ProdOrderBindingModel
                {
                    id = id
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Статус заказа изменен. Обновите список", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);

                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void buttonNewList_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void покупателиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new FormClients();
            form.ShowDialog();
        }

        private void элементыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new FormElements();
            form.ShowDialog();
        }

        private void украшенияToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new FormAdornments();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new FormManyHangars();
            form.ShowDialog();
        }

        private void сотрудникиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new FormCustomers();
            form.ShowDialog();
        }

        private void добавитьКомпонентыНаСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormPutOnHangar();
            form.ShowDialog();
        }

        private void прайсУкрашенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SaveAdornmentPrice", new ReportBindingModel
                {
                    fileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
                TaskContinuationOptions.OnlyOnRanToCompletion);

                task.ContinueWith((prevTask) =>
                {
                    var ex = (Exception)prevTask.Exception;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void загруженностьСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormHangarsLoad();
            form.ShowDialog();
        }

        private void заказыПокупателейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormBuyerOrders();
            form.ShowDialog();
        }
    }
}
