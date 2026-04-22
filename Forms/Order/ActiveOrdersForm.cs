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
    public partial class ActiveOrdersForm : Form
    {
        public ActiveOrdersForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ActiveOrdersForm_Load);
        }

        private void ActiveOrdersForm_Load(object sender, EventArgs e)
        {

            Configurator configurator = new Configurator();

            DataTable dTableOrders = configurator.LoadOrders('A');

            for(int i = 0; i < dTableOrders.Rows.Count; i++)
            {
                int order_ID = Convert.ToInt32(dTableOrders.Rows[i].ItemArray[0]);
                ActiveOrderPreviewForm fa = new ActiveOrderPreviewForm(order_ID);
                fa.TopLevel = false;
                fa.Show();
                flowLayoutPanel1.Controls.Add(fa);
                fa.parent = "active";
            }


        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            OrderForm fOrder = new OrderForm(-1, "add");
            fOrder.Show();
        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.Controls.Clear();
            this.InitializeComponent();
            this.ActiveOrdersForm_Load(e, e);
            //this.Show();
        }
    }
}
