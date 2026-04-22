using RestaurantPOS.Entities;
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
    public partial class ActiveOrderPreviewForm : Form
    {
        private int order_ID;
        public string parent = null;
        private int table_id;

        public ActiveOrderPreviewForm(int order_ID)
        {
            InitializeComponent();
            this.order_ID = order_ID;
            this.Load += new System.EventHandler(this.ActiveOrderPreview_Load);
        }

        private void ActiveOrderPreview_Load(object sender, EventArgs e)
        {


            Configurator configurator = new Configurator();

            DataTable dTableActiveOrder = configurator.LoadOrderDetailsByOrderID(order_ID);

            
            for (int i = 0; i < dTableActiveOrder.Rows.Count; i++)
            {
                string[] row = {Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[2]), //
                    Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[3])}; //
                this.dataGridView1.Rows.Add(row);

                table_id = Convert.ToInt32(dTableActiveOrder.Rows[i].ItemArray[1]);
            }

            labelTable.Text += " " + Convert.ToString(table_id);
            labelOrder.Text += " " + Convert.ToString(order_ID);

        }

        private void labelTable_Click(object sender, EventArgs e)
        {
            if(parent == "active")
            {
                OrderForm fOrder = new OrderForm(table_id, "view");
                fOrder.ShowDialog();
            }
            if(parent == "closed")
            {
                OrderForm fOrder = new OrderForm(order_ID, "archiveView");
                fOrder.ShowDialog();
            }
        }

        private void labelOrder_Click(object sender, EventArgs e)
        {
            if (parent == "active")
            {
                OrderForm fOrder = new OrderForm(table_id, "view");
                fOrder.ShowDialog();
            }
            if (parent == "closed")
            {
                OrderForm fOrder = new OrderForm(order_ID, "archiveView");
                fOrder.ShowDialog();
            }
        }
    }
}
