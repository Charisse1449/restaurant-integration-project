namespace RestaurantPOS.Forms
{
    partial class OrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewOrderMenuItems = new System.Windows.Forms.DataGridView();
            this.NameMenuItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MenuItem_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCompleteOrder = new System.Windows.Forms.Button();
            this.labelTableNumber = new System.Windows.Forms.Label();
            this.textBoxTableNumber = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.comboBoxTableNumber = new System.Windows.Forms.ComboBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTotal = new System.Windows.Forms.Label();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.textBoxStaffMember = new System.Windows.Forms.TextBox();
            this.comboBoxStaffMember = new System.Windows.Forms.ComboBox();
            this.labelStaffMember = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderMenuItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewOrderMenuItems
            // 
            this.dataGridViewOrderMenuItems.AllowUserToAddRows = false;
            this.dataGridViewOrderMenuItems.AllowUserToDeleteRows = false;
            this.dataGridViewOrderMenuItems.AllowUserToResizeColumns = false;
            this.dataGridViewOrderMenuItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridViewOrderMenuItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOrderMenuItems.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewOrderMenuItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewOrderMenuItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkSlateGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOrderMenuItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewOrderMenuItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrderMenuItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameMenuItem,
            this.Quantity,
            this.MenuItem_ID,
            this.Price,
            this.Subtotal});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOrderMenuItems.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewOrderMenuItems.Location = new System.Drawing.Point(30, 70);
            this.dataGridViewOrderMenuItems.Name = "dataGridViewOrderMenuItems";
            this.dataGridViewOrderMenuItems.ReadOnly = true;
            this.dataGridViewOrderMenuItems.RowHeadersWidth = 20;
            this.dataGridViewOrderMenuItems.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewOrderMenuItems.RowTemplate.Height = 24;
            this.dataGridViewOrderMenuItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewOrderMenuItems.ShowEditingIcon = false;
            this.dataGridViewOrderMenuItems.Size = new System.Drawing.Size(491, 245);
            this.dataGridViewOrderMenuItems.TabIndex = 4;
            //this.dataGridViewOrderMenuItems.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrderMenuItems_CellContentDoubleClick);
            //this.dataGridViewOrderMenuItems.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewOrderMenuItems_RowHeaderMouseClick);
            // 
            // NameMenuItem
            // 
            this.NameMenuItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.NameMenuItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.NameMenuItem.FillWeight = 50.80214F;
            this.NameMenuItem.HeaderText = "Name";
            this.NameMenuItem.MinimumWidth = 6;
            this.NameMenuItem.Name = "NameMenuItem";
            this.NameMenuItem.ReadOnly = true;
            this.NameMenuItem.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.Quantity.FillWeight = 13.0525F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MenuItem_ID
            // 
            this.MenuItem_ID.HeaderText = "MenuItem_ID";
            this.MenuItem_ID.MinimumWidth = 6;
            this.MenuItem_ID.Name = "MenuItem_ID";
            this.MenuItem_ID.ReadOnly = true;
            this.MenuItem_ID.Visible = false;
            this.MenuItem_ID.Width = 125;
            // 
            // Price
            // 
            this.Price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.Price.DefaultCellStyle = dataGridViewCellStyle5;
            this.Price.FillWeight = 18.07269F;
            this.Price.HeaderText = "Price";
            this.Price.MinimumWidth = 6;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Subtotal
            // 
            this.Subtotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.Subtotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.Subtotal.FillWeight = 18.07269F;
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.MinimumWidth = 6;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.ForeColor = System.Drawing.Color.White;
            this.buttonEdit.Location = new System.Drawing.Point(30, 405);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(79, 34);
            this.buttonEdit.TabIndex = 10;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click_1);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(29, 405);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(79, 34);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCompleteOrder
            // 
            this.buttonCompleteOrder.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonCompleteOrder.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.buttonCompleteOrder.FlatAppearance.BorderSize = 0;
            this.buttonCompleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCompleteOrder.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCompleteOrder.ForeColor = System.Drawing.Color.White;
            this.buttonCompleteOrder.Location = new System.Drawing.Point(350, 405);
            this.buttonCompleteOrder.Name = "buttonCompleteOrder";
            this.buttonCompleteOrder.Size = new System.Drawing.Size(171, 34);
            this.buttonCompleteOrder.TabIndex = 13;
            this.buttonCompleteOrder.Text = "Complete Order";
            this.buttonCompleteOrder.UseVisualStyleBackColor = false;
            this.buttonCompleteOrder.Click += new System.EventHandler(this.buttonCompleteOrder_Click);
            // 
            // labelTableNumber
            // 
            this.labelTableNumber.AutoSize = true;
            this.labelTableNumber.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTableNumber.ForeColor = System.Drawing.Color.White;
            this.labelTableNumber.Location = new System.Drawing.Point(24, 30);
            this.labelTableNumber.Name = "labelTableNumber";
            this.labelTableNumber.Size = new System.Drawing.Size(143, 25);
            this.labelTableNumber.TabIndex = 14;
            this.labelTableNumber.Text = "Table Number ";
            this.labelTableNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTableNumber
            // 
            this.textBoxTableNumber.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTableNumber.Location = new System.Drawing.Point(177, 28);
            this.textBoxTableNumber.Name = "textBoxTableNumber";
            this.textBoxTableNumber.ReadOnly = true;
            this.textBoxTableNumber.Size = new System.Drawing.Size(49, 31);
            this.textBoxTableNumber.TabIndex = 15;
            // 
            // buttonExit
            // 
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(500, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(46, 43);
            this.buttonExit.TabIndex = 16;
            this.buttonExit.Text = "✕";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // comboBoxTableNumber
            // 
            this.comboBoxTableNumber.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxTableNumber.FormattingEnabled = true;
            this.comboBoxTableNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxTableNumber.Location = new System.Drawing.Point(177, 26);
            this.comboBoxTableNumber.Name = "comboBoxTableNumber";
            this.comboBoxTableNumber.Size = new System.Drawing.Size(49, 33);
            this.comboBoxTableNumber.TabIndex = 17;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(29, 405);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(79, 34);
            this.buttonAdd.TabIndex = 18;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Visible = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelTotal);
            this.panel1.Controls.Add(this.textBoxTotal);
            this.panel1.Controls.Add(this.textBoxStaffMember);
            this.panel1.Controls.Add(this.comboBoxStaffMember);
            this.panel1.Controls.Add(this.labelStaffMember);
            this.panel1.Controls.Add(this.labelTableNumber);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.dataGridViewOrderMenuItems);
            this.panel1.Controls.Add(this.buttonCompleteOrder);
            this.panel1.Controls.Add(this.buttonEdit);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 459);
            this.panel1.TabIndex = 19;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.ForeColor = System.Drawing.Color.White;
            this.labelTotal.Location = new System.Drawing.Point(369, 344);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(55, 25);
            this.labelTotal.TabIndex = 23;
            this.labelTotal.Text = "Total";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotal.Location = new System.Drawing.Point(436, 338);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(85, 31);
            this.textBoxTotal.TabIndex = 22;
            this.textBoxTotal.Click += new System.EventHandler(this.textBoxTotal_Click);
            // 
            // textBoxStaffMember
            // 
            this.textBoxStaffMember.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxStaffMember.Location = new System.Drawing.Point(161, 338);
            this.textBoxStaffMember.Name = "textBoxStaffMember";
            this.textBoxStaffMember.Size = new System.Drawing.Size(202, 31);
            this.textBoxStaffMember.TabIndex = 21;
            // 
            // comboBoxStaffMember
            // 
            this.comboBoxStaffMember.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxStaffMember.FormattingEnabled = true;
            this.comboBoxStaffMember.Location = new System.Drawing.Point(161, 338);
            this.comboBoxStaffMember.Name = "comboBoxStaffMember";
            this.comboBoxStaffMember.Size = new System.Drawing.Size(202, 33);
            this.comboBoxStaffMember.TabIndex = 20;
            // 
            // labelStaffMember
            // 
            this.labelStaffMember.AutoSize = true;
            this.labelStaffMember.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStaffMember.ForeColor = System.Drawing.Color.White;
            this.labelStaffMember.Location = new System.Drawing.Point(25, 341);
            this.labelStaffMember.Name = "labelStaffMember";
            this.labelStaffMember.Size = new System.Drawing.Size(130, 25);
            this.labelStaffMember.TabIndex = 19;
            this.labelStaffMember.Text = "Staff Member";
            this.labelStaffMember.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.DarkSlateGray;
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Malgun Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(115, 405);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(88, 34);
            this.buttonDelete.TabIndex = 11;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(551, 459);
            this.Controls.Add(this.comboBoxTableNumber);
            this.Controls.Add(this.textBoxTableNumber);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order";
            this.Load += new System.EventHandler(this.Order_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrderMenuItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewOrderMenuItems;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCompleteOrder;
        private System.Windows.Forms.Label labelTableNumber;
        private System.Windows.Forms.TextBox textBoxTableNumber;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ComboBox comboBoxTableNumber;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ComboBox comboBoxStaffMember;
        private System.Windows.Forms.Label labelStaffMember;
        private System.Windows.Forms.TextBox textBoxStaffMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn MenuItem_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.TextBox textBoxTotal;
        private System.Windows.Forms.Label labelTotal;
    }
}