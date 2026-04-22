using RestaurantPOS.Forms;
using RestaurantPOS.Forms.StaffMembers;
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
    public partial class MainForm : Form
    {
        private Button currentButton;
       
        //private int tempIndex;
        private Form activeForm;

        public int role;

        public MainForm(int role)
        {
            InitializeComponent();
            this.role = role;
            MainForm_Load();
            
        }

        public void MainForm_Load()
        {
            

            if(role == 1)
            {
                buttonAddUser.Visible = true;
                buttonStaff.Visible = true;
            }
            else
            {
                buttonAddUser.Visible = false;
                buttonStaff.Visible = false;
            }
        }

        private void ActivateButton(object btnSender)
        {
            if(btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = Color.White;
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.DarkSlateGray;
                    //currentButton.Font
                    
                }
            }
        }

        private void DisableButton()
        {
            foreach(Control previousBtn in panelNavigation.Controls)
            {
                if (previousBtn.GetType()==typeof(Button))
                {
                    previousBtn.BackColor = Color.DarkSlateGray;
                    previousBtn.ForeColor = Color.White;
                    //previousBtn.Font =
                }    
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
            //labelTitle.Text = childForm.Text;
        }

        private void buttonTables_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender);
            //OpenChildForm(new Forms.Tables(), sender);
            TablesForm fTables = new TablesForm();
            OpenChildForm(fTables, sender);
            

            textBoxSearch.Visible = false;
            buttonSearch.Visible = false;
            buttonExcel.Visible = false;

        }

        private void buttonOrders_Click(object sender, EventArgs e)
        {
            ActiveOrdersForm fActiveOrders = new ActiveOrdersForm();
            OpenChildForm(fActiveOrders, sender);

            textBoxSearch.Visible = false;
            buttonSearch.Visible = false;
            buttonExcel.Visible = false;
        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            MenuForm fMenu = new MenuForm(role, "view");
            OpenChildForm(fMenu, sender);

            textBoxSearch.Visible = false;
            buttonSearch.Visible = false;
            buttonExcel.Visible = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();

            LoginForm lForm = new LoginForm();
            lForm.ShowDialog();

            textBoxSearch.Visible = false;
            buttonSearch.Visible = false;
            buttonExcel.Visible = false;
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm fAddUser = new AddUserForm();
            fAddUser.ShowDialog();

            textBoxSearch.Visible = false;
            buttonSearch.Visible = false;
            buttonExcel.Visible = false;
        }

        private void buttonClosedOrders_Click(object sender, EventArgs e)
        {
            ClosedOrdersForm fClosedOrders = new ClosedOrdersForm(null, null);
            OpenChildForm(fClosedOrders, sender);

            textBoxSearch.Visible = true;
            buttonSearch.Visible = true;
            textBoxSearch.Text = "";
            buttonExcel.Visible = true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            int x;
            if (int.TryParse(Convert.ToString(textBoxSearch.Text), out x))
            {
                string criteria = textBoxSearch.Text;
                ClosedOrdersForm fClosedOrders = new ClosedOrdersForm("table_id", criteria);
                OpenChildForm(fClosedOrders, sender);
            }
            else
            {
                string criteria = textBoxSearch.Text;
                ClosedOrdersForm fClosedOrders = new ClosedOrdersForm("staffMemberName", criteria);
                OpenChildForm(fClosedOrders, sender);
            }

            buttonSearch.BackColor = Color.DarkSlateGray;

            
        }

        private void buttonStaff_Click(object sender, EventArgs e)
        {
            StaffForm fStaff = new StaffForm();
            OpenChildForm(fStaff, sender);

            buttonSearch.Visible = false;
            textBoxSearch.Visible = false;
            buttonExcel.Visible = false;
        }

        private void buttonMaximize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            buttonMaximize.Visible = false;
            buttonRestore.Visible = true;
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            buttonMaximize.Visible = true;
            buttonRestore.Visible = false;
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            
            Microsoft.Office.Interop.Excel._Application app = new
            Microsoft.Office.Interop.Excel.Application();
            
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add();
            
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            
            app.Visible = true;
            
            
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            
            worksheet.Name = "Exported orders";

            Configurator configurator = new Configurator();

            DataTable dtClosedOrders = configurator.LoadOrders('C');

            int Order_ID = 0;
            for (int i = 0; i < dtClosedOrders.Rows.Count; i++)
            {
                Order_ID = Convert.ToInt32(dtClosedOrders.Rows[i].ItemArray[0]);
                DataTable dtDetails = configurator.LoadOrderDetailsByOrderID(Order_ID);

                for (int y = 0; y < dtDetails.Columns.Count; y++)
                {
                    worksheet.Cells[1, y + 1] = dtDetails.Columns[y].ColumnName;
                }

                for (int x = 0; x < dtDetails.Rows.Count; x++)
                {
                    for (int z = 0; z < dtDetails.Columns.Count; z++)
                    {
                        worksheet.Cells[x + 2, z + 1] = Convert.ToString(dtDetails.Rows[x].ItemArray[z]);
                    }
                    
                }
            }

            //workbook.SaveAs("C:\Users/gerga/Desktop\\output.xls");
            app.Quit();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
