using SanyaCountryLogicContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaCountryView
{
    public partial class FormSettlementBuilding : Form
    {
        public int Id
        {
            get { return Convert.ToInt32(comboBoxBuilding.SelectedValue); }
            set { comboBoxBuilding.SelectedValue = value; }
        }

        public string BuildingName { get { return comboBoxBuilding.Text; } }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public FormSettlementBuilding(IBuildingLogic logic)
        {
            InitializeComponent();
            var list = logic.Read(null);
            if (list != null)
            {
                comboBoxBuilding.DisplayMember = "Name";
                comboBoxBuilding.ValueMember = "Id";
                comboBoxBuilding.DataSource = list;
                comboBoxBuilding.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxBuilding.SelectedValue == null)
            {
                MessageBox.Show("Выберите строение", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
