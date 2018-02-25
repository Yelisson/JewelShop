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
            if (comboBox2.SelectedValue != null && !string.IsNullOrEmpty(textBox1.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBox2.SelectedValue);
                    AdornmentViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBox1.Text);
                    textBox2.Text = (count * product.cost).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox2.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new ProdOrderBindingModel
                {
                    buyerId = Convert.ToInt32(comboBox1.SelectedValue),
                    adornmentId = Convert.ToInt32(comboBox2.SelectedValue),
                    count = Convert.ToInt32(textBox1.Text),
                    sum = Convert.ToInt32(textBox2.Text)
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                    comboBox1.DisplayMember = "buyerFIO";
                    comboBox1.ValueMember = "id";
                    comboBox1.DataSource = listC;
                    comboBox1.SelectedItem = null;
                }
                List<AdornmentViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBox2.DisplayMember = "adornmentName";
                    comboBox2.ValueMember = "id";
                    comboBox2.DataSource = listP;
                    comboBox2.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
