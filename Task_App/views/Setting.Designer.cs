namespace Task_App.views
{
    partial class User_Setting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.main_flow = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_path = new System.Windows.Forms.Label();
            this.txt_passEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txt_path = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_save = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PassAccount = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_savePassAccount = new Guna.UI2.WinForms.Guna2Button();
            this.main_flow.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_flow
            // 
            this.main_flow.AutoSize = true;
            this.main_flow.BackColor = System.Drawing.Color.White;
            this.main_flow.Controls.Add(this.tableLayoutPanel1);
            this.main_flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.main_flow.Location = new System.Drawing.Point(10, 10);
            this.main_flow.Margin = new System.Windows.Forms.Padding(0);
            this.main_flow.Name = "main_flow";
            this.main_flow.Size = new System.Drawing.Size(1133, 568);
            this.main_flow.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_path, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_passEmail, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_path, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_save, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_PassAccount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btn_savePassAccount, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1133, 184);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(503, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu email";
            // 
            // lbl_path
            // 
            this.lbl_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_path.Location = new System.Drawing.Point(5, 5);
            this.lbl_path.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(503, 58);
            this.lbl_path.TabIndex = 0;
            this.lbl_path.Text = "Thư mục lưu trữ";
            this.lbl_path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_passEmail
            // 
            this.txt_passEmail.AutoSize = true;
            this.txt_passEmail.BorderRadius = 5;
            this.txt_passEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_passEmail.DefaultText = "";
            this.txt_passEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_passEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_passEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_passEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_passEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_passEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_passEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_passEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_passEmail.IconRight = global::Task_App.Properties.Resources.notshow;
            this.txt_passEmail.IconRightCursor = System.Windows.Forms.Cursors.Hand;
            this.txt_passEmail.IconRightOffset = new System.Drawing.Point(10, 0);
            this.txt_passEmail.IconRightSize = new System.Drawing.Size(15, 15);
            this.txt_passEmail.Location = new System.Drawing.Point(513, 68);
            this.txt_passEmail.Margin = new System.Windows.Forms.Padding(5);
            this.txt_passEmail.Name = "txt_passEmail";
            this.txt_passEmail.PlaceholderText = "";
            this.txt_passEmail.SelectedText = "";
            this.txt_passEmail.Size = new System.Drawing.Size(493, 48);
            this.txt_passEmail.TabIndex = 1;
            // 
            // txt_path
            // 
            this.txt_path.AutoSize = true;
            this.txt_path.BorderRadius = 5;
            this.txt_path.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_path.DefaultText = "";
            this.txt_path.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_path.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_path.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_path.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_path.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_path.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_path.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_path.Location = new System.Drawing.Point(513, 10);
            this.txt_path.Margin = new System.Windows.Forms.Padding(5);
            this.txt_path.Name = "txt_path";
            this.txt_path.PlaceholderText = "";
            this.txt_path.SelectedText = "";
            this.txt_path.Size = new System.Drawing.Size(493, 48);
            this.txt_path.TabIndex = 1;
            this.txt_path.Click += new System.EventHandler(this.txt_path_Click);
            // 
            // btn_save
            // 
            this.btn_save.BorderRadius = 5;
            this.btn_save.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_save.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_save.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_save.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_save.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_save.ForeColor = System.Drawing.Color.White;
            this.btn_save.Location = new System.Drawing.Point(1014, 66);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(111, 45);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Cập nhật";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(503, 58);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mật khẩu tài khoản";
            // 
            // txt_PassAccount
            // 
            this.txt_PassAccount.BorderRadius = 5;
            this.txt_PassAccount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_PassAccount.DefaultText = "";
            this.txt_PassAccount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_PassAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_PassAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_PassAccount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_PassAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_PassAccount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_PassAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_PassAccount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_PassAccount.IconRight = global::Task_App.Properties.Resources.notshow;
            this.txt_PassAccount.IconRightOffset = new System.Drawing.Point(10, 0);
            this.txt_PassAccount.IconRightSize = new System.Drawing.Size(15, 15);
            this.txt_PassAccount.Location = new System.Drawing.Point(513, 126);
            this.txt_PassAccount.Margin = new System.Windows.Forms.Padding(5);
            this.txt_PassAccount.Name = "txt_PassAccount";
            this.txt_PassAccount.PlaceholderText = "";
            this.txt_PassAccount.SelectedText = "";
            this.txt_PassAccount.Size = new System.Drawing.Size(493, 48);
            this.txt_PassAccount.TabIndex = 4;
            // 
            // btn_savePassAccount
            // 
            this.btn_savePassAccount.BorderRadius = 5;
            this.btn_savePassAccount.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_savePassAccount.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_savePassAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_savePassAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_savePassAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_savePassAccount.ForeColor = System.Drawing.Color.White;
            this.btn_savePassAccount.Location = new System.Drawing.Point(1014, 124);
            this.btn_savePassAccount.Name = "btn_savePassAccount";
            this.btn_savePassAccount.Size = new System.Drawing.Size(111, 45);
            this.btn_savePassAccount.TabIndex = 5;
            this.btn_savePassAccount.Text = "Cập nhật";
            this.btn_savePassAccount.Click += new System.EventHandler(this.btn_savePassAccount_Click);
            // 
            // User_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.main_flow);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "User_Setting";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(1153, 588);
            this.Load += new System.EventHandler(this.User_Setting_Load);
            this.main_flow.ResumeLayout(false);
            this.main_flow.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel main_flow;
        private System.Windows.Forms.Label lbl_path;
        private Guna.UI2.WinForms.Guna2TextBox txt_path;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2TextBox txt_passEmail;
        private Guna.UI2.WinForms.Guna2Button btn_save;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txt_PassAccount;
        private Guna.UI2.WinForms.Guna2Button btn_savePassAccount;
    }
}
