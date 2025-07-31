namespace Task_App
{
    partial class DashboardControlViewer
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
            this.components = new System.ComponentModel.Container();
            this.viewer = new DevExpress.DashboardWin.DashboardViewer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).BeginInit();
            this.SuspendLayout();
            // 
            // viewer
            // 
            this.viewer.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.viewer.Appearance.Options.UseBackColor = true;
            this.viewer.AsyncMode = true;
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.Location = new System.Drawing.Point(10, 10);
            this.viewer.Margin = new System.Windows.Forms.Padding(0);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(827, 407);
            this.viewer.TabIndex = 0;
            // 
            // DashboardControlViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.viewer);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DashboardControlViewer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(847, 427);
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.DashboardWin.DashboardViewer viewer;
    }
}
