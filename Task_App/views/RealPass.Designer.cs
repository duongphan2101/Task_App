namespace Task_App.views
{
    partial class RealPass
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
            this.label1 = new System.Windows.Forms.Label();
            this.main = new Guna.UI2.WinForms.Guna2Panel();
            this.main_panel = new Guna.UI2.WinForms.Guna2Panel();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.txtTmpPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.eyes = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btn = new Guna.UI2.WinForms.Guna2Button();
            this.main.SuspendLayout();
            this.main_panel.SuspendLayout();
            this.flow.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lưu ý: Vui lòng đảm bảo nhập đúng mật khẩu của email hiện tại, vì đây dùng để gửi" +
    " email phân công công việc";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // main
            // 
            this.main.Controls.Add(this.main_panel);
            this.main.Controls.Add(this.label1);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.FillColor = System.Drawing.Color.White;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Margin = new System.Windows.Forms.Padding(0);
            this.main.Name = "main";
            this.main.Padding = new System.Windows.Forms.Padding(8);
            this.main.Size = new System.Drawing.Size(512, 134);
            this.main.TabIndex = 0;
            // 
            // main_panel
            // 
            this.main_panel.BackColor = System.Drawing.Color.White;
            this.main_panel.Controls.Add(this.flow);
            this.main_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_panel.Location = new System.Drawing.Point(8, 59);
            this.main_panel.Margin = new System.Windows.Forms.Padding(0);
            this.main_panel.Name = "main_panel";
            this.main_panel.Padding = new System.Windows.Forms.Padding(15, 32, 15, 32);
            this.main_panel.Size = new System.Drawing.Size(496, 67);
            this.main_panel.TabIndex = 3;
            // 
            // flow
            // 
            this.flow.AutoSize = true;
            this.flow.Controls.Add(this.txtTmpPass);
            this.flow.Controls.Add(this.eyes);
            this.flow.Controls.Add(this.btn);
            this.flow.Location = new System.Drawing.Point(2, 11);
            this.flow.Margin = new System.Windows.Forms.Padding(0);
            this.flow.Name = "flow";
            this.flow.Padding = new System.Windows.Forms.Padding(4);
            this.flow.Size = new System.Drawing.Size(495, 59);
            this.flow.TabIndex = 0;
            // 
            // txtTmpPass
            // 
            this.txtTmpPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTmpPass.DefaultText = "";
            this.txtTmpPass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTmpPass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTmpPass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTmpPass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTmpPass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTmpPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTmpPass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTmpPass.Location = new System.Drawing.Point(6, 7);
            this.txtTmpPass.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTmpPass.Name = "txtTmpPass";
            this.txtTmpPass.PlaceholderText = "";
            this.txtTmpPass.SelectedText = "";
            this.txtTmpPass.Size = new System.Drawing.Size(278, 39);
            this.txtTmpPass.TabIndex = 0;
            // 
            // eyes
            // 
            this.eyes.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.eyes.HoverState.ImageSize = new System.Drawing.Size(24, 24);
            this.eyes.Image = global::Task_App.Properties.Resources.notshow;
            this.eyes.ImageOffset = new System.Drawing.Point(0, 0);
            this.eyes.ImageRotate = 0F;
            this.eyes.ImageSize = new System.Drawing.Size(24, 24);
            this.eyes.Location = new System.Drawing.Point(288, 6);
            this.eyes.Margin = new System.Windows.Forms.Padding(2);
            this.eyes.Name = "eyes";
            this.eyes.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.eyes.Size = new System.Drawing.Size(48, 44);
            this.eyes.TabIndex = 1;
            this.eyes.Click += new System.EventHandler(this.eyes_Click);
            // 
            // btn
            // 
            this.btn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn.ForeColor = System.Drawing.Color.White;
            this.btn.Location = new System.Drawing.Point(338, 4);
            this.btn.Margin = new System.Windows.Forms.Padding(0);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(135, 37);
            this.btn.TabIndex = 2;
            this.btn.Text = "Xác nhận";
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // RealPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 134);
            this.Controls.Add(this.main);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "RealPass";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RealPass_Load);
            this.main.ResumeLayout(false);
            this.main_panel.ResumeLayout(false);
            this.main_panel.PerformLayout();
            this.flow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel main;
        private Guna.UI2.WinForms.Guna2Panel main_panel;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private Guna.UI2.WinForms.Guna2TextBox txtTmpPass;
        private Guna.UI2.WinForms.Guna2ImageButton eyes;
        private Guna.UI2.WinForms.Guna2Button btn;
    }
}