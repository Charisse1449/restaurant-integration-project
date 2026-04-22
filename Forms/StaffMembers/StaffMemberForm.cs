using RestaurantPOS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantPOS.Forms.StaffMembers
{
    public partial class StaffMemberForm : Form
    {
        private int staffMember_ID;
        private string action; //add, view, edit
        public StaffMemberForm(int staffMember_ID, string action)
        {
            InitializeComponent();
            this.staffMember_ID = staffMember_ID;
            this.action = action;
            this.Load += new System.EventHandler(this.StaffMemberForm_Load);
        }

        private void StaffMemberForm_Load(object sender, EventArgs e)
        {
            if (action == "view" || action == "edit")
            {
                Configurator configurator = new Configurator();

                StaffMember staffMember = configurator.LoadStaffMembersByStaffMemberID(staffMember_ID);

                textBoxFirstName.Text = staffMember.FirstName;
                textBoxMiddleName.Text = staffMember.MiddleName;
                textBoxLastName.Text = staffMember.LastName;
                textBoxDisplayName.Text = staffMember.DisplayName;

                if(staffMember.Image != null)
                {
                    Image returnImage = null;
                    using (MemoryStream ms = new MemoryStream(staffMember.Image))
                    {
                        returnImage = Image.FromStream(ms);
                    }

                    pictureBox1.Image = returnImage;
                }
                
                if(action == "view")
                {
                    textBoxFirstName.ReadOnly = true;
                    textBoxMiddleName.ReadOnly = true;
                    textBoxLastName.ReadOnly = true;
                    textBoxDisplayName.ReadOnly = true;
                    pictureBox1.Enabled = false;
                    buttonExit.Visible = false;
                    buttonAdd.Visible = false;
                    buttonDelete.Visible = false;
                    buttonEdit.Visible = true;
                    buttonSave.Visible = false;
                    buttonAdd.Visible = false;

                }
                if(action == "edit")
                {
                    textBoxFirstName.ReadOnly = false;
                    textBoxMiddleName.ReadOnly = false;
                    textBoxLastName.ReadOnly = false;
                    textBoxDisplayName.ReadOnly = false;
                    pictureBox1.Enabled = true;

                    buttonExit.Visible = true;
                    buttonDelete.Visible = true;
                    buttonAdd.Visible = false;
                    buttonEdit.Visible = false;
                    buttonSave.Visible = true;
                    buttonAdd.Visible = false;
                }
                
            }
            else //adding new //"add"
            {
                textBoxFirstName.ReadOnly = false;
                textBoxMiddleName.ReadOnly = false;
                textBoxLastName.ReadOnly = false;
                textBoxDisplayName.ReadOnly = false;
                pictureBox1.Enabled = true;

                buttonEdit.Visible = false;
                buttonDelete.Visible = false;
                buttonSave.Visible = false;
                buttonAdd.Visible = true;
                buttonExit.Visible = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            //converting image to byte[]
            Image image = pictureBox1.Image;
            byte[] imageByte = null;
            if (image != null)
            {
                
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
                imageByte = xByte;

                configurator.AddStaffMember(textBoxFirstName.Text, textBoxMiddleName.Text, textBoxLastName.Text, textBoxDisplayName.Text, imageByte);
                MessageBox.Show("Succesfully added.");
                this.Close();

            }
            else
            {
                MessageBox.Show("Please add image.");
            }
            

            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            StaffMemberForm fSM = new StaffMemberForm(staffMember_ID, "edit");
            fSM.ShowDialog();
            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {
                Configurator configurator = new Configurator();
                configurator.DeleteStaffMember(staffMember_ID);

                
                this.Close();

            }
            if (res == DialogResult.Cancel)
            {

            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Configurator configurator = new Configurator();

            Image image = pictureBox1.Image;
            byte[] imageByte = null;
            if (image != null)
            {
                try
                {
                    ImageConverter _imageConverter = new ImageConverter();
                    byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
                    imageByte = xByte;

                    configurator.UpdateStaffMember(staffMember_ID, textBoxFirstName.Text, textBoxMiddleName.Text, textBoxLastName.Text, textBoxDisplayName.Text, imageByte);
                    MessageBox.Show("Succesfully updated.");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Please re-upload the image file.");
                }
                

                

            }
            else
            {
                MessageBox.Show("Please add image.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            openFileDialog1.Multiselect = false;
            openFileDialog1.ShowDialog();

            //if(openFileDialog1.FileName != null)
            //new image
            if(openFileDialog1.FileName != null)
            {
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Bitmap myBitmap = new Bitmap(openFileDialog1.FileName);
                Image myThumbnail = myBitmap.GetThumbnailImage(300, 300, myCallback, IntPtr.Zero);
                pictureBox1.Image = myThumbnail;
            }
            
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}
