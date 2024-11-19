using Orch.Services.AbsenceProcess;

namespace Orch.App;

public class AbsWorker : BackgroundService
{
    private readonly ILogger<AbsWorker> _logger;
    private readonly AbsenceAssessment _absenceAssessment;
    IHostApplicationLifetime _applicationLifetime;

    public AbsWorker(ILogger<AbsWorker> logger, IHostApplicationLifetime applicationLifetime, AbsenceAssessment absenceAssessment)
    {
        _logger = logger;
        _absenceAssessment = absenceAssessment;
        _applicationLifetime = applicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var arrayList = Environment.GetCommandLineArgs();
        var envIdx = Array.FindIndex( arrayList, x => x.Contains("-e") );
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                //_logger.LogInformation("Worker running at: {time} string: {value}", DateTimeOffset.Now, arrayList[envIdx+1]);
                _logger.LogInformation("Running at ({time})", DateTimeOffset.Now);
                string breaker = "breaka!";
                _absenceAssessment.ProcessAbsenceAssessment();
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
