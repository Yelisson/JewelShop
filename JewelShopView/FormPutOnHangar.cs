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
    public partial class FormPutOnHangar : Form
    {

        public FormPutOnHangar()
        {
            InitializeComponent();
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<ElementViewModel> listC = Task.Run(() => APIClient.GetRequestData<List<ElementViewModel>>("api/Element/GetList")).Result;
                if (listC != null)
                {
                    comboBoxElement.DisplayMember = "elementName";
                    comboBoxElement.ValueMember = "id";
                    comboBoxElement.DataSource = listC;
                    comboBoxElement.SelectedItem = null;
                }

                List<HangarViewModel> listS = Task.Run(() => APIClient.GetRequestData<List<HangarViewModel>>("api/Hangar/GetList")).Result;
                if (listS != null)
                {
                    comboBoxHangar.DisplayMember = "hangarName";
                    comboBoxHangar.ValueMember = "id";
                    comboBoxHangar.DataSource = listS;
                    comboBoxHangar.SelectedItem = null;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxElement.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxHangar.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int componentId = Convert.ToInt32(comboBoxElement.SelectedValue);
                int stockId = Convert.ToInt32(comboBoxHangar.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                Task task = Task.Run(() => APIClient.PostRequestData("api/Main/PutComponentOnStock", new HangarElementBindingModel
                {
                    elementId = componentId,
                    hangarId = stockId,
                    Count = count
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Склад пополнен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
