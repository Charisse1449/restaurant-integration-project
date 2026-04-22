using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Forms.StaffMembers
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.StaffForm_Load);
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            DataTable dTableStaffMember = configurator.LoadStaffMembers();

            for (int i = 0; i < dTableStaffMember.Rows.Count; i++)
            {
                int staffMember_ID = Convert.ToInt32(dTableStaffMember.Rows[i].ItemArray[0]);
                StaffMemberForm fa = new StaffMemberForm(staffMember_ID, "view");
                fa.TopLevel = false;
                //fa.FormClosed += new FormClosedEventHandler(child_FormClosed); //dava greshka
                fa.Show();
                flowLayoutPanel1.Controls.Add(fa);
            }

            //int staffMember_ID = Convert.ToInt32(dTableStaffMember.Rows[2].ItemArray[0]);
            //MessageBox.Show(Convert.ToString(staffMember_ID));
            //StaffMemberForm fa = new StaffMemberForm(staffMember_ID, "view");
            //fa.TopLevel = false;
            ////fa.FormClosed += new FormClosedEventHandler(child_FormClosed); //dava greshka
            //fa.Show();
            //flowLayoutPanel1.Controls.Add(fa);


        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {

            this.Controls.Clear();
            this.InitializeComponent();
            this.StaffForm_Load(e, e);
            //this.Show();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            StaffMemberForm fa = new StaffMemberForm(-1, "add");
            fa.FormClosed += new FormClosedEventHandler(child_FormClosed);
            fa.ShowDialog();
        }
    }
}
