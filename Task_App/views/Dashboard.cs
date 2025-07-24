using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_App.views
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void flow_Head_Resize(object sender, EventArgs e)
        {
            int panelWidth = flow_Head.Width / 3;

            head_panel_1.Width = panelWidth;
            head_panel_2.Width = panelWidth;
            head_panel_3.Width = panelWidth;

        }
    }
}
