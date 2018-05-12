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
                var responseC = APIClient.GetRequest("api/Element/GetList");
                if (responseC.Result.IsSuccessStatusCode)
                {
                    List<ElementViewModel> list = APIClient.GetElement<List<ElementViewModel>>(responseC);
                    if (list != null)
                    {
                        comboBoxElement.DisplayMember = "elementName";
                        comboBoxElement.ValueMember = "id";
                        comboBoxElement.DataSource = list;
                        comboBoxElement.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
                var responseS = APIClient.GetRequest("api/Hangar/GetList");
                if (responseS.Result.IsSuccessStatusCode)
                {
                    List<HangarViewModel> list = APIClient.GetElement<List<HangarViewModel>>(responseS);
                    if (list != null)
                    {
                        comboBoxHangar.DisplayMember = "hangarName";
                        comboBoxHangar.ValueMember = "id";
                        comboBoxHangar.DataSource = list;
                        comboBoxHangar.SelectedItem = null;
                    }
                }
                else
                {
                    throw new Exception(APIClient.GetError(responseC));
                }
            }
            catch (Exception ex)
            {
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
                var response = APIClient.PostRequest("api/Main/PutComponentOnStock", new HangarElementBindingModel
                {
                    elementId = Convert.ToInt32(comboBoxElement.SelectedValue),
                    hangarId = Convert.ToInt32(comboBoxHangar.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
    }
}
