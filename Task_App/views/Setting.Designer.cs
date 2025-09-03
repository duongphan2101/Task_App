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
            this.flow_1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_path = new System.Windows.Forms.Label();
            this.txt_path = new Guna.UI2.WinForms.Guna2TextBox();
            this.main_flow.SuspendLayout();
            this.flow_1.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_flow
            // 
            this.main_flow.Controls.Add(this.flow_1);
            this.main_flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.main_flow.Location = new System.Drawing.Point(10, 10);
            this.main_flow.Margin = new System.Windows.Forms.Padding(0);
            this.main_flow.Name = "main_flow";
            this.main_flow.Size = new System.Drawing.Size(1414, 650);
            this.main_flow.TabIndex = 0;
            // 
            // flow_1
            // 
            this.flow_1.Controls.Add(this.lbl_path);
            this.flow_1.Controls.Add(this.txt_path);
            this.flow_1.Location = new System.Drawing.Point(0, 0);
            this.flow_1.Margin = new System.Windows.Forms.Padding(0);
            this.flow_1.Name = "flow_1";
            this.flow_1.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.flow_1.Size = new System.Drawing.Size(1408, 50);
            this.flow_1.TabIndex = 0;
            // 
            // lbl_path
            // 
            this.lbl_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_path.Location = new System.Drawing.Point(20, 0);
            this.lbl_path.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(313, 50);
            this.lbl_path.TabIndex = 0;
            this.lbl_path.Text = "Thư mục lưu trữ";
            this.lbl_path.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_path
            // 
            this.txt_path.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_path.DefaultText = "";
            this.txt_path.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_path.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_path.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_path.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_path.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_path.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_path.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_path.Location = new System.Drawing.Point(383, 0);
            this.txt_path.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.txt_path.Name = "txt_path";
            this.txt_path.PlaceholderText = "";
            this.txt_path.SelectedText = "";
            this.txt_path.Size = new System.Drawing.Size(933, 46);
            this.txt_path.TabIndex = 1;
            this.txt_path.Click += new System.EventHandler(this.txt_path_Click);
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
            this.Size = new System.Drawing.Size(1434, 670);
            this.Load += new System.EventHandler(this.User_Setting_Load);
            this.main_flow.ResumeLayout(false);
            this.flow_1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel main_flow;
        private System.Windows.Forms.FlowLayoutPanel flow_1;
        private System.Windows.Forms.Label lbl_path;
        private Guna.UI2.WinForms.Guna2TextBox txt_path;
    }
}
