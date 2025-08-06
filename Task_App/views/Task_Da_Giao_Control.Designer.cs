using System.Drawing;

namespace Task_App.views
{
    partial class Create_Task_Control
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_Task_Control));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Panel_TDG_main = new Guna.UI2.WinForms.Guna2Panel();
            this.TDG_Panel_Bottom = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_AddTask = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.Panel_Bottom_Left = new Guna.UI2.WinForms.Guna2Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radio_DaHoanThanh = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radio_Xep_Theo_Ngay = new Guna.UI2.WinForms.Guna2RadioButton();
            this.data_test = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Panel_TDG_main.SuspendLayout();
            this.TDG_Panel_Bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddTask)).BeginInit();
            this.Panel_Bottom_Left.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_test)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_TDG_main
            // 
            this.Panel_TDG_main.BackColor = System.Drawing.Color.Transparent;
            this.Panel_TDG_main.Controls.Add(this.TDG_Panel_Bottom);
            this.Panel_TDG_main.Controls.Add(this.data_test);
            this.Panel_TDG_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_TDG_main.FillColor = System.Drawing.Color.Teal;
            this.Panel_TDG_main.Location = new System.Drawing.Point(11, 10);
            this.Panel_TDG_main.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_TDG_main.Name = "Panel_TDG_main";
            this.Panel_TDG_main.Size = new System.Drawing.Size(829, 402);
            this.Panel_TDG_main.TabIndex = 0;
            // 
            // TDG_Panel_Bottom
            // 
            this.TDG_Panel_Bottom.BackColor = System.Drawing.Color.Transparent;
            this.TDG_Panel_Bottom.Controls.Add(this.btn_AddTask);
            this.TDG_Panel_Bottom.Controls.Add(this.Panel_Bottom_Left);
            this.TDG_Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TDG_Panel_Bottom.FillColor = System.Drawing.Color.White;
            this.TDG_Panel_Bottom.Location = new System.Drawing.Point(0, 354);
            this.TDG_Panel_Bottom.Margin = new System.Windows.Forms.Padding(0);
            this.TDG_Panel_Bottom.Name = "TDG_Panel_Bottom";
            this.TDG_Panel_Bottom.Size = new System.Drawing.Size(829, 48);
            this.TDG_Panel_Bottom.TabIndex = 1;
            // 
            // btn_AddTask
            // 
            this.btn_AddTask.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddTask.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddTask.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_AddTask.FillColor = System.Drawing.Color.Transparent;
            this.btn_AddTask.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddTask.Image")));
            this.btn_AddTask.ImageRotate = 0F;
            this.btn_AddTask.Location = new System.Drawing.Point(781, 0);
            this.btn_AddTask.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AddTask.Name = "btn_AddTask";
            this.btn_AddTask.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btn_AddTask.Size = new System.Drawing.Size(48, 48);
            this.btn_AddTask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btn_AddTask.TabIndex = 1;
            this.btn_AddTask.TabStop = false;
            this.btn_AddTask.Click += new System.EventHandler(this.btn_AddTask_Click);
            this.btn_AddTask.MouseHover += new System.EventHandler(this.btn_AddTask_MouseHover);
            // 
            // Panel_Bottom_Left
            // 
            this.Panel_Bottom_Left.Controls.Add(this.flowLayoutPanel1);
            this.Panel_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Bottom_Left.FillColor = System.Drawing.Color.White;
            this.Panel_Bottom_Left.Location = new System.Drawing.Point(0, 0);
            this.Panel_Bottom_Left.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Panel_Bottom_Left.Name = "Panel_Bottom_Left";
            this.Panel_Bottom_Left.Size = new System.Drawing.Size(829, 48);
            this.Panel_Bottom_Left.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.radio_DaHoanThanh);
            this.flowLayoutPanel1.Controls.Add(this.radio_Xep_Theo_Ngay);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(829, 48);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // radio_DaHoanThanh
            // 
            this.radio_DaHoanThanh.AutoSize = true;
            this.radio_DaHoanThanh.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radio_DaHoanThanh.CheckedState.BorderThickness = 0;
            this.radio_DaHoanThanh.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radio_DaHoanThanh.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radio_DaHoanThanh.CheckedState.InnerOffset = -4;
            this.radio_DaHoanThanh.Location = new System.Drawing.Point(10, 8);
            this.radio_DaHoanThanh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radio_DaHoanThanh.Name = "radio_DaHoanThanh";
            this.radio_DaHoanThanh.Size = new System.Drawing.Size(129, 20);
            this.radio_DaHoanThanh.TabIndex = 0;
            this.radio_DaHoanThanh.Text = "Theo Trạng Thái";
            this.radio_DaHoanThanh.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radio_DaHoanThanh.UncheckedState.BorderThickness = 2;
            this.radio_DaHoanThanh.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radio_DaHoanThanh.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radio_DaHoanThanh.CheckedChanged += new System.EventHandler(this.radio_DaHoanThanh_CheckedChanged);
            // 
            // radio_Xep_Theo_Ngay
            // 
            this.radio_Xep_Theo_Ngay.AutoSize = true;
            this.radio_Xep_Theo_Ngay.Checked = true;
            this.radio_Xep_Theo_Ngay.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radio_Xep_Theo_Ngay.CheckedState.BorderThickness = 0;
            this.radio_Xep_Theo_Ngay.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.radio_Xep_Theo_Ngay.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radio_Xep_Theo_Ngay.CheckedState.InnerOffset = -4;
            this.radio_Xep_Theo_Ngay.Location = new System.Drawing.Point(145, 8);
            this.radio_Xep_Theo_Ngay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radio_Xep_Theo_Ngay.Name = "radio_Xep_Theo_Ngay";
            this.radio_Xep_Theo_Ngay.Size = new System.Drawing.Size(96, 20);
            this.radio_Xep_Theo_Ngay.TabIndex = 1;
            this.radio_Xep_Theo_Ngay.TabStop = true;
            this.radio_Xep_Theo_Ngay.Text = "Theo Ngày";
            this.radio_Xep_Theo_Ngay.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radio_Xep_Theo_Ngay.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radio_Xep_Theo_Ngay.UncheckedState.BorderThickness = 2;
            this.radio_Xep_Theo_Ngay.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radio_Xep_Theo_Ngay.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radio_Xep_Theo_Ngay.CheckedChanged += new System.EventHandler(this.radio_Xep_Theo_Ngay_CheckedChanged);
            // 
            // data_test
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.data_test.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.data_test.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.data_test.ColumnHeadersHeight = 18;
            this.data_test.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.data_test.DefaultCellStyle = dataGridViewCellStyle3;
            this.data_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.data_test.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data_test.Location = new System.Drawing.Point(0, 0);
            this.data_test.Margin = new System.Windows.Forms.Padding(0);
            this.data_test.Name = "data_test";
            this.data_test.RowHeadersVisible = false;
            this.data_test.RowHeadersWidth = 50;
            this.data_test.RowTemplate.Height = 24;
            this.data_test.Size = new System.Drawing.Size(829, 402);
            this.data_test.TabIndex = 0;
            this.data_test.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.data_test.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.data_test.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.data_test.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.data_test.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.data_test.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.data_test.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.data_test.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.data_test.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.data_test.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data_test.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.data_test.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.data_test.ThemeStyle.HeaderStyle.Height = 18;
            this.data_test.ThemeStyle.ReadOnly = false;
            this.data_test.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.data_test.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.data_test.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.data_test.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data_test.ThemeStyle.RowsStyle.Height = 24;
            this.data_test.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.White;
            this.data_test.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.data_test.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_test_CellClick);
            this.data_test.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.data_test_CellFormatting);
            // 
            // Create_Task_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Panel_TDG_main);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Create_Task_Control";
            this.Padding = new System.Windows.Forms.Padding(11, 10, 11, 10);
            this.Size = new System.Drawing.Size(851, 422);
            this.Panel_TDG_main.ResumeLayout(false);
            this.TDG_Panel_Bottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_AddTask)).EndInit();
            this.Panel_Bottom_Left.ResumeLayout(false);
            this.Panel_Bottom_Left.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_test)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel Panel_TDG_main;
        //private Task_Application_DBDataSet task_Application_DBDataSet;
        //private Task_Application_DBDataSetTableAdapters.CongViecTableAdapter congViecTableAdapter;
        //private Task_Application_DBDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private Guna.UI2.WinForms.Guna2DataGridView data_test;
        private Guna.UI2.WinForms.Guna2Panel TDG_Panel_Bottom;
        private Guna.UI2.WinForms.Guna2Panel Panel_Bottom_Left;
        private Guna.UI2.WinForms.Guna2CirclePictureBox btn_AddTask;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Guna.UI2.WinForms.Guna2RadioButton radio_DaHoanThanh;
        private Guna.UI2.WinForms.Guna2RadioButton radio_Xep_Theo_Ngay;
    }
}
