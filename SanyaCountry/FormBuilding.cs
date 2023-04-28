using SanyaCountryLogicContracts.BindingModels;
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
    public partial class FormBuilding : Form
    {
        public int Id { set { id = value; } }
        private readonly IBuildingLogic _logic;
        private int? id;
        public FormBuilding(IBuildingLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }

        private void FormBuilding_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var building = _logic.Read(new BuildingBindingModel { Id = id })?[0];
                    if (building != null)
                    {
                        textBoxName.Text = building.Name;
                        textBoxPrice.Text = building.Price.ToString();
                        textBoxSquare.Text = building.Square.ToString();
                        textBoxDateCreated.Text = building.Created.ToString();
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
                textBoxDateCreated.Visible = false;
                labelDateCreated.Visible = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new BuildingBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text,
                    Price = double.Parse(textBoxPrice.Text),
                    Square = double.Parse(textBoxSquare.Text),
                    Created = id.HasValue ? DateTime.Parse(textBoxDateCreated.Text) : null,
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
