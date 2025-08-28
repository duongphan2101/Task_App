namespace Task_App.views
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tab = new Guna.UI2.WinForms.Guna2TabControl();
            this.accounts = new System.Windows.Forms.TabPage();
            this.panel_center = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_panel_center = new Guna.UI2.WinForms.Guna2Panel();
            this.panel_top = new Guna.UI2.WinForms.Guna2Panel();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();
            this.txt_TimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.all = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.data = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tab.SuspendLayout();
            this.accounts.SuspendLayout();
            this.panel_center.SuspendLayout();
            this.panel_panel_center.SuspendLayout();
            this.panel_top.SuspendLayout();
            this.flow.SuspendLayout();
            this.all.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tab.Controls.Add(this.accounts);
            this.tab.Controls.Add(this.all);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.ItemSize = new System.Drawing.Size(180, 60);
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Margin = new System.Windows.Forms.Padding(0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1382, 653);
            this.tab.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tab.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tab.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tab.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tab.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tab.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tab.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tab.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tab.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tab.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tab.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tab.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tab.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tab.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tab.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tab.TabButtonSize = new System.Drawing.Size(180, 60);
            this.tab.TabIndex = 1;
            this.tab.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            // 
            // accounts
            // 
            this.accounts.AutoScroll = true;
            this.accounts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.accounts.Controls.Add(this.panel_center);
            this.accounts.Controls.Add(this.panel_top);
            this.accounts.Location = new System.Drawing.Point(184, 4);
            this.accounts.Margin = new System.Windows.Forms.Padding(0);
            this.accounts.Name = "accounts";
            this.accounts.Padding = new System.Windows.Forms.Padding(10);
            this.accounts.Size = new System.Drawing.Size(1194, 645);
            this.accounts.TabIndex = 0;
            this.accounts.Text = "Accounts";
            this.accounts.ToolTipText = "Accounts";
            // 
            // panel_center
            // 
            this.panel_center.BackColor = System.Drawing.Color.Transparent;
            this.panel_center.Controls.Add(this.panel_panel_center);
            this.panel_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_center.Location = new System.Drawing.Point(10, 110);
            this.panel_center.Margin = new System.Windows.Forms.Padding(0);
            this.panel_center.Name = "panel_center";
            this.panel_center.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panel_center.Size = new System.Drawing.Size(1174, 525);
            this.panel_center.TabIndex = 1;
            // 
            // panel_panel_center
            // 
            this.panel_panel_center.BorderRadius = 15;
            this.panel_panel_center.Controls.Add(this.data);
            this.panel_panel_center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_panel_center.FillColor = System.Drawing.Color.White;
            this.panel_panel_center.Location = new System.Drawing.Point(0, 20);
            this.panel_panel_center.Margin = new System.Windows.Forms.Padding(0);
            this.panel_panel_center.Name = "panel_panel_center";
            this.panel_panel_center.Padding = new System.Windows.Forms.Padding(20);
            this.panel_panel_center.Size = new System.Drawing.Size(1174, 505);
            this.panel_panel_center.TabIndex = 0;
            // 
            // panel_top
            // 
            this.panel_top.BackColor = System.Drawing.Color.Transparent;
            this.panel_top.BorderRadius = 15;
            this.panel_top.Controls.Add(this.flow);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.FillColor = System.Drawing.Color.White;
            this.panel_top.Location = new System.Drawing.Point(10, 10);
            this.panel_top.Margin = new System.Windows.Forms.Padding(0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(1174, 100);
            this.panel_top.TabIndex = 0;
            // 
            // flow
            // 
            this.flow.AutoSize = true;
            this.flow.BackColor = System.Drawing.Color.Transparent;
            this.flow.Controls.Add(this.txt_TimKiem);
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.Location = new System.Drawing.Point(0, 0);
            this.flow.Margin = new System.Windows.Forms.Padding(0);
            this.flow.Name = "flow";
            this.flow.Padding = new System.Windows.Forms.Padding(20, 25, 20, 20);
            this.flow.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flow.Size = new System.Drawing.Size(1174, 100);
            this.flow.TabIndex = 0;
            // 
            // txt_TimKiem
            // 
            this.txt_TimKiem.BorderRadius = 15;
            this.txt_TimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_TimKiem.DefaultText = "";
            this.txt_TimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_TimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_TimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_TimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_TimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_TimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_TimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_TimKiem.IconRight = ((System.Drawing.Image)(resources.GetObject("txt_TimKiem.IconRight")));
            this.txt_TimKiem.IconRightOffset = new System.Drawing.Point(10, 0);
            this.txt_TimKiem.Location = new System.Drawing.Point(706, 25);
            this.txt_TimKiem.Margin = new System.Windows.Forms.Padding(0);
            this.txt_TimKiem.Name = "txt_TimKiem";
            this.txt_TimKiem.PlaceholderText = "Tìm kiếm";
            this.txt_TimKiem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_TimKiem.SelectedText = "";
            this.txt_TimKiem.Size = new System.Drawing.Size(428, 48);
            this.txt_TimKiem.TabIndex = 0;
            this.txt_TimKiem.TextChanged += new System.EventHandler(this.txt_TimKiem_TextChanged);
            // 
            // all
            // 
            this.all.BackColor = System.Drawing.Color.White;
            this.all.Controls.Add(this.label1);
            this.all.Location = new System.Drawing.Point(184, 4);
            this.all.Margin = new System.Windows.Forms.Padding(0);
            this.all.Name = "all";
            this.all.Padding = new System.Windows.Forms.Padding(10);
            this.all.Size = new System.Drawing.Size(1194, 645);
            this.all.TabIndex = 1;
            this.all.Text = "All";
            this.all.ToolTipText = "all";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1174, 625);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Account";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // data
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.data.ColumnHeadersHeight = 18;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.data.DefaultCellStyle = dataGridViewCellStyle3;
            this.data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.Location = new System.Drawing.Point(20, 20);
            this.data.Margin = new System.Windows.Forms.Padding(0);
            this.data.Name = "data";
            this.data.RowHeadersVisible = false;
            this.data.RowHeadersWidth = 50;
            this.data.RowTemplate.Height = 24;
            this.data.Size = new System.Drawing.Size(1134, 465);
            this.data.TabIndex = 0;
            this.data.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.data.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.data.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.data.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.data.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.data.ThemeStyle.HeaderStyle.Height = 18;
            this.data.ThemeStyle.ReadOnly = false;
            this.data.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.data.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.data.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data.ThemeStyle.RowsStyle.Height = 24;
            this.data.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_CellContentClick);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1382, 653);
            this.Controls.Add(this.tab);
            this.Name = "AdminForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADMIN";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.tab.ResumeLayout(false);
            this.accounts.ResumeLayout(false);
            this.panel_center.ResumeLayout(false);
            this.panel_panel_center.ResumeLayout(false);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.flow.ResumeLayout(false);
            this.all.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tab;
        private System.Windows.Forms.TabPage accounts;
        private System.Windows.Forms.TabPage all;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel panel_center;
        private Guna.UI2.WinForms.Guna2Panel panel_top;
        private System.Windows.Forms.FlowLayoutPanel flow;
        private Guna.UI2.WinForms.Guna2TextBox txt_TimKiem;
        private Guna.UI2.WinForms.Guna2Panel panel_panel_center;
        private Guna.UI2.WinForms.Guna2DataGridView data;
    }
}