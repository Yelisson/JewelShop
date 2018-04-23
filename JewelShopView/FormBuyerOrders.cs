using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using Microsoft.Reporting.WinForms;
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
    public partial class FormBuyerOrders : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IReportService service;

        public FormBuyerOrders(IReportService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonMakeReport_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePickerFrom.Value.ToShortDateString() +
                                            " по " + dateTimePickerTo.Value.ToShortDateString());
                reportViewer.LocalReport.SetParameters(parameter);

                var dataSource = service.GetBuyerOrders(new ReportBindingModel
                {
                    dateFrom = dateTimePickerFrom.Value,
                    dateTo = dateTimePickerTo.Value
                });
                BuyerOrdersModelBindingSource.DataSource = dataSource;
                ReportDataSource source = new ReportDataSource("DataSetOrders", dataSource);
                Console.Write(source);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    service.SaveBuyerOrders(new ReportBindingModel
                    {

                        fileName = sfd.FileName,
                        dateFrom = dateTimePickerFrom.Value,
                        dateTo = dateTimePickerTo.Value
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void FormBuyerOrders_Load(object sender, EventArgs e)
        {
            this.reportViewer.RefreshReport();
        }
    }
}
