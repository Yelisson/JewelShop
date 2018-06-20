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
using Unity;
using Unity.Attributes;

namespace JewelShopView
{
    public partial class FormOrder : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IBuyerService serviceC;

        private readonly IAdornmentService serviceP;

        private readonly IMainService serviceM;

        public FormOrder(IBuyerService serviceC, IAdornmentService serviceP, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }

        private void CalcSum()
        {
            if (comboBoxAdornment.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxAdornment.SelectedValue);
                    AdornmentViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count *(int) product.cost).ToString();
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
                serviceM.CreateOrder(new ProdOrderBindingModel
                {
                    buyerId = Convert.ToInt32(comboBoxBuyer.SelectedValue),
                    adornmentId = Convert.ToInt32(comboBoxAdornment.SelectedValue),
                    count = Convert.ToInt32(textBoxCount.Text),
                    sum = Convert.ToInt32(textBoxSum.Text)
                });
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
                List<BuyerViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxBuyer.DisplayMember = "buyerFIO";
                    comboBoxBuyer.ValueMember = "id";
                    comboBoxBuyer.DataSource = listC;
                    comboBoxBuyer.SelectedItem = null;
                }
                List<AdornmentViewModel> listP = serviceP.GetList();
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
