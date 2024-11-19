using Orch.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orch.Domain;
using orch.Domain.Context;
using Orch.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Core.Models;
using System.ComponentModel;
using Microsoft.SqlServer.Server;

namespace Orch.Infrastructure.Repository
{
    public class SoarianRepository : IAbsenceAssessmentDetails
    {
        private readonly SoarianContext _db;
        private readonly ILogger<SoarianRepository> _logger;

        public SoarianRepository(SoarianContext db, ILogger<SoarianRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public List<AbsenceAssesmentsDTO> GetAbsenceAssesments()
        {
            string rawQuery =
                "select a1.* from ( " +
                "select ha.Patient_oid, datepart(MONTH, ha.creationtime) as 'Month', datepart(day, ha.creationtime) as 'Day', " +
                "datepart(YEAR, ha.creationtime) as 'Year', count(ha.assessmentid) as 'AssessmentCount' " +
                "from Soarian_Clin_Prd_1.dbo.HAssessment ha with (nolock) " +
                "where ha.FormUsageDisplayName = 'Absence and Hospitalization Record' " +
                "and ha.CreationTime > '01/01/2021' AND HA.AssessmentStatusCode = 1 AND HA.CreatedUserId = 6 and ha.Version = 0 " +
                "group by ha.Patient_oid, datepart(MONTH, ha.creationtime), datepart(day, ha.creationtime), datepart(YEAR, ha.creationtime))A1 " +
                "WHERE A1.AssessmentCount > 1 ORDER BY A1.AssessmentCount DESC";
            return _db.AbsenceAssesments.FromSqlRaw(rawQuery).ToList();
        }

        public List<AssesmentRequestDTO?> AbssenceAssesmentsRecords(int Patient_oid, int Month, int Day, int Year)
        {
            var queryEl = _db.Hassessments
            .OrderByDescending(h => h.CreationTime)
            .Where(
                h => h.PatientOid == Patient_oid && 
                h.FormUsageDisplayName == "Absence and Hospitalization Record" &&
                h.CreationTime!.Value.Month == Month &&
                h.CreationTime.Value.Day == Day &&
                h.CreationTime.Value.Year == Year &&
                h.EndDt == null &&
                h.AssessmentStatusCode == 1)
            .Select(x => new AssesmentRequestDTO 
                {
                    PatientOid = x.PatientOid,
                    PatientVisitOid = x.PatientVisitOid,
                    AssessmentId = x.AssessmentId
                })
            .DefaultIfEmpty()
            .ToList();
            // string rawQuery = 
            //     $"SELECT Patient_oid, PatientVisit_oid, AssessmentID " +
	        //     $"FROM Soarian_Clin_Prd_1.DBO.HAssessment ha WITH (NOLOCK) " +
	        //     $"WHERE Patient_oid = {Patient_oid} and FormUsageDisplayName = 'Absence and Hospitalization Record' " +
	        //     $"and datepart(MONTH, ha.creationtime) = {Month} and datepart(day, ha.creationtime) = {Day} " +
	        //     $"and datepart(YEAR, ha.creationtime) = {Year} and EndDT is null and AssessmentStatusCode = 1 " +
	        //     $"ORDER BY CreationTime DESC";
                // var rawlist = _db.Hassessments.FromSqlRaw(rawQuery).ToList();
                // _logger.LogInformation($"rawList = {rawlist}");
            // return _db.AssesmentRequestDTO.FromSqlRaw(rawQuery).DefaultIfEmpty().ToList();
            return queryEl;
        }
    }
}
