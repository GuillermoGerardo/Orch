using Core.Models;
using orch.Services.QueryProcess;
using Orch.Services.AbsenceProcess;
using Orch.Services.ExcellProcess;

namespace Orch.App;

public class AbsWorker : BackgroundService
{
    private readonly ILogger<AbsWorker> _logger;
    private readonly AbsenceAssessment _absenceAssessment;
    private readonly QueryService _queryService;
    private readonly ExcellService _excellService;
    IHostApplicationLifetime _applicationLifetime;

    public AbsWorker(ILogger<AbsWorker> logger, IHostApplicationLifetime applicationLifetime, AbsenceAssessment absenceAssessment, QueryService queryService, ExcellService excellService)
    {
        _logger = logger;
        _absenceAssessment = absenceAssessment;
        _applicationLifetime = applicationLifetime;
        _queryService = queryService;
        _excellService = excellService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var arrayList = Environment.GetCommandLineArgs();
        string closed = "Done, Closing program.";
        string pOid = "";
        var envIdx = Array.FindIndex( arrayList, x => x.Contains("-e") );
        if (arrayList.Contains("-f") || arrayList.Contains("--filter")) 
        {
            var filterIdx = Array.FindIndex(arrayList, x => x.Contains("-f"));
            pOid = arrayList[filterIdx + 1];
            _logger.LogInformation("Filtered results with pOid: {pOid}", pOid);
        }
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Running at ({time})", DateTimeOffset.Now);
                List<AssesmentRequestDTO> result =
                    _absenceAssessment.ProcessAbsenceAssessment
                    (
                        arrayList[envIdx + 1], (arrayList.Contains("--logging") || arrayList.Contains("-l")), pOid
                    );
                if (result.Count > 0 && (arrayList.Contains("-a") || arrayList.Contains("--audit"))) 
                {
                    result = _queryService.PatientClinicMrn(result, "Absence");
                    _logger.LogInformation("Exporting processed records..");
                    _excellService.Print(result, arrayList[envIdx + 1], "Absence");
                    _logger.LogInformation("{closing}", closed);
                }
            }
            Environment.Exit(0);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
