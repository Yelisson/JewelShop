using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JewelShopView
{
    public partial class FormAdornment : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        private List<AdornmentElementViewModel> productComponents;

        public FormAdornment()
        {
            InitializeComponent();
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
             if (id.HasValue)
            {
                try
                {
                    var product = Task.Run(() => APIClient.GetRequestData<AdornmentViewModel>("api/Adornment/Get/" + id.Value)).Result;
                    textBoxName.Text = product.adornmentName;
                    textBoxPrice.Text = product.cost.ToString();
                    productComponents = product.ProductComponent;
                    LoadData();
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
            else
            {
                productComponents = new List<AdornmentElementViewModel>();
            }
        }


        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridViewComponents.DataSource = null;
                    dataGridViewComponents.DataSource = productComponents;
                    dataGridViewComponents.Columns[0].Visible = false;
                    dataGridViewComponents.Columns[1].Visible = false;
                    dataGridViewComponents.Columns[2].Visible = false;
                    dataGridViewComponents.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormAdornmentElement();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.adornmentId = id.Value;
                    }
                    productComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewComponents.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        productComponents.RemoveAt(dataGridViewComponents.SelectedRows[0].Cells[0].RowIndex);
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
            if (dataGridViewComponents.SelectedRows.Count == 1)
            {
                var form = new FormAdornmentElement();
                form.Model = productComponents[dataGridViewComponents.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[dataGridViewComponents.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<AdornmentElementBindingModel> productComponentBM = new List<AdornmentElementBindingModel>();
            for (int i = 0; i < productComponents.Count; ++i)
            {
                productComponentBM.Add(new AdornmentElementBindingModel
                {
                    id = productComponents[i].id,
                    adornmentId = productComponents[i].adornmentId,
                    elementId = productComponents[i].elementId,
                    count = productComponents[i].count
                });
            }
            string name = textBoxName.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            Task task;
            if (id.HasValue)
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Adornment/UpdElement", new AdornmentBindingModel
                {
                    id = id.Value,
                    adornmentName = name,
                    cost = price,
                    AdornmentComponent = productComponentBM
                }));
            }
            else
            {
                task = Task.Run(() => APIClient.PostRequestData("api/Adornment/AddElement", new AdornmentBindingModel
                {
                    adornmentName = name,
                    cost = price,
                    AdornmentComponent = productComponentBM
                }));
            }
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
    }
}
