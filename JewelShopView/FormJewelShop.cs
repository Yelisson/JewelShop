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
                var response = APIClient.GetRequest("api/Main/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<ProdOrderViewModel> list = APIClient.GetElement<List<ProdOrderViewModel>>(response);
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
                else
                {
                    throw new Exception(APIClient.GetError(response));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
     
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = new FormOrder();
            form.ShowDialog();
            LoadData();
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
                    var response = APIClient.PostRequest("api/Main/FinishOrder", new ProdOrderBindingModel
                    {
                        id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
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
                    var response = APIClient.PostRequest("api/Main/PayOrder", new ProdOrderBindingModel
                    {
                        id = id
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        LoadData();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
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
                try
                {
                    var response = APIClient.PostRequest("api/Report/SaveAdornmentPrice", new ReportBindingModel
                    {
                        fileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(response));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
