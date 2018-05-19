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
    public partial class FormManyHangars : Form
    {
        public FormManyHangars()
        {
            InitializeComponent();
        }

        private void FormManyHangars_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<HangarViewModel> list = Task.Run(() => APIClient.GetRequestData<List<HangarViewModel>>("api/Hangar/GetList")).Result;
                if (list != null)
                {
                    dataGridViewHangars.DataSource = list;
                    dataGridViewHangars.Columns[0].Visible = false;
                    dataGridViewHangars.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormHangar();
            form.ShowDialog();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewHangars.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewHangars.SelectedRows[0].Cells[0].Value);

                    Task task = Task.Run(() => APIClient.PostRequestData("api/Hangar/DelElement", new BuyerBindingModel { id = id }));

                    task.ContinueWith((prevTask) => MessageBox.Show("Запись удалена. Обновите список", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewHangars.SelectedRows.Count == 1)
            {
                var form = new FormHangar
                {
                    Id = Convert.ToInt32(dataGridViewHangars.SelectedRows[0].Cells[0].Value)
                };
                form.ShowDialog();
            }
        }

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
