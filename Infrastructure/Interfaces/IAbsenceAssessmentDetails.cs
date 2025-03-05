using Core.Models;
using Orch.Domain.Models;

namespace Orch.Infrastructure.Interfaces;

public interface IAbsenceAssessmentDetails
{
    List<AbsenceAssesmentsDTO> GetAbsenceAssesments(string[] pOid);
    List<AssesmentRequestDTO> AbssenceAssesmentsRecords(int Patient_oid, int Month, int Day, int Year);
}
