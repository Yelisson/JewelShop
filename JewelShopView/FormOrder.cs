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
    public partial class FormOrder : Form
    {
        public FormOrder()
        {
            InitializeComponent();
        }

        private void CalcSum()
        {
            if (comboBoxAdornment.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxAdornment.SelectedValue);
                    var responseP = APIClient.GetRequest("api/Adornment/Get/" + id);
                    if (responseP.Result.IsSuccessStatusCode)
                    {
                        AdornmentViewModel product = APIClient.GetElement<AdornmentViewModel>(responseP);
                        int count = Convert.ToInt32(textBoxCount.Text);
                        textBoxSum.Text = (count * (int)product.cost).ToString();
                    }
                    else
                    {
                        throw new Exception(APIClient.GetError(responseP));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxBuyer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxAdornment.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var response = APIClient.PostRequest("api/Main/CreateOrder", new ProdOrderBindingModel
                {
                    buyerId = Convert.ToInt32(comboBoxBuyer.SelectedValue),
                    adornmentId = Convert.ToInt32(comboBoxAdornment.SelectedValue),
                    count = Convert.ToInt32(textBoxCount.Text),
                    sum = Convert.ToInt32(textBoxSum.Text)
                });
                if (response.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void comboBoxAdornment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void FormOrder_Load_1(object sender, EventArgs e)
        {
            try
            {
                var responseC = APIClient.GetRequest("api/Buyer/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<BuyerViewModel> list = APIClient.GetElement<List<BuyerViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxBuyer.DisplayMember = "buyerFIO";
                        comboBoxBuyer.ValueMember = "id";
                        comboBoxBuyer.DataSource = list;
                        comboBoxBuyer.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseP = APIClient.GetRequest("api/Adornment/GetList");
                if (responseP.Result.IsSuccessStatusCode)
                {
                    List<AdornmentViewModel> list = APIClient.GetElement<List<AdornmentViewModel>>(responseP);
                    if (list != null)
                    {
                        comboBoxAdornment.DisplayMember = "adornmentName";
                        comboBoxAdornment.ValueMember = "id";
                        comboBoxAdornment.DataSource = list;
                        comboBoxAdornment.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseP));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
