using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
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
    public partial class FormHangarsLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportService service;
        public FormHangarsLoad(IReportService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormHangarsLoad_Load(object sender, EventArgs e)
        {
            try
            {
                var dict = service.GetHangarsLoad();
                if (dict != null)
                {
                    dataGridViewHangars.Rows.Clear();
                    foreach (var elem in dict)
                    {
                        dataGridViewHangars.Rows.Add(new object[] { elem.hangarName, "", "" });
                        foreach (var listElem in elem.Elements)
                        {
                            dataGridViewHangars.Rows.Add(new object[] { "", listElem.Item1, listElem.Item2 });
                        }
                        dataGridViewHangars.Rows.Add(new object[] { "Итого", "", elem.totalCount });
                        dataGridViewHangars.Rows.Add(new object[] { });
                    }
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
                    service.SaveHangarsLoad(new ReportBindingModel
                    {
                        fileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
