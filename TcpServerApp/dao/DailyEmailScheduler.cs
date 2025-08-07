using System;
using System.Timers;
using TcpServerApp.DAO;

public class DailyEmailScheduler
{
    private Timer dailyTimer;
    private CongViecDAO congViecDAO;

    public DailyEmailScheduler()
    {
        dailyTimer = new Timer(GetNextInterval());
        dailyTimer.Elapsed += OnTimedEvent;
        dailyTimer.AutoReset = false;
        congViecDAO = new CongViecDAO();
    }

    public void Start()
    {
        dailyTimer.Start();
    }

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        try
        {
            congViecDAO.Auto_SendEmail();
            congViecDAO.Auto_SendEmail_NhacNho();
            congViecDAO.CapNhatTrangThaiTreHan();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi khi gửi email định kỳ: " + ex.Message);
        }

        dailyTimer.Interval = GetNextInterval();
        dailyTimer.Start();
    }

    //private double GetNextInterval()
    //{
    //    DateTime now = DateTime.Now;
    //    DateTime next7AM = now.Hour < 7
    //        ? now.Date.AddHours(7)
    //        : now.Date.AddDays(1).AddHours(7);

    //    return (next7AM - now).TotalMilliseconds;
    //}

    private double GetNextInterval()
    {
        DateTime now = DateTime.Now;
        DateTime nextRunTime = now.Hour < 9 || (now.Hour == 9 && now.Minute < 27)
            ? now.Date.AddHours(9).AddMinutes(27)
            : now.Date.AddDays(1).AddHours(9).AddMinutes(27);

        return (nextRunTime - now).TotalMilliseconds;
    }

}
