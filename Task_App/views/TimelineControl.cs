using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_App.Model;
using Task_App.Response;
using Task_App.TaskApp_Dao;

namespace Task_App.views
{
    public partial class TimelineControl : UserControl
    {
        private List<ChiTietCongViec> tasks;
        private ToolTip toolTip;
        private int? hoveredTaskIndex = null;
        public class TaskClickedEventArgs : EventArgs
        {
            public ChiTietCongViec Task { get; set; }
        }
        public event EventHandler<TaskClickedEventArgs> TaskClicked;

        public TimelineControl(List<ChiTietCongViec> tasks)
        {
            InitializeComponent();
            this.tasks = tasks;
            DoubleBuffered = true;
            toolTip = new ToolTip();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hoveredTaskIndex = null;
            toolTip.Hide(this);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (tasks == null) return;

            int? newHover = null;

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                if (!task.NgayNhanCongViec.HasValue || !task.NgayKetThucCongViec.HasValue)
                    continue;

                DateTime minDate = tasks.Where(t => t.NgayNhanCongViec.HasValue)
                                        .Min(t => t.NgayNhanCongViec.Value);
                DateTime maxDate = tasks.Where(t => t.NgayKetThucCongViec.HasValue)
                                        .Max(t => t.NgayKetThucCongViec.Value);

                float totalDays = (float)(maxDate - minDate).TotalDays;
                if (totalDays <= 0) totalDays = 1;

                float width = this.ClientSize.Width;
                float height = this.ClientSize.Height;
                float yStep = height / (tasks.Count + 1);

                DateTime start = task.NgayNhanCongViec.Value;
                DateTime end = task.NgayKetThucCongViec.Value;

                float xStart = (float)((start - minDate).TotalDays / totalDays * width);
                float xEnd = (float)((end - minDate).TotalDays / totalDays * width);
                float y = (i + 1) * yStep;

                float rectWidth = (xEnd - xStart <= 0) ? 4 : (xEnd - xStart);
                RectangleF rect = new RectangleF(xStart, y - 8, rectWidth, 16);

                if (rect.Contains(e.Location))
                {
                    newHover = i;
                    toolTip.Show($"{task.TieuDe}\n{start:dd/MM} - {end:dd/MM}", this, e.Location.X + 15, e.Location.Y + 15);
                    break;
                }
            }

            if (newHover != hoveredTaskIndex)
            {
                hoveredTaskIndex = newHover;
                Invalidate();
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (hoveredTaskIndex.HasValue && hoveredTaskIndex.Value >= 0 && hoveredTaskIndex.Value < tasks.Count)
            {
                var clickedTask = tasks[hoveredTaskIndex.Value];
                TaskClicked?.Invoke(this, new TaskClickedEventArgs { Task = clickedTask });
            }
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

            // Xác định ngày min/max (bỏ qua task null ngày)
            DateTime minDate = tasks.Where(t => t.NgayNhanCongViec.HasValue)
                                    .Min(t => t.NgayNhanCongViec.Value);
            DateTime maxDate = tasks.Where(t => t.NgayKetThucCongViec.HasValue)
                                    .Max(t => t.NgayKetThucCongViec.Value);

            float totalDays = (float)(maxDate - minDate).TotalDays;
            if (totalDays <= 0) totalDays = 1;

            float yStep = height / (tasks.Count + 1);

            using (Pen timelinePen = new Pen(Color.Gray, 1))
            using (Pen taskPen = new Pen(Color.Black, 1))
            using (Font font = new Font(this.Font.FontFamily, 8))
            {
                // Vẽ trục ngày
                DateTime cursor = minDate;
                while (cursor <= maxDate)
                {
                    float x = Math.Min(width - 10,
                        Math.Max(0, (float)((cursor - minDate).TotalDays / totalDays * width)));

                    g.DrawLine(Pens.LightGray, x, 0, x, height);
                    g.DrawString(cursor.ToString("dd/MM"), font, Brushes.Black, x + 2, 0);

                    cursor = cursor.AddDays(1);
                }

                // Vẽ task
                for (int i = 0; i < tasks.Count; i++)
                {
                    var task = tasks[i];
                    if (!task.NgayNhanCongViec.HasValue || !task.NgayKetThucCongViec.HasValue)
                        continue; // bỏ qua task thiếu ngày

                    DateTime start = task.NgayNhanCongViec.Value;
                    DateTime end = task.NgayKetThucCongViec.Value;

                    float daysDiff = (float)(end - start).TotalDays;
                    float xStart = Math.Min(width - 10,
                        Math.Max(0, (float)((start - minDate).TotalDays / totalDays * width)));
                    float xEnd = Math.Min(width - 10,
                        Math.Max(xStart, (float)((end - minDate).TotalDays / totalDays * width)));
                    float margin = 8; // khoảng cách top/bottom giữa các task
                    float y = Math.Min(height - 10, (i + 1) * yStep + i * margin);


                    // màu trạng thái
                    Brush brush = Brushes.Gray;
                    switch (task.TrangThai)
                    {
                        case 0: brush = Brushes.LightGray; break;
                        case 1: brush = Brushes.LightBlue; break;
                        case 2: brush = Brushes.LightGreen; break;
                        case 3: brush = Brushes.Red; break;
                    }

                    // Nếu task chỉ có 1 ngày -> rectangle mỏng (>= 4px)
                    float rectWidth = (daysDiff <= 0) ? 4 : (xEnd - xStart);
                    g.FillRectangle(brush, xStart, y - 8, rectWidth, 16);
                    g.DrawRectangle(taskPen, xStart, y - 8, rectWidth, 16);

                    // Vẽ tên task
                    string label = $"{task.TieuDe} Tiến độ: {task.TienDo}%";
                    SizeF textSize = g.MeasureString(label, font);
                    g.DrawString(label, font, Brushes.Black, xStart + 2, y - 8 - textSize.Height);
                }
            }
        }


    }
}
