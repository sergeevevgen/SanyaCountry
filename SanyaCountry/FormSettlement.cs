using SanyaCountry;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.BusinessLogicsContracts;
using SanyaCountryLogicContracts.Enums;
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

namespace SanyaCountryView
{
    public partial class FormSettlement : Form
    {
        public int Id { set { id = value; } }
        private readonly ISettlementLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> buildings;

        public FormSettlement(ISettlementLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            comboBoxType.DataSource = Enum.GetValues(typeof(SettlementType))
                            .Cast<SettlementType>()
                            .Select(t => new { Type = Enum.GetName(typeof(SettlementType), t), Value = (int)t })
                            .ToList();
            comboBoxType.DisplayMember = "Type";
            comboBoxType.ValueMember = "Type";
        }

        private void FormSettlement_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var settlement = _logic.Read(new SettlementBindingModel
                    { Id = id.Value })?[0];
                    if (settlement != null)
                    {
                        textBoxName.Text = settlement.Name;

                        comboBoxType.SelectedItem = new { Type = settlement.Type, Value = (int)Enum.Parse(typeof(SettlementType), settlement.Type) };
                        buildings = settlement.Buildings;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
            else
            {
                comboBoxType.SelectedItem = null;
                buildings = new Dictionary<int, (string, int)>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (buildings != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var b in buildings)
                    {
                        dataGridView.Rows.Add(new object[] { b.Key, b.Value.Item1,
                        b.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormSettlementBuilding>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (buildings.ContainsKey(form.Id))
                {
                    buildings[form.Id] = (form.BuildingName, form.Count);
                }
                else
                {
                    buildings.Add(form.Id, (form.BuildingName, form.Count));
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormSettlementBuilding>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = buildings[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    buildings[form.Id] = (form.BuildingName, form.Count);
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        buildings.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxType.SelectedValue == null)
            {
                MessageBox.Show("Заполните тип", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (buildings == null || buildings.Count == 0)
            {
                MessageBox.Show("Заполните строения", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new SettlementBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text,
                    Type = (SettlementType)Enum.Parse(typeof(SettlementType), comboBoxType.SelectedValue.ToString()),
                    Buildings = buildings
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
