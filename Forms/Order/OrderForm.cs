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
    public partial class OrderForm : Form
    {
        private int table_id;
        private string action; //add, addWithTableID, viewWithOrderID, archiveView
        private int order_id;
        StaffMember staffMember;

        public OrderForm(int table_id, string action)
        {
            InitializeComponent();
            this.table_id = table_id;
            this.action = action;
        }

        private void Order_Load(object sender, EventArgs e)
        {
            if (action == "add") //using the form for creating a new order without table_id
            {
                buttonEdit.Visible = false;
                buttonCompleteOrder.Visible = false;
                textBoxTableNumber.Visible = false;
                textBoxStaffMember.Visible = false;

                buttonAdd.Visible = true;
                comboBoxTableNumber.Visible = true;
                dataGridViewOrderMenuItems.AllowUserToAddRows = true;
                dataGridViewOrderMenuItems.AllowUserToDeleteRows = true;
                dataGridViewOrderMenuItems.ReadOnly = false;

                comboBoxStaffMember.Visible = true;
                comboBoxStaffMember.Enabled = true;
                Configurator configurator = new Configurator();
                DataTable dTableStaffMembers = configurator.LoadStaffMembers();
                this.comboBoxStaffMember.DataSource = dTableStaffMembers;
                this.comboBoxStaffMember.ValueMember = "staffMember_ID";
                this.comboBoxStaffMember.DisplayMember = "displayName";

                dataGridViewOrderMenuItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrderMenuItems_CellClick);
            }
            else if (action == "addWithTableID")
            {
                buttonEdit.Visible = false;
                buttonCompleteOrder.Visible = false;
                comboBoxTableNumber.Visible = false;
                textBoxStaffMember.Visible = false;

                textBoxTableNumber.Visible = true;
                textBoxTableNumber.Text = Convert.ToString(table_id);

                buttonAdd.Visible = true;
                dataGridViewOrderMenuItems.AllowUserToAddRows = true;
                dataGridViewOrderMenuItems.AllowUserToDeleteRows = true;
                dataGridViewOrderMenuItems.ReadOnly = false;

                comboBoxStaffMember.Visible = true;
                comboBoxStaffMember.Enabled = true;
                Configurator configurator = new Configurator();
                DataTable dTableStaffMembers = configurator.LoadStaffMembers();
                this.comboBoxStaffMember.DataSource = dTableStaffMembers;
                this.comboBoxStaffMember.ValueMember = "staffMember_ID";
                this.comboBoxStaffMember.DisplayMember = "displayName";

                dataGridViewOrderMenuItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrderMenuItems_CellClick);
            }
            else //view, archiveView, viewWithOrderID 
            {
                textBoxTableNumber.Text = Convert.ToString(table_id);
  
                Configurator configurator = new Configurator();

                DataTable dTableActiveOrder = new DataTable();

                if (action == "archiveView")
                {
                    dTableActiveOrder = configurator.LoadOrderDetailsByOrderID(table_id);
                }
                else
                {
                    dTableActiveOrder = configurator.LoadOrderDetailsByTableID(table_id);
                }

                double subtotal = 0;
                for (int i = 0; i < dTableActiveOrder.Rows.Count; i++)
                {
                    subtotal = Convert.ToDouble(dTableActiveOrder.Rows[i].ItemArray[4]) * Convert.ToInt32(dTableActiveOrder.Rows[i].ItemArray[3]);

                    string[] row = {Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[2]), //name
                    Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[3]), //quantity
                    Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[5]), //menuItem_ID
                    Convert.ToString(dTableActiveOrder.Rows[i].ItemArray[4]), //price
                    Convert.ToString(subtotal)}; //subtotal
                    this.dataGridViewOrderMenuItems.Rows.Add(row);
                } 

                if (dTableActiveOrder.Rows.Count == 0)
                {
                    //this.Close();

                    //DialogResult res = MessageBox.Show("There is no active order for this table. Would you like to create one?", "Confirmation", MessageBoxButtons.YesNo);
                    //if (res == DialogResult.Yes)
                    //{
                    //    OrderForm fOrder = new OrderForm(table_id, "addWithTableID");
                    //    fOrder.ShowDialog();
                    //}
                    //if (res == DialogResult.No)
                    //{

                    //}
                }
                else
                {
                    order_id = Convert.ToInt32(dTableActiveOrder.Rows[0].ItemArray[0]);

                    staffMember = configurator.LoadStaffMembersByOrderID(order_id);
                    textBoxStaffMember.Visible = true;
                    textBoxStaffMember.Text = staffMember.DisplayName;

                    textBoxTotal.ReadOnly= true;
                    
                    textBoxTotal_Click(e, e);

                }

                comboBoxTableNumber.Visible = false;
                comboBoxStaffMember.Visible = false;

                buttonDelete.Visible = true;

                if(action == "archiveView")
                {
                    
                    buttonEdit.Visible = false;
                    buttonCompleteOrder.Visible = false;
                    buttonDelete.Visible = false;

                }

            }

            buttonSave.Visible = false;
            //buttonCancel.Visible = false;
            

        } 

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {
            this.dataGridViewOrderMenuItems.AllowUserToAddRows = true;
            this.dataGridViewOrderMenuItems.AllowUserToDeleteRows = true;
            this.dataGridViewOrderMenuItems.ReadOnly = false;
            buttonSave.Visible = true;
            //buttonCancel.Visible = true;
            buttonEdit.Visible = false;
            textBoxTableNumber.Visible = false;
            comboBoxTableNumber.Visible = true;
            comboBoxTableNumber.Text = Convert.ToString(table_id);

            textBoxStaffMember.Visible = false;
            comboBoxStaffMember.Visible = true;
            Configurator configurator = new Configurator();
            DataTable dTableStaffMembers = configurator.LoadStaffMembers();
            this.comboBoxStaffMember.DataSource = dTableStaffMembers;
            this.comboBoxStaffMember.ValueMember = "staffMember_ID";
            this.comboBoxStaffMember.DisplayMember = "displayName";
            comboBoxStaffMember.Text = staffMember.DisplayName;
            

            dataGridViewOrderMenuItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrderMenuItems_CellClick);
        }

        //private void buttonCancel_Click(object sender, EventArgs e)
        //{
        //    this.dataGridViewOrderMenuItems.AllowUserToAddRows = false;
        //    this.dataGridViewOrderMenuItems.AllowUserToDeleteRows = false;
        //    this.dataGridViewOrderMenuItems.ReadOnly = true;
        //    buttonSave.Visible = false;
        //    //buttonCancel.Visible = false;
        //    buttonEdit.Visible = true;
        //    dataGridViewOrderMenuItems.Update();
        //    textBoxTableNumber.Visible = true;
        //    comboBoxTableNumber.Visible = false;
        //}


        private void buttonSave_Click(object sender, EventArgs e)//Save after editing
        {

            bool mistake = this.CellValidating(dataGridViewOrderMenuItems, comboBoxTableNumber);

            if (mistake)
            {
                return;
            }
            else if (!mistake)
            {
                
                Configurator configurator = new Configurator();

                int newTableID = this.comboBoxTableNumber.SelectedIndex + 1;

                //update Order
                configurator.UpdateOrder(order_id, Convert.ToInt32(newTableID), 'A');

                //update OrderMenuItems
                configurator.DeleteOrderMenuItem(order_id);

                for (int i = 0; i < dataGridViewOrderMenuItems.RowCount - 1; i++)
                {
                    int newMenuItem_ID = Convert.ToInt32(dataGridViewOrderMenuItems.Rows[i].Cells[2].Value);
                    int newQuantity = Convert.ToInt32(dataGridViewOrderMenuItems.Rows[i].Cells[1].Value);
                    configurator.AddNewOrderMenuItem(order_id, newMenuItem_ID, newQuantity);
                }

                this.dataGridViewOrderMenuItems.AllowUserToDeleteRows = false;
                this.dataGridViewOrderMenuItems.AllowUserToAddRows = false;
                this.dataGridViewOrderMenuItems.ReadOnly = true;
                buttonSave.Visible = false;
                //buttonCancel.Visible = false;
                buttonEdit.Visible = true;
                textBoxTableNumber.Visible = true;
                comboBoxTableNumber.Visible = false;

                if (Convert.ToString(comboBoxTableNumber.SelectedItem) != Convert.ToString(table_id))
                {
                    this.Close();
                }

            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)//Save after creating a new order
        {
            bool mistake = this.CellValidating(dataGridViewOrderMenuItems, comboBoxTableNumber);
            
            //bool mistake = false;
            if (mistake)
            {
                return;
            }

            else if (!mistake)
            {

                Configurator configurator = new Configurator();

                //add new order
                int newTable_ID = -1;

                if(action == "addWithTableID")
                {
                    newTable_ID = table_id;

                } else if (action == "add")
                {
                    newTable_ID = Convert.ToInt32(comboBoxTableNumber.SelectedIndex) + 1;
                }

                int newStaffMember_ID = Convert.ToInt32(comboBoxStaffMember.SelectedValue);

                int newOrder_ID = configurator.AddNewOrder(Convert.ToInt32(newTable_ID), 'A', newStaffMember_ID);


                //add orderMenuItems
                for (int i = 0; i < dataGridViewOrderMenuItems.RowCount - 1; i++)
                {
                    int newMenuItem_ID = Convert.ToInt32(dataGridViewOrderMenuItems.Rows[i].Cells[2].Value);
                    int newQuantity = Convert.ToInt32(dataGridViewOrderMenuItems.Rows[i].Cells[1].Value);
                    configurator.AddNewOrderMenuItem(newOrder_ID, newMenuItem_ID, newQuantity);
                }

                MessageBox.Show("Succesfully added! Order number: " + Convert.ToString(newOrder_ID));

                this.dataGridViewOrderMenuItems.AllowUserToDeleteRows = false;
                this.dataGridViewOrderMenuItems.AllowUserToAddRows = false;
                this.dataGridViewOrderMenuItems.ReadOnly = true;
                buttonSave.Visible = false;
                //buttonCancel.Visible = false;
                buttonEdit.Visible = true;
                textBoxTableNumber.Visible = true;
                comboBoxTableNumber.Visible = false;
                this.Close();
            }


        }

        private bool CellValidating(DataGridView dataGridView, ComboBox comboBoxTable)
        {
            //cheking if Qty is int
            int x;
            bool mistake = false;
            bool noName = false;
            bool wholeNumber = false;
            int difference = 1;
            if(dataGridView.RowCount == 1)
            {
                difference = 0;
            }
            for (int i = 0; i < dataGridView.RowCount - difference; i++)
            {
                if (Convert.ToString(dataGridView.Rows[i].Cells[0].Value) == string.Empty)
                {
                    mistake = true;
                    noName = true;
                }
                if (Convert.ToString(dataGridView.Rows[i].Cells[1].Value) == string.Empty)
                {
                    mistake = true;
                    wholeNumber = true;
                }
                else if (!int.TryParse(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), out x))
                {
                    mistake = true;
                    wholeNumber = true;
                }
            }

            if (wholeNumber)
            {
                MessageBox.Show("The value for Qty must be a whole number.");
            }
            if (noName)
            {
                MessageBox.Show("Please select an item from the Menu.");
            }

            if (action == "add" && comboBoxTable.SelectedItem == null)
            {
                MessageBox.Show("Choose a table");
                mistake = true;
            }

                return mistake;
        }

        

        private void dataGridViewOrderMenuItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridViewOrderMenuItems.CurrentCell.ColumnIndex == 0)
            {
                MenuForm fMenuForm = new MenuForm(2, "add");
                fMenuForm.ShowDialog();
                dataGridViewOrderMenuItems.CurrentCell.Value = fMenuForm.menuItemName;
                dataGridViewOrderMenuItems.CurrentRow.Cells[2].Value = fMenuForm.menuItem_ID;

                Configurator configurator = new Configurator();
                Entities.MenuItem menuItem = configurator.LoadMenuItemByName(fMenuForm.menuItemName);
                dataGridViewOrderMenuItems.CurrentRow.Cells[3].Value = menuItem.Price;
                
            }
            if(dataGridViewOrderMenuItems.CurrentCell.ColumnIndex == 4)
            {
                double subtotal = Convert.ToDouble(dataGridViewOrderMenuItems.CurrentRow.Cells[3].Value) * Convert.ToInt32(dataGridViewOrderMenuItems.CurrentRow.Cells[1].Value);
                dataGridViewOrderMenuItems.CurrentRow.Cells[4].Value = subtotal;
            }

            

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Configurator configurator = new Configurator();
                configurator.DeleteActiveOrder(order_id);

                MessageBox.Show("Succesfully deleted.");
                this.Close();

            }
            if (res == DialogResult.No)
            {

            }
        }

        private void buttonCompleteOrder_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to complete this order?", "Confirmation", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                Configurator configurator = new Configurator();
                configurator.FinishOrder(order_id);

                MessageBox.Show("Succesfully finished.");
                this.Close();

            }
            if (res == DialogResult.No)
            {

            }

            
        }

        private void textBoxTotal_Click(object sender, EventArgs e)
        {

            double total = 0;
            double subtotal = 0;
            for (int i = 0; i < dataGridViewOrderMenuItems.RowCount; i++)
            {

                if(dataGridViewOrderMenuItems.Rows[i].Cells[1].Value == null)
                {
                    subtotal = 0;
                }
                else
                {
                    subtotal = Convert.ToDouble(dataGridViewOrderMenuItems.Rows[i].Cells[3].Value) * Convert.ToInt32(dataGridViewOrderMenuItems.Rows[i].Cells[1].Value);
                    dataGridViewOrderMenuItems.Rows[i].Cells[4].Value = subtotal;
                    total += subtotal;
                }
                
            }

            textBoxTotal.Text = Convert.ToString(total);
        }

        //private void dataGridViewOrderMenuItems_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    dataGridViewOrderMenuItems.CurrentRow.Selected = true;
        //}

        //private void dataGridViewOrderMenuItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    foreach (DataGridViewRow item in this.dataGridViewOrderMenuItems.SelectedRows)
        //    {
        //        dataGridViewOrderMenuItems.Rows.RemoveAt(item.Index);
        //    }
        //}
    }
}
