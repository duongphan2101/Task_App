namespace Task_App
{
    partial class FormLogin
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
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnLogin = new Guna.UI2.WinForms.Guna2Button();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_DangKy = new Guna.UI2.WinForms.Guna2Button();
            this.loginlayoutControl1ConvertedLayout = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.panelControl1item = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl2item = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginlayoutControl1ConvertedLayout)).BeginInit();
            this.loginlayoutControl1ConvertedLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1item)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2item)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(33, 192);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(61, 24);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Bahnschrift SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.ForeColor = System.Drawing.Color.Black;
            this.lblPass.Location = new System.Drawing.Point(33, 277);
            this.lblPass.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(93, 24);
            this.lblPass.TabIndex = 0;
            this.lblPass.Text = "Mật khẩu";
            // 
            // btnLogin
            // 
            this.btnLogin.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderRadius = 10;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(39, 379);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(251, 52);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.Click += new System.EventHandler(this.OnLoginBtnPressed);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.Transparent;
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.ForeColor = System.Drawing.Color.DimGray;
            this.txtEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtEmail.Location = new System.Drawing.Point(39, 225);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Nhập địa chỉ email";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(251, 42);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPressed);
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.Transparent;
            this.txtPass.BorderRadius = 10;
            this.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPass.DefaultText = "";
            this.txtPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPass.ForeColor = System.Drawing.Color.DimGray;
            this.txtPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPass.IconRight = global::Task_App.Properties.Resources.notshow;
            this.txtPass.IconRightOffset = new System.Drawing.Point(10, 0);
            this.txtPass.IconRightSize = new System.Drawing.Size(15, 15);
            this.txtPass.Location = new System.Drawing.Point(39, 310);
            this.txtPass.Margin = new System.Windows.Forms.Padding(0);
            this.txtPass.Name = "txtPass";
            this.txtPass.PlaceholderText = "Nhập mật khẩu";
            this.txtPass.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPass.SelectedText = "";
            this.txtPass.Size = new System.Drawing.Size(251, 42);
            this.txtPass.TabIndex = 2;
            this.txtPass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPressed);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(472, 601);
            this.panelControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 601);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 601);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng Nhập";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.panel2);
            this.panelControl2.Location = new System.Drawing.Point(472, 0);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(328, 601);
            this.panelControl2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.btn_DangKy);
            this.panel2.Controls.Add(this.txtPass);
            this.panel2.Controls.Add(this.txtEmail);
            this.panel2.Controls.Add(this.lblEmail);
            this.panel2.Controls.Add(this.btnLogin);
            this.panel2.Controls.Add(this.lblPass);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 601);
            this.panel2.TabIndex = 0;
            // 
            // btn_DangKy
            // 
            this.btn_DangKy.BorderColor = System.Drawing.Color.Transparent;
            this.btn_DangKy.BorderRadius = 10;
            this.btn_DangKy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DangKy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_DangKy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_DangKy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_DangKy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_DangKy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_DangKy.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DangKy.ForeColor = System.Drawing.Color.Black;
            this.btn_DangKy.Location = new System.Drawing.Point(39, 540);
            this.btn_DangKy.Margin = new System.Windows.Forms.Padding(0);
            this.btn_DangKy.Name = "btn_DangKy";
            this.btn_DangKy.Size = new System.Drawing.Size(251, 52);
            this.btn_DangKy.TabIndex = 4;
            this.btn_DangKy.Text = "Đăng Ký";
            this.btn_DangKy.Click += new System.EventHandler(this.btn_DangKy_Click);
            // 
            // loginlayoutControl1ConvertedLayout
            // 
            this.loginlayoutControl1ConvertedLayout.Controls.Add(this.panelControl1);
            this.loginlayoutControl1ConvertedLayout.Controls.Add(this.panelControl2);
            this.loginlayoutControl1ConvertedLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginlayoutControl1ConvertedLayout.Location = new System.Drawing.Point(0, 0);
            this.loginlayoutControl1ConvertedLayout.Margin = new System.Windows.Forms.Padding(4);
            this.loginlayoutControl1ConvertedLayout.Name = "loginlayoutControl1ConvertedLayout";
            this.loginlayoutControl1ConvertedLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(906, 372, 650, 400);
            this.loginlayoutControl1ConvertedLayout.Root = this.layoutControlGroup1;
            this.loginlayoutControl1ConvertedLayout.Size = new System.Drawing.Size(800, 601);
            this.loginlayoutControl1ConvertedLayout.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.panelControl1item,
            this.panelControl2item});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(800, 601);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // panelControl1item
            // 
            this.panelControl1item.Control = this.panelControl1;
            this.panelControl1item.Location = new System.Drawing.Point(0, 0);
            this.panelControl1item.Name = "panelControl1item";
            this.panelControl1item.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.panelControl1item.Size = new System.Drawing.Size(472, 601);
            this.panelControl1item.TextVisible = false;
            // 
            // panelControl2item
            // 
            this.panelControl2item.Control = this.panelControl2;
            this.panelControl2item.Location = new System.Drawing.Point(472, 0);
            this.panelControl2item.Name = "panelControl2item";
            this.panelControl2item.OptionsTableLayoutItem.ColumnIndex = 1;
            this.panelControl2item.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.panelControl2item.Size = new System.Drawing.Size(328, 601);
            this.panelControl2item.TextVisible = false;
            // 
            // FormLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.loginlayoutControl1ConvertedLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 648);
            this.MinimumSize = new System.Drawing.Size(818, 648);
            this.Name = "FormLogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginlayoutControl1ConvertedLayout)).EndInit();
            this.loginlayoutControl1ConvertedLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1item)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2item)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPass;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtPass;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraLayout.LayoutControl loginlayoutControl1ConvertedLayout;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem panelControl1item;
        private DevExpress.XtraLayout.LayoutControlItem panelControl2item;
        private Guna.UI2.WinForms.Guna2Button btn_DangKy;
        private System.Windows.Forms.Label label1;
    }
}

