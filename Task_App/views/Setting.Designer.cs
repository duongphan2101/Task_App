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
            this.lbl_path = new System.Windows.Forms.Label();
            this.txt_path = new Guna.UI2.WinForms.Guna2TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_passEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
            // lbl_path
            // 
            this.lbl_path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_path.Location = new System.Drawing.Point(5, 5);
            this.lbl_path.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(550, 50);
            this.lbl_path.TabIndex = 0;
            this.lbl_path.Text = "Thư mục lưu trữ";
            this.lbl_path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.txt_path.Location = new System.Drawing.Point(560, 10);
            this.txt_path.Margin = new System.Windows.Forms.Padding(5);
            this.txt_path.Name = "txt_path";
            this.txt_path.PlaceholderText = "";
            this.txt_path.SelectedText = "";
            this.txt_path.Size = new System.Drawing.Size(540, 40);
            this.txt_path.TabIndex = 1;
            this.txt_path.Click += new System.EventHandler(this.txt_path_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(550, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mật khẩu email";
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
            this.txt_passEmail.Location = new System.Drawing.Point(560, 60);
            this.txt_passEmail.Margin = new System.Windows.Forms.Padding(5);
            this.txt_passEmail.Name = "txt_passEmail";
            this.txt_passEmail.PlaceholderText = "";
            this.txt_passEmail.SelectedText = "";
            this.txt_passEmail.Size = new System.Drawing.Size(540, 40);
            this.txt_passEmail.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_path, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_passEmail, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_path, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1110, 110);
            this.tableLayoutPanel1.TabIndex = 2;
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
    }
}
