using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Forms
{
    public partial class TablesForm : Form
    {
        List<Button> activeTableButtons;

        public TablesForm()
        {
            InitializeComponent();
            this.activeTableButtons = new List<Button>();
        }

        private void Tables_Load(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            DataTable dTableTables = configurator.LoadTables();

            List<Button> tableButtons = new List<Button>
{
            table1, table2, table3, table4, table5,
            table6, table7, table8, table9
};

            for (int i = 0; i < tableButtons.Count; i++)
            {
                if (i < dTableTables.Rows.Count)
                {
                    tableButtons[i].Text = dTableTables.Rows[i]["Table_ID"].ToString();
                }
                else
                {
                    tableButtons[i].Text = "-";
                    tableButtons[i].Enabled = false;
                }
            }


            DataTable dTableActiveTables = configurator.LoadActiveTables();

            for(int i = 0; i < dTableActiveTables.Rows.Count; i++)
            {
                int table_id = Convert.ToInt32(dTableActiveTables.Rows[i].ItemArray[0]);

                for(int y = 0; y < tableButtons.Count; y++)
                {
                    if (tableButtons[y].Text == table_id.ToString())
                    {
                        tableButtons[y].BackColor = Color.DarkRed;
                        activeTableButtons.Add(tableButtons[y]);
                    }
                   
                }
                
            }

        }

        private void OpenOrder(Button table)
        {
            int selectedTableId;

            if (!int.TryParse(table.Text, out selectedTableId))
            {
                MessageBox.Show("Invalid table selected.");
                return;
            }

            if (activeTableButtons.Contains(table))
            {
                OrderForm fOrder = new OrderForm(selectedTableId, "view");
                fOrder.ShowDialog();

                this.Controls.Clear();
                this.InitializeComponent();
                this.activeTableButtons = new List<Button>();
                this.Tables_Load(this, EventArgs.Empty);
            }
            else
            {
                DialogResult res = MessageBox.Show(
                    "There is no active order for this table. Would you like to create one?",
                    "Confirmation",
                    MessageBoxButtons.YesNo
                );

                if (res == DialogResult.Yes)
                {
                    OrderForm fOrder = new OrderForm(selectedTableId, "addWithTableID");
                    fOrder.ShowDialog();

                    this.Controls.Clear();
                    this.InitializeComponent();
                    this.activeTableButtons = new List<Button>();
                    this.Tables_Load(this, EventArgs.Empty);
                }
            }
        }

        private void table1_Click(object sender, EventArgs e)
        {
            OpenOrder(table1);
        }

        private void table2_Click(object sender, EventArgs e)
        {
            OpenOrder(table2);
        }

        private void table3_Click(object sender, EventArgs e)
        {
            OpenOrder(table3);
        }

        private void table4_Click(object sender, EventArgs e)
        {
            OpenOrder(table4);
        }

        private void table5_Click(object sender, EventArgs e)
        {
            OpenOrder(table5);
        }

        private void table6_Click(object sender, EventArgs e)
        {
            OpenOrder(table6);
        }

        private void table7_Click(object sender, EventArgs e)
        {
            OpenOrder(table7);
        }

        private void table8_Click(object sender, EventArgs e)
        {
            OpenOrder(table8);
        }

        private void table9_Click(object sender, EventArgs e)
        {
            OpenOrder(table9);
        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            this.activeTableButtons = new List<Button>();
            this.Tables_Load(this, EventArgs.Empty);
        }

    }
}
