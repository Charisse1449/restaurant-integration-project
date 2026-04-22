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

            string username = this.textBoxUsername.Text.Trim();
            string password = this.textBoxPassword.Text.Trim();

            // ✅ VALIDATION FIRST
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please input username and password first.");
                return;
            }
            else if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please input username first.");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please input password first.");
                return;
            }

            // ✅ ONLY CALL DATABASE IF VALID
            Configurator configurator = new Configurator();
            int role = configurator.CheckLoginAndRole(username, password);

            if (role != 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm(role);
                mainForm.ShowDialog();
                this.Close();
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
