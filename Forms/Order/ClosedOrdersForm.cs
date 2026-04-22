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
    public partial class ClosedOrdersForm : Form
    {
        private string searchType; //table_id, staffMemberName
        private string criteria;

        public ClosedOrdersForm(string searchType, string criteria)
        {
            InitializeComponent();
            this.searchType = searchType; 
            this.criteria = criteria;
            this.Load += new System.EventHandler(this.ClosedOrdersForm_Load);
        }

        private void ClosedOrdersForm_Load(object sender, EventArgs e)
        {

            Configurator configurator = new Configurator();

            DataTable dTableOrders = new DataTable();

            if (searchType == null)
            {
                dTableOrders = configurator.LoadOrders('C');
            }
            else if (searchType == "table_id")
            {
                dTableOrders = configurator.SearchOrdersArchiveByTable_ID(Convert.ToInt32(criteria));
            } 
            else if (searchType == "staffMemberName")
            {
                dTableOrders = configurator.SearchOrdersArchiveByStaffMemberName(criteria);
            }
            

            for (int i = 0; i < dTableOrders.Rows.Count; i++)
            {
                int order_ID = Convert.ToInt32(dTableOrders.Rows[i].ItemArray[0]);
                ActiveOrderPreviewForm fa = new ActiveOrderPreviewForm(order_ID);
                fa.TopLevel = false;
                fa.Show();
                flowLayoutPanel1.Controls.Add(fa);
                fa.parent = "closed";
            }
        }
    }
}
