using APIServerApp.services;
using Microsoft.Extensions.DependencyInjection;

public class DailyEmailJob : IHostedService, IDisposable
{
    private readonly ILogger<DailyEmailJob> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public DailyEmailJob(ILogger<DailyEmailJob> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var next8h = DateTime.Today.AddHours(8);
        var next16h = DateTime.Today.AddHours(16);

        if (now > next8h) next8h = next8h.AddDays(1);
        if (now > next16h) next16h = next16h.AddDays(1);

        var dueTime = next8h - now;
        _timer = new Timer(DoWork, null, dueTime, TimeSpan.FromHours(8));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        using var scope = _scopeFactory.CreateScope();
        var autoJobService = scope.ServiceProvider.GetRequiredService<AutoJobService>();

        _logger.LogInformation("Running job at: {time}", DateTime.Now);

        autoJobService.CapNhatTrangThaiTreHan();
        autoJobService.Auto_SendEmail_NhacNho();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
