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
                var response = APIClient.GetRequest("api/Buyer/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    List<BuyerViewModel> list = APIClient.GetElement<List<BuyerViewModel>>(response);
                    if (list != null)
                    {
                        dataGridViewBuyers.DataSource = list;
                        dataGridViewBuyers.Columns[0].Visible = false;
                        dataGridViewBuyers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadData();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormBuyer();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewBuyers.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewBuyers.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        var response = APIClient.PostRequest("api/Buyer/DelElement", new BuyerBindingModel { id = id });
                        if (!response.Result.IsSuccessStatusCode)
                        {
                            throw new Exception(APIClient.GetError(response));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewBuyers.SelectedRows.Count == 1)
            {
                var form = new FormBuyer();
                form.Id = Convert.ToInt32(dataGridViewBuyers.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
    }
}
