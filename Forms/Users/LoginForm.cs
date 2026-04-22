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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //if ((this.textBoxUsername.Text == "admin") && (this.textBoxPassword.Text == "admin"))
            //{
            //    this.Hide();
            //    MainForm mainForm = new MainForm("a");
            //    mainForm.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Wrong username or password.");
            //}

            string username = this.textBoxUsername.Text;
            string password = this.textBoxPassword.Text;

            Configurator configurator = new Configurator();

            int role = configurator.CheckLoginAndRole(username, password);

            if (role != 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm(role);
                mainForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong username or password.");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
