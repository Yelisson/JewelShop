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
    public partial class FormPutOnHangar : Form
    {

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IHangarService serviceS;

        private readonly IElementService serviceC;

        private readonly IMainService serviceM;

        public FormPutOnHangar(IHangarService serviceS, IElementService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<HangarViewModel> listC = serviceS.GetList();
                if (listC != null)
                {
                    comboBoxHangar.DisplayMember = "hangarName";
                    comboBoxHangar.ValueMember = "id";
                    comboBoxHangar.DataSource = listC;
                    comboBoxHangar.SelectedItem = null;
                }
                List<ElementViewModel> listS = serviceC.GetList();
                if (listS != null)
                {
                    comboBoxElement.DisplayMember = "elementName";
                    comboBoxElement.ValueMember = "id";
                    comboBoxElement.DataSource = listS;
                    comboBoxElement.SelectedItem = null;
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
            if (comboBoxHangar.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxElement.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.PutComponentOnStock(new HangarElementBindingModel
                {
                    elementId = Convert.ToInt32(comboBoxElement.SelectedValue),
                    hangarId = Convert.ToInt32(comboBoxHangar.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
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
    }
}
