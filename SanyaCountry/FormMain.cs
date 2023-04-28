using SanyaCountryBusinessLogic.BusinessLogic;
using SanyaCountryContracts.BindingModels;
using SanyaCountryContracts.BusinessLogicsContracts;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.BusinessLogicsContracts;
using SanyaCountryView;
using Unity;

namespace SanyaCountry
{
    public partial class FormMain : Form
    {
        private readonly ISettlementLogic _settlementLogic;
        private readonly IReportLogic _reportLogic;
        public FormMain(ISettlementLogic settlementLogic, IReportLogic reportLogic)
        {
            InitializeComponent();
            _settlementLogic = settlementLogic;
            _reportLogic = reportLogic;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _settlementLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormBuildings>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormSettlement>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormSettlement>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("������� ������", "������", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                    Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _settlementLogic.Delete(new SettlementBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= DateTime.Now)
            {
                MessageBox.Show("���� ������ ������ ���� ������ ����������� ����", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _reportLogic.SaveSettlementBuildingsToPdfFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        DateFrom = dateTimePickerFrom.Value,
                    });
                    MessageBox.Show("���������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= DateTime.Now)
            {
                MessageBox.Show("���� ������ ������ ���� ������ ����������� ����", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            using var dialog = new SaveFileDialog { Filter = "xml|*.xml" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _reportLogic.SaveOtchet(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        DateFrom = dateTimePickerFrom.Value
                    });
                    MessageBox.Show("����� ������", "���������",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
    }
}