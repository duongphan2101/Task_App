namespace Task_App.views
{
    partial class Register
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
            this.panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.cb_CV = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_CV = new System.Windows.Forms.Label();
            this.cb_PB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_PB = new System.Windows.Forms.Label();
            this.cb_DV = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_RePass = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_RePass = new System.Windows.Forms.Label();
            this.txt_Pass = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_Pass = new System.Windows.Forms.Label();
            this.txt_UserName = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txt_Email = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.btn_DangKy = new Guna.UI2.WinForms.Guna2Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.panel1.Controls.Add(this.lbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 601);
            this.panel1.TabIndex = 0;
            // 
            // lbl
            // 
            this.lbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(414, 601);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Đăng Ký";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_DangKy);
            this.panel2.Controls.Add(this.cb_CV);
            this.panel2.Controls.Add(this.lbl_CV);
            this.panel2.Controls.Add(this.cb_PB);
            this.panel2.Controls.Add(this.lbl_PB);
            this.panel2.Controls.Add(this.cb_DV);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txt_RePass);
            this.panel2.Controls.Add(this.lbl_RePass);
            this.panel2.Controls.Add(this.txt_Pass);
            this.panel2.Controls.Add(this.lbl_Pass);
            this.panel2.Controls.Add(this.txt_UserName);
            this.panel2.Controls.Add(this.lblName);
            this.panel2.Controls.Add(this.txt_Email);
            this.panel2.Controls.Add(this.lblEmail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(414, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(386, 601);
            this.panel2.TabIndex = 1;
            // 
            // cb_CV
            // 
            this.cb_CV.BackColor = System.Drawing.Color.Transparent;
            this.cb_CV.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_CV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_CV.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_CV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_CV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cb_CV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cb_CV.ItemHeight = 30;
            this.cb_CV.Location = new System.Drawing.Point(32, 476);
            this.cb_CV.Margin = new System.Windows.Forms.Padding(0);
            this.cb_CV.Name = "cb_CV";
            this.cb_CV.Size = new System.Drawing.Size(325, 36);
            this.cb_CV.TabIndex = 19;
            this.cb_CV.SelectedIndexChanged += new System.EventHandler(this.cb_CV_SelectedIndexChanged);
            // 
            // lbl_CV
            // 
            this.lbl_CV.AutoSize = true;
            this.lbl_CV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CV.Location = new System.Drawing.Point(28, 456);
            this.lbl_CV.Name = "lbl_CV";
            this.lbl_CV.Size = new System.Drawing.Size(70, 20);
            this.lbl_CV.TabIndex = 18;
            this.lbl_CV.Text = "Chức vụ";
            // 
            // cb_PB
            // 
            this.cb_PB.BackColor = System.Drawing.Color.Transparent;
            this.cb_PB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_PB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PB.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_PB.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_PB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cb_PB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cb_PB.ItemHeight = 30;
            this.cb_PB.Location = new System.Drawing.Point(32, 404);
            this.cb_PB.Margin = new System.Windows.Forms.Padding(0);
            this.cb_PB.Name = "cb_PB";
            this.cb_PB.Size = new System.Drawing.Size(325, 36);
            this.cb_PB.TabIndex = 17;
            this.cb_PB.SelectedIndexChanged += new System.EventHandler(this.cb_PB_SelectedIndexChanged);
            // 
            // lbl_PB
            // 
            this.lbl_PB.AutoSize = true;
            this.lbl_PB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PB.Location = new System.Drawing.Point(28, 384);
            this.lbl_PB.Name = "lbl_PB";
            this.lbl_PB.Size = new System.Drawing.Size(88, 20);
            this.lbl_PB.TabIndex = 16;
            this.lbl_PB.Text = "Phòng ban";
            // 
            // cb_DV
            // 
            this.cb_DV.BackColor = System.Drawing.Color.Transparent;
            this.cb_DV.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_DV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DV.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_DV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cb_DV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cb_DV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cb_DV.ItemHeight = 30;
            this.cb_DV.Location = new System.Drawing.Point(32, 331);
            this.cb_DV.Margin = new System.Windows.Forms.Padding(0);
            this.cb_DV.Name = "cb_DV";
            this.cb_DV.Size = new System.Drawing.Size(325, 36);
            this.cb_DV.TabIndex = 15;
            this.cb_DV.SelectedIndexChanged += new System.EventHandler(this.cb_DV_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Đơn Vị";
            // 
            // txt_RePass
            // 
            this.txt_RePass.BorderRadius = 10;
            this.txt_RePass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_RePass.DefaultText = "";
            this.txt_RePass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_RePass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_RePass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_RePass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_RePass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_RePass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_RePass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_RePass.IconRight = global::Task_App.Properties.Resources.notshow;
            this.txt_RePass.Location = new System.Drawing.Point(32, 254);
            this.txt_RePass.Margin = new System.Windows.Forms.Padding(0);
            this.txt_RePass.Name = "txt_RePass";
            this.txt_RePass.PlaceholderText = "";
            this.txt_RePass.SelectedText = "";
            this.txt_RePass.Size = new System.Drawing.Size(325, 35);
            this.txt_RePass.TabIndex = 13;
            // 
            // lbl_RePass
            // 
            this.lbl_RePass.AutoSize = true;
            this.lbl_RePass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RePass.Location = new System.Drawing.Point(28, 234);
            this.lbl_RePass.Name = "lbl_RePass";
            this.lbl_RePass.Size = new System.Drawing.Size(152, 20);
            this.lbl_RePass.TabIndex = 12;
            this.lbl_RePass.Text = "Xác nhận mật khẩu";
            // 
            // txt_Pass
            // 
            this.txt_Pass.BorderRadius = 10;
            this.txt_Pass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Pass.DefaultText = "";
            this.txt_Pass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Pass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Pass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Pass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Pass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Pass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Pass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Pass.IconRight = global::Task_App.Properties.Resources.notshow;
            this.txt_Pass.Location = new System.Drawing.Point(32, 182);
            this.txt_Pass.Margin = new System.Windows.Forms.Padding(0);
            this.txt_Pass.Name = "txt_Pass";
            this.txt_Pass.PlaceholderText = "";
            this.txt_Pass.SelectedText = "";
            this.txt_Pass.Size = new System.Drawing.Size(325, 35);
            this.txt_Pass.TabIndex = 11;
            // 
            // lbl_Pass
            // 
            this.lbl_Pass.AutoSize = true;
            this.lbl_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Pass.Location = new System.Drawing.Point(28, 162);
            this.lbl_Pass.Name = "lbl_Pass";
            this.lbl_Pass.Size = new System.Drawing.Size(80, 20);
            this.lbl_Pass.TabIndex = 10;
            this.lbl_Pass.Text = "Mật Khẩu";
            // 
            // txt_UserName
            // 
            this.txt_UserName.BorderRadius = 10;
            this.txt_UserName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_UserName.DefaultText = "";
            this.txt_UserName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_UserName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_UserName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_UserName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_UserName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_UserName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_UserName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_UserName.Location = new System.Drawing.Point(32, 109);
            this.txt_UserName.Margin = new System.Windows.Forms.Padding(0);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.PlaceholderText = "";
            this.txt_UserName.SelectedText = "";
            this.txt_UserName.Size = new System.Drawing.Size(325, 35);
            this.txt_UserName.TabIndex = 9;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(28, 89);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(130, 20);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Tên Người Dùng";
            // 
            // txt_Email
            // 
            this.txt_Email.BorderRadius = 10;
            this.txt_Email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Email.DefaultText = "";
            this.txt_Email.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Email.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Email.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Email.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Email.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Email.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Email.Location = new System.Drawing.Point(32, 37);
            this.txt_Email.Margin = new System.Windows.Forms.Padding(0);
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.PlaceholderText = "";
            this.txt_Email.SelectedText = "";
            this.txt_Email.Size = new System.Drawing.Size(325, 35);
            this.txt_Email.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(28, 17);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 20);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email";
            // 
            // btn_DangKy
            // 
            this.btn_DangKy.BorderRadius = 10;
            this.btn_DangKy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_DangKy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_DangKy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_DangKy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_DangKy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.btn_DangKy.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangKy.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btn_DangKy.Location = new System.Drawing.Point(32, 537);
            this.btn_DangKy.Name = "btn_DangKy";
            this.btn_DangKy.Size = new System.Drawing.Size(325, 50);
            this.btn_DangKy.TabIndex = 20;
            this.btn_DangKy.Text = "Đăng Ký";
            this.btn_DangKy.Click += new System.EventHandler(this.btn_DangKy_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Register";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel panel2;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txt_Email;
        private Guna.UI2.WinForms.Guna2TextBox txt_UserName;
        private System.Windows.Forms.Label lblName;
        private Guna.UI2.WinForms.Guna2TextBox txt_RePass;
        private System.Windows.Forms.Label lbl_RePass;
        private Guna.UI2.WinForms.Guna2TextBox txt_Pass;
        private System.Windows.Forms.Label lbl_Pass;
        private Guna.UI2.WinForms.Guna2ComboBox cb_CV;
        private System.Windows.Forms.Label lbl_CV;
        private Guna.UI2.WinForms.Guna2ComboBox cb_PB;
        private System.Windows.Forms.Label lbl_PB;
        private Guna.UI2.WinForms.Guna2ComboBox cb_DV;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btn_DangKy;
    }
}