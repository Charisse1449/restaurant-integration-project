using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.AddUserForm_Load);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            DataTable dTable = configurator.LoadRoles();

            this.comboBoxRole.DataSource = dTable;
            this.comboBoxRole.ValueMember = "id";
            this.comboBoxRole.DisplayMember = "name";

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(textBoxPassword.Text)|| string.IsNullOrEmpty(textBoxUsername.Text))
            {
                MessageBox.Show("Please fill all fields!");
            }
            else
            {
                Configurator configurator = new Configurator();

                configurator.AddNewUser(this.textBoxUsername.Text,
                    this.textBoxPassword.Text,
                    Convert.ToInt32(this.comboBoxRole.SelectedValue));

                MessageBox.Show("New user succesfully added.");

                this.Close();
            }
            
        }
    }
}