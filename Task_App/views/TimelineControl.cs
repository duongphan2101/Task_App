using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_App.views
{
    public partial class TimelineControl : UserControl
    {
        private List<TaskItem> tasks;
        private ToolTip toolTip;
        private int? hoveredTaskIndex = null;
        public TimelineControl()
        {
            InitializeComponent();
            tasks = new List<TaskItem>();
            DoubleBuffered = true;
            LoadMockData();
        }

        private void LoadMockData()
        {
            DateTime testStart = new DateTime(2023, 5, 1); // 1/5/2023
            DateTime testEnd = new DateTime(2023, 5, 31);  // 31/5/2023

            AddTask("Design", testStart.AddDays(2), testStart.AddDays(5), "InProgress");
            AddTask("Unit Testing", testStart.AddDays(10), testStart.AddDays(15), "InProgress");
            AddTask("Integration Testing", testStart.AddDays(20), testEnd.AddDays(-5), "Pending");
        }

        public void AddTask(string name, DateTime start, DateTime end, string status)
        {
            tasks.Add(new TaskItem { Name = name, Start = start, End = end, Status = status });
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate(); // Gọi lại vẽ khi kích thước thay đổi
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (tasks == null || tasks.Count == 0)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            float width = this.ClientSize.Width;
            float height = this.ClientSize.Height;

            // Xác định ngày bắt đầu và ngày kết thúc dựa trên các task
            DateTime minDate = tasks.Min(t => t.Start);
            DateTime maxDate = tasks.Max(t => t.End);

            if (minDate == DateTime.MaxValue || maxDate == DateTime.MinValue)
                return;

            float totalDays = (float)(maxDate - minDate).TotalDays;
            if (totalDays <= 0) totalDays = 1; // Tránh chia cho 0

            float yStep = height / (tasks.Count + 1);

            using (Pen timelinePen = new Pen(Color.Gray, 1))
            using (Pen taskPen = new Pen(Color.Black, 1))
            using (Font font = new Font(this.Font.FontFamily, 8)) // Sử dụng font cố định
            {
                // Vẽ trục ngày, vẽ tất cả các ngày
                DateTime cursor = minDate;
                while (cursor <= maxDate)
                {
                    float x = Math.Min(width - 10, Math.Max(0, (float)((cursor - minDate).TotalDays / totalDays * width)));
                    g.DrawLine(Pens.LightGray, x, 0, x, height);
                    g.DrawString(cursor.ToString("dd/MM"), font, Brushes.Black, x + 2, 0);
                    cursor = cursor.AddDays(1); // Vẽ từng ngày
                }

                // Vẽ các task
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];
                    float daysDiff = (float)(task.End - task.Start).TotalDays;
                    float xStart = Math.Min(width - 10, Math.Max(0, (float)((task.Start - minDate).TotalDays / totalDays * width)));
                    float xEnd = Math.Min(width - 10, Math.Max(xStart, xStart + (daysDiff / totalDays * width)));
                    float y = Math.Min(height - 10, (i + 1) * yStep);

                    Brush brush;
                    switch (task.Status.ToLower())
                    {
                        case "completed":
                            brush = Brushes.Green;
                            break;
                        case "inprogress":
                            brush = Brushes.Yellow;
                            break;
                        case "pending":
                            brush = Brushes.Red;
                            break;
                        default:
                            brush = Brushes.Gray;
                            break;
                    }

                    // Vẽ thanh task
                    if (xEnd > xStart)
                    {
                        g.FillRectangle(brush, xStart, y - 8, xEnd - xStart, 16);
                        g.DrawRectangle(taskPen, xStart, y - 8, xEnd - xStart, 16);
                    }

                    // Vẽ tên task và khoảng thời gian
                    string label = $"{task.Name} ({task.Start:dd/MM} - {task.End:dd/MM})";
                    SizeF textSize = g.MeasureString(label, font);
                    g.DrawString(label, font, Brushes.Black, xStart + 2, y - 8 - textSize.Height);

                    // Nếu là milestone (ví dụ duration = 0), vẽ hình thoi
                    if (daysDiff == 0 && xStart >= 0 && xStart < width)
                    {
                        PointF[] diamond = new PointF[]
                        {
                            new PointF(xStart, y),
                            new PointF(Math.Max(0, xStart - 5), Math.Max(0, y - 5)),
                            new PointF(xStart, Math.Max(0, y - 10)),
                            new PointF(Math.Min(width, xStart + 5), Math.Max(0, y - 5))
                        };
                        g.FillPolygon(brush, diamond);
                        g.DrawPolygon(taskPen, diamond);
                    }
                }
            }
        }


        private class TaskItem
        {
            public string Name { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string Status { get; set; }
        }
    }
}
