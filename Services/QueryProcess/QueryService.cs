using Core.Models;
using Orch.Domain.Models;
using Microsoft.Extensions.Logging;
using Orch.Infrastructure.Interfaces;
using Orch.Services.AbsenceProcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orch.Services.QueryProcess
{
    public class QueryService
    {
        private readonly ILogger<QueryService> _logger;
        private readonly IGenericQuery _repository;
        public QueryService(ILogger<QueryService> logger, IGenericQuery repository) 
        { 
            _logger = logger;
            _repository = repository;
        }

        public List<AssesmentRequestDTO> PatientClinicMrn(List<AssesmentRequestDTO> patientData)
        {
            List<AssesmentRequestDTO> returnList = new List<AssesmentRequestDTO>();
            foreach (var item in patientData)
            {
                _logger.LogInformation($"Retrieving Clinic and Mrn from patient: {item.PatientOid}");
                item.PatientDetails.Add(
                    new PatientDetails()
                    {
                        Mrn = _repository.CheckForMrn(item.PatientOid),
                        Clinic = _repository.CheckForClinic(item.PatientOid).ToString()
                    }
                );
                returnList.Add(item);
            }
            return returnList;
        }
    }
}
