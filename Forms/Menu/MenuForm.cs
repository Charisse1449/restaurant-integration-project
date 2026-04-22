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
    public partial class MenuForm : Form
    {
        private int role;
        private string action;
        public string menuItemName;
        public int menuItem_ID;

        public MenuForm(int role, string action)
        {
            InitializeComponent();
            this.role = role;
            this.action = action;
            this.Load += new System.EventHandler(this.MenuForm_Load);
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            DataTable dTableSalads = configurator.LoadMenuItemsByType("salad");
            MenuButtons_Load(dTableSalads, panelSalads);

            DataTable dTableAppetizers = configurator.LoadMenuItemsByType("appetizer");
            MenuButtons_Load(dTableAppetizers, panelAppetizers);

            DataTable dTableMainDeshes = configurator.LoadMenuItemsByType("main");
            MenuButtons_Load(dTableMainDeshes, panelMainDishes);

            DataTable dTablePizza = configurator.LoadMenuItemsByType("pizza");
            MenuButtons_Load(dTablePizza, panelPizza);

            DataTable dTableDesserts = configurator.LoadMenuItemsByType("dessert");
            MenuButtons_Load(dTableDesserts, panelDesserts);

            DataTable dTableDrinks = configurator.LoadMenuItemsByType("drink");
            MenuButtons_Load(dTableDrinks, panelDrinks);

            if (role == 1)
            {
                buttonAdd.Visible = true;
            }
            else
            {
                buttonAdd.Visible = false;
            }

        }

        private void MenuButtons_Load(DataTable dTable, Panel panel)
        {
            int btnHeight = 45;
            if (dTable.Rows.Count > 0)
            {
                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    Button btn = new Button();
                    btn.Text = Convert.ToString(dTable.Rows[i].ItemArray[1]); //name
                    btn.Tag = Convert.ToString(dTable.Rows[i].ItemArray[0]); //id
                    btn.Size = new Size(136, 45);
                    btn.Location = new Point(0, btnHeight * i);
                    btn.BackColor = Color.White;
                    btn.ForeColor = Color.DarkSlateGray;
                    btn.Font = new Font("Malgun Gothic", 11);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.LightGray;
                    //btn.AutoSize = true;
                    //btn.AutoSizeMode = AutoSizeMode.GrowOnly;
                    //btn.MaximumSize = new Size(136, 90);

                    panel.Controls.Add(btn);

                    btn.Click += new EventHandler(btn_Click);

                }
            }

            
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (action == "view")
            {
                MenuItemForm fMenuItem = new MenuItemForm(button.Text, role);
                fMenuItem.FormClosed += new FormClosedEventHandler(child_FormClosed);
                fMenuItem.ShowDialog();
            } 
            else if(action == "add")
            {
                menuItemName = button.Text;
                menuItem_ID = Convert.ToInt32(button.Tag);
                this.Close();
            }
            
        }

        private void labelSalads_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
        
            MenuItemForm fMenuItem = new MenuItemForm(null, role);
            fMenuItem.FormClosed += new FormClosedEventHandler(child_FormClosed);
            fMenuItem.Show();

        }

        private void fMenuItem_Closing(object sender, FormClosingEventArgs e)
        {
            this.Refresh();
        }

        private void labelDrinks_Click(object sender, EventArgs e)
        {

        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.Controls.Clear();
            this.InitializeComponent();
            this.MenuForm_Load(e, e);
            //this.Show();
        }
    }
}
