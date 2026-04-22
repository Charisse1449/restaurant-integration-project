using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Forms
{
    public partial class MenuItemForm : Form
    {
        string menuItemName;
        int menuItem_ID;
        int role;

        public MenuItemForm(string menuItemName, int role)
        {
            InitializeComponent();
            this.menuItemName = menuItemName;
            this.role = role;
            this.Load += new System.EventHandler(this.MenuItemForm_Load);
            RoleCheck(role);
        }

        private void MenuItemForm_Load(object sender, EventArgs e)
        {
            if (menuItemName == null)
            {
                buttonEdit.Visible = false;
                buttonDelete.Visible = false;
                buttonAdd.Visible = true;

                textBoxName.ReadOnly = false;
                textBoxType.ReadOnly = false;
                textBoxType.Visible = false;

                comboBoxType.Visible = true;
                comboBoxType.Items.Add("Salad");
                comboBoxType.Items.Add("Appetizer");
                comboBoxType.Items.Add("Main");
                comboBoxType.Items.Add("Pizza");
                comboBoxType.Items.Add("Dessert");
                comboBoxType.Items.Add("Drink");

                textBoxPrice.ReadOnly = false;
                textBoxQuantity.ReadOnly = false;
                richTextBoxDescription.ReadOnly = false;

            }
            else
            {

                Configurator configurator = new Configurator();

                Entities.MenuItem menuItem = configurator.LoadMenuItemByName(menuItemName);

                menuItem_ID = menuItem.MenuItem_ID;

                textBoxName.Text = menuItem.Name;
                textBoxType.Text = menuItem.Type;
                textBoxPrice.Text = Convert.ToString(menuItem.Price);
                textBoxQuantity.Text = menuItem.Quantity;
                richTextBoxDescription.Text = menuItem.Description;

                
            }
            buttonSave.Visible = false;
        }

        private void RoleCheck (int role)
        {
            if (role == 1 && menuItemName != null)
            {
                buttonEdit.Visible = true;
                buttonDelete.Visible = true;
                buttonAdd.Visible = false;
                comboBoxType.Visible = false;
            }
            else
            {
                buttonEdit.Visible = false;
                buttonDelete.Visible = false;
                buttonAdd.Visible = false;
                comboBoxType.Visible = false;
            }
        }

        private void labelPrice_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            buttonSave.Visible = true;
            buttonDelete.Visible = false;
            buttonEdit.Visible = false;
            textBoxName.ReadOnly = false;
            textBoxType.ReadOnly = false;
            textBoxPrice.ReadOnly = false;
            textBoxQuantity.ReadOnly = false;
            richTextBoxDescription.ReadOnly = false;
            this.Update();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            if (string.IsNullOrEmpty(this.textBoxName.Text) ||
                string.IsNullOrEmpty(this.textBoxType.Text) ||
                string.IsNullOrEmpty(this.textBoxPrice.Text) ||
                string.IsNullOrEmpty(this.textBoxQuantity.Text) ||
                string.IsNullOrEmpty(this.richTextBoxDescription.Text))
            {
                MessageBox.Show("Please fill all fields.");
            }
            else
            {

                configurator.UpdateMenuItem(menuItem_ID,
                    this.textBoxName.Text,
                    this.textBoxType.Text,
                    Convert.ToDouble(this.textBoxPrice.Text),
                    this.textBoxQuantity.Text,
                    this.richTextBoxDescription.Text);

                MessageBox.Show("Succesfully edited.");
                this.Close();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                Configurator configurator = new Configurator();
                configurator.DeleteMenuItem(menuItem_ID);

                MessageBox.Show("Succesfully deleted.");
                this.Close();

            }
            if (res == DialogResult.Cancel)
            {
                
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            Configurator configurator = new Configurator();

            if (string.IsNullOrEmpty(this.textBoxName.Text) ||
                string.IsNullOrEmpty(this.comboBoxType.Text) ||
                string.IsNullOrEmpty(this.textBoxPrice.Text) ||
                string.IsNullOrEmpty(this.textBoxQuantity.Text) ||
                string.IsNullOrEmpty(this.richTextBoxDescription.Text))
            {
                MessageBox.Show("Please fill all fields.");
            }
            else
            {

                configurator.CreateMenuItem(this.textBoxName.Text,
                    Convert.ToString(this.comboBoxType.SelectedItem),
                    Convert.ToDouble(this.textBoxPrice.Text),
                    this.textBoxQuantity.Text,
                    this.richTextBoxDescription.Text);

                MessageBox.Show("New Menu Item created.");
                this.Close();
                
            }

            
            
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
