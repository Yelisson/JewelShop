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
                var response = APIClient.GetRequest("api/Report/GetHangarsLoad");
                if (response.Result.IsSuccessStatusCode)
                {
                    dataGridViewHangars.Rows.Clear();
                    foreach (var elem in APIClient.GetElement<List<HangarsLoadViewModel>>(response))
                    {
                        dataGridViewHangars.Rows.Add(new object[] { elem.hangarName, "", "" });
                        foreach (var listElem in elem.Elements)
                        {
                            dataGridViewHangars.Rows.Add(new object[] { "", listElem.ElementName, listElem.Count });
                            //dataGridViewHangars.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridViewHangars.Rows.Add(new object[] { "Итого", "", elem.totalCount });
                        dataGridViewHangars.Rows.Add(new object[] { });
                    }
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

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var response = APIClient.PostRequest("api/Report/SaveHangarsLoad", new ReportBindingModel
                    {
                        fileName = sfd.FileName
                    });
                    if (response.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
    }
}
