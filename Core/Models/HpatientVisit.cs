using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orch.Domain.Models
{
    public class HpatientVisit
    {
        public int ObjectId { get; set; }

        public short InstanceHfcid { get; set; }

        public int? RecordId { get; set; }

        public string? Description { get; set; }

        public byte IsDeleted { get; set; }

        public bool IsVersioned { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? CreationTime { get; set; }

        public string? VisitId { get; set; }

        public string? AlternateVisitId { get; set; }

        public string? PatientAccountId { get; set; }

        public bool? Vipindicator { get; set; }

        public DateTime VisitStartDateTime { get; set; }

        public DateTime? VisitEndDateTime { get; set; }

        public byte? VisitStatus { get; set; }

        public string? VisitTypeCode { get; set; }

        public string? Debtor { get; set; }

        public string PatientCategory1 { get; set; } = null!;

        public string? PatientLocationName { get; set; }

        public string? Remarks { get; set; }

        public string VisitCategory { get; set; } = null!;

        public string? PrivacyText { get; set; }

        public string? PatientReasonForSeekingHc { get; set; }

        public string? PaymentMode { get; set; }

        public string? Financialclass { get; set; }

        public byte? ProviderGenderPreference { get; set; }

        public short? ExpectedLengthofStay { get; set; }

        public short? ActualLengthofStay { get; set; }

        public string? CorrectedBy { get; set; }

        public string? Comments { get; set; }

        public string? LatestBedName { get; set; }

        public int? DeltaValue { get; set; }

        public string? UnitContactedName { get; set; }

        public string? VisitLocation { get; set; }

        public string? ReasonForVisit { get; set; }

        public string? DischargeDisposition { get; set; }

        public string? IsolationIndicator { get; set; }

        public string? PatientCondition { get; set; }

        public string? EntityName { get; set; }

        public string? EntityAbb { get; set; }

        public string? AccommodationType { get; set; }

        public int? DischargeFormId { get; set; }

        public int PatientOid { get; set; }

        public int? UnitContactedOid { get; set; }

        public int? ReferringInstitutionOid { get; set; }

        public int? PatientLocationOid { get; set; }

        public int PatientClassOid { get; set; }

        public int? EntityOid { get; set; }

        public int? PatientVisitExtensionOid { get; set; }

        public string? Confidentiality { get; set; }

        public string? ExternalMidWife { get; set; }

        public int? TypeCode { get; set; }

        public string? DischargeTypeCode { get; set; }

        public double? Weight { get; set; }

        public double? VentilatorTime { get; set; }

        public bool? Complication { get; set; }

        public string? AdmissionType { get; set; }

        public string? ReasonforDischarge { get; set; }

        public int? StartingVisitOid { get; set; }

        public string? PublicityIndicator { get; set; }

        public string? PatientStatusCode { get; set; }

        public byte? CriticalCare { get; set; }

        public string? ClinicCode { get; set; }

        public string? Dnrvalue { get; set; }

        public string? DischargeTo { get; set; }

        public DateTime? LastCngDtime { get; set; }

        public string? AdmitReasonCode { get; set; }

        public DateTime? PreVisitDate { get; set; }

        public string? LevelOfCare { get; set; }

        public string? ReadmissionIndicator { get; set; }

        public string? RtlstagId { get; set; }

        public string? AdmitSource { get; set; }

        public string? ReferralSource { get; set; }

        public DateTime? PrevDschDate { get; set; }

        public DateTime? ExpAdmDtime { get; set; }

        public DateTime? ExpDschDtime { get; set; }

        public string? PreArrivalTrackingNo { get; set; }

        public string? RegistrationStatus { get; set; }

        public string? PreArrivalMergeStatus { get; set; }

        public string? ModeOfArrival { get; set; }

        public string? TransportAgency { get; set; }

        public short? IsNotInterfaceable { get; set; }

        public string? SpecialNeeds { get; set; }

        public string? RestrictionIndicator { get; set; }

        public byte? IsAmbulatory { get; set; }

        public int? ClinicOrgOid { get; set; }

        public string? ClinicOrgName { get; set; }

        public string? PresentingVisitType { get; set; }

        public DateTime? PresentingDateTime { get; set; }

        public string? ChiefComplaintCode { get; set; }

        public string? ChiefComplaintCodingSystem { get; set; }

        public bool? IsOnLeaveOfAbsence { get; set; }

        public string? AdmitReasonText { get; set; }

        public string? AdmitReasonCodingSystem { get; set; }

        public string? Hieconsent { get; set; }

    }
}
