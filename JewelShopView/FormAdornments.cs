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
    public partial class FormAdornments : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IAdornmentService service;

        public FormAdornments(IAdornmentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAdornment>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void FormAdornments_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<AdornmentViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridViewAdornments.DataSource = list;
                    dataGridViewAdornments.Columns[0].Visible = false;
                    dataGridViewAdornments.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdornments.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormAdornment>();
                form.Id = Convert.ToInt32(dataGridViewAdornments.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewAdornments.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewAdornments.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
