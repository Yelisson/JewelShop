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
    public partial class FormHangarsLoad : Form
    {
        public FormHangarsLoad()
        {
            InitializeComponent();
        }

        private void FormHangarsLoad_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewHangars.Rows.Clear();
                foreach (var elem in Task.Run(() => APIClient.GetRequestData<List<HangarsLoadViewModel>>("api/Report/GetHangarsLoad")).Result)
                {
                    dataGridViewHangars.Rows.Add(new object[] { elem.hangarName, "", "" });
                    foreach (var listElem in elem.Elements)
                    {
                        dataGridViewHangars.Rows.Add(new object[] { "", listElem.ElementName, listElem.Count });
                    }
                    dataGridViewHangars.Rows.Add(new object[] { "Итого", "", elem.totalCount });
                    dataGridViewHangars.Rows.Add(new object[] { });
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                Task task = Task.Run(() => APIClient.PostRequestData("api/Report/SaveHangarsLoad", new ReportBindingModel
                {
                    fileName = fileName
                }));

                task.ContinueWith((prevTask) => MessageBox.Show("Выполнено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information),
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
}
