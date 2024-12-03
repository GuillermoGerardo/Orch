using System.Linq;
using System.IO.Pipes;
using com.staffware.sso.data;
using com.staffware.sso.server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orch.Infrastructure.Interfaces;
using System.Diagnostics;
using Orch.Services.PipeProcess;
using Orch.Domain.Models;
using static System.Net.WebRequestMethods;
using Core.Models;

namespace Orch.Services.AbsenceProcess;

public class AbsenceAssessment
{
    private readonly ILogger<AbsenceAssessment> _logger;
    private readonly IAbsenceAssessmentDetails _repository;
    private readonly IConfiguration _configuration;
    

    public AbsenceAssessment(ILogger<AbsenceAssessment> logger, IAbsenceAssessmentDetails repository, IConfiguration configuration)
    {
        _logger = logger;
        _repository = repository;
        _configuration = configuration;
    }

    public List<AssesmentRequestDTO> ProcessAbsenceAssessment(string environment, bool writeFlag)
    {
        _logger.LogInformation("Query Absence Assessment Records");
        List<AssesmentRequestDTO> returnList = new List<AssesmentRequestDTO>();
        string numberCase = "";
        var absenceDetails = _repository.GetAbsenceAssesments();
        if (absenceDetails.Any()){
        _logger.LogInformation($"Query Absence Assessment Count: {absenceDetails.Count()} records");
            foreach (var details in absenceDetails)
            {
            _logger.LogInformation($"Loading information for Poid number: {details.Patient_oid}");

                var assessments = _repository.AbssenceAssesmentsRecords(details.Patient_oid, details.Month, details.Day, details.Year);
                if (!String.IsNullOrEmpty(assessments.Select(a => a.PatientOid).FirstOrDefault().ToString()))
                {
                    foreach (var assesment in assessments)
                    { 
                        _logger.LogInformation($"Calling FWASMERR case for (POid) (Pvisit) (AssmtID): {assesment.PatientOid}, {assesment.PatientVisitOid}, {assesment.AssessmentId}");
                        if (!writeFlag)
                        {
                            numberCase = SendFWASMERRCase(
                            (int)assesment.PatientOid!, (int)assesment.PatientVisitOid!, (int)assesment.AssessmentId!,
                            _configuration.GetRequiredSection("SSO").GetSection("User").Value!, _configuration.GetRequiredSection("SSO").GetSection("Pass").Value!,
                            environment);
                        }
                        if (!String.IsNullOrEmpty(numberCase) ) 
                        {
                            //result.Add($"{assesment.PatientOid},{assesment.PatientVisitOid},{assesment.AssessmentId},{numberCase}");
                            
                            assesment.CaseDetails.Add(
                                new CaseDetails()
                                {
                                    caseNumber = int.Parse(numberCase)
                                });
                            returnList.Add(assesment);
                        }
                        else 
                        {
                            if (!writeFlag)
                            {
                                _logger.LogWarning($"There was no case number created for assessment ID: {assesment.AssessmentId}");
                            }
                            else 
                            {
                                _logger.LogWarning($"Running on Logging mode.");
                            }
                            //result.Add($"{assesment.PatientOid},{assesment.PatientVisitOid},{assesment.AssessmentId},-1");
                            //assesment.CaseDetails.FirstOrDefault()!.caseNumber = 1;
                            assesment.CaseDetails.Add( 
                                new CaseDetails() { 
                                caseNumber = 0
                            });
                            returnList.Add(assesment);
                        }
                    }
                }
                else{
                    _logger.LogInformation($"No assessment information for Poid: {details.Patient_oid}");
                    _logger.LogInformation("Skipping record...");
                }
            }
        }else{
            _logger.LogInformation("No assessment records to report");
        }
        _logger.LogInformation("Absence Assessment process finished.");
        return returnList;
    }

    private string SendFWASMERRCase(int PatientOid, int PatientVisitOid, int AssessmentId, string user, string pass, string environment)
    {
        _logger.LogInformation("Entering FWASMERRCASE...");
        var caseNumber = 
            PipeSelector.RunPipe("FWASMERRCASE", $"{user}, {pass}, WTP264AbsenceAssessment, {environment}, {PatientOid}, {PatientVisitOid}, {AssessmentId}");
        return caseNumber;
    }
}
