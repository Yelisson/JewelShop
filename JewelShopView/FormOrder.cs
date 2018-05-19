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
                    AdornmentViewModel product = Task.Run(() => APIClient.GetRequestData<AdornmentViewModel>("api/Adornment/Get/" + id)).Result;
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * (int)product.cost).ToString();
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
            int clientId = Convert.ToInt32(comboBoxBuyer.SelectedValue);
            int productId = Convert.ToInt32(comboBoxAdornment.SelectedValue);
            int count = Convert.ToInt32(textBoxCount.Text);
            int sum = Convert.ToInt32(textBoxSum.Text);
            Task task = Task.Run(() => APIClient.PostRequestData("api/Main/CreateOrder", new ProdOrderBindingModel
            {
                buyerId = clientId,
                adornmentId = productId,
                count = count,
                sum = sum
            }));

            task.ContinueWith((prevTask) => MessageBox.Show("Сохранение прошло успешно. Обновите список", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
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
                List<BuyerViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<BuyerViewModel>>("api/Buyer/GetList")).Result;
                if (listC != null)
                {
                    comboBoxBuyer.DisplayMember = "buyerFIO";
                    comboBoxBuyer.ValueMember = "id";
                    comboBoxBuyer.DataSource = listC;
                    comboBoxBuyer.SelectedItem = null;
                }

                List<AdornmentViewModel> listP = Task.Run(() => APIClient.GetRequestData<List<AdornmentViewModel>>("api/Adornment/GetList")).Result;
                if (listP != null)
                {
                    comboBoxAdornment.DisplayMember = "adornmentName";
                    comboBoxAdornment.ValueMember = "id";
                    comboBoxAdornment.DataSource = listP;
                    comboBoxAdornment.SelectedItem = null;
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
    }
}
