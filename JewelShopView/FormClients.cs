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
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<BuyerViewModel> list = Task.Run(() => APIClient.GetRequestData<List<BuyerViewModel>>("api/Buyer/GetList")).Result;
                if (list != null)
                {
                    dataGridViewBuyers.DataSource = list;
                    dataGridViewBuyers.Columns[0].Visible = false;
                    dataGridViewBuyers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormBuyer();
            form.ShowDialog();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewBuyers.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewBuyers.SelectedRows[0].Cells[0].Value);

                    Task task = Task.Run(() => APIClient.PostRequestData("api/Buyer/DelElement", new BuyerBindingModel { id = id }));

                    task.ContinueWith((prevTask) => MessageBox.Show("Запись удалена. Обновите список", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {

            if (dataGridViewBuyers.SelectedRows.Count == 1)
            {
                var form = new FormBuyer
                {
                    Id = Convert.ToInt32(dataGridViewBuyers.SelectedRows[0].Cells[0].Value)
                };
                form.ShowDialog();
            }
        }
    }
}
