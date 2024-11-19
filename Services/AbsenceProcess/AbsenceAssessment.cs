using System.Linq;
using Microsoft.Extensions.Logging;
using Orch.Infrastructure.Interfaces;

namespace Orch.Services.AbsenceProcess;

public class AbsenceAssessment
{
    private readonly ILogger<AbsenceAssessment> _logger;
    private readonly IAbsenceAssessmentDetails _repository;

    public AbsenceAssessment(ILogger<AbsenceAssessment> logger, IAbsenceAssessmentDetails repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public string ProcessAbsenceAssessment()
    {
        _logger.LogInformation("Query Absence Assessment Records");
        var absenceDetails = _repository.GetAbsenceAssesments();
        if (absenceDetails.Any()){
        _logger.LogInformation($"Query Absence Assessment Count: {absenceDetails.Count()} records");
            foreach (var details in absenceDetails){
            _logger.LogInformation($"Loading information for Poid number: {details.Patient_oid}");

                var assessments = _repository.AbssenceAssesmentsRecords(details.Patient_oid, details.Month, details.Day, details.Year);
                if (!String.IsNullOrEmpty(assessments.Select(a => a.PatientOid).FirstOrDefault().ToString()))
                {
                    foreach (var assesment in assessments)
                    {
                        _logger.LogInformation($"Information to process (POid) (Pvisit) (AssmtID): {assesment.PatientOid}, {assesment.PatientVisitOid}, {assesment.AssessmentId}");
                    }
                }else{
                    _logger.LogInformation($"No assessment information for Poid: {details.Patient_oid}");
                    _logger.LogInformation("Skipping record...");
                }
                // var retrieve = assessments.Select(a => a).FirstOrDefault();
                // if (assessments.Any()){
                //     foreach (var assesment in assessments)
                //     {
                //         _logger.LogInformation($"checking info for: {assesment.PatientOid}");
                //     }
                //     // foreach (var item in assessments)
                //     // {
                        
                //     // }
                //     _logger.LogInformation($"check for each assesment: {assessments.Count()}");
                //     _logger.LogInformation($"check for each assesment (=>): {assessments.FirstOrDefault().AssessmentId}");
                //     _logger.LogInformation("waiting.....");
                //     Task.Delay(20000);
                    
                // }else{
                //     _logger.LogInformation($"No assessment information for Poid: {details.Patient_oid}");
                // }
            }
        }else{
            _logger.LogInformation("No assessment records to report");
        }
        int i = 0;
        foreach (var item in absenceDetails)
        {
            
            _logger.LogInformation($"Iteration value: {i}");
            _logger.LogInformation($"Poid value: {item.Patient_oid.ToString()}");
            _logger.LogInformation($"Month value: {item.Month.ToString()}");
            _logger.LogInformation($"Day value: {item.Day.ToString()}");
            _logger.LogInformation($"Year value: {item.Year.ToString()}");
            _logger.LogInformation($"Count value: {item.AssessmentCount.ToString()}");
            i++;
            
        }
        var breaker = "hi";
        return "hi";
    }
}
