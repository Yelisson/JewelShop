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
    public partial class FormAdornmentElement : Form
    {
        public AdornmentElementViewModel Model { set { model = value; } get { return model; } }
        private AdornmentElementViewModel model;

        public FormAdornmentElement()
        {
            InitializeComponent();
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new AdornmentElementViewModel
                    {
                        elementId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        elementName = comboBoxComponent.Text,
                        count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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

        private void FormAdornmentElement_Load(object sender, EventArgs e)
        {
            try
            {
                var response = APIClient.GetRequest("api/Element/GetList");
                if (response.Result.IsSuccessStatusCode)
                {
                    comboBoxComponent.DisplayMember = "elementName";
                    comboBoxComponent.ValueMember = "id";
                    comboBoxComponent.DataSource = APIClient.GetElement<List<ElementViewModel>>(response);
                    comboBoxComponent.SelectedItem = null;
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
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.elementId;
                textBoxCount.Text = model.count.ToString();
            }
        }
    }
}
