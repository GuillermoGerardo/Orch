using System;
using System.Collections.Generic;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Orch.Domain.Models;

namespace orch.Domain.Context;

public partial class SoarianContext : DbContext
{
    public SoarianContext()
    {
    }

    public SoarianContext(DbContextOptions<SoarianContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hassessment> Hassessments { get; set; }
    public virtual DbSet<AbsenceAssesmentsDTO> AbsenceAssesments { get; set; }
    public virtual DbSet<AssesmentRequestDTO> AssesmentRequestDTO { get; set; }
    public virtual DbSet<HpatientIdentifier> HpatientIdentifiers { get; set; }
    public virtual DbSet<HpatientVisit> HpatientVisits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Hassessment>(entity =>
        {
            entity.HasKey(e => e.ObjectId)
                .HasName("PKCL_HAssessment")
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("HAssessment", tb =>
                {
                    tb.HasTrigger("HAssessment_updateModality_trg");
                    tb.HasTrigger("HAssessment_update_trg");
                });

            entity.HasIndex(e => new { e.PatientOid, e.CollectedDt }, "HAssessment_ClusteredIndex")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.EnteredDt, e.FormUsage }, "HAssessment_EnteredDt_NIDX").HasFillFactor(90);

            entity.HasIndex(e => new { e.LinkObjectId, e.LinkHfcid }, "HAssessment_LinkObj_HFCID_NIDX").HasFillFactor(90);

            entity.HasIndex(e => new { e.AssessmentId, e.EndDt }, "HAssessment_NonClusteredAssmtID").HasFillFactor(90);

            entity.HasIndex(e => new { e.OrderOid, e.ReccurringOrderOid }, "HAssessment_OrdReccurr_NIDX").HasFillFactor(90);

            entity.HasIndex(e => e.LastCngDtime, "LastCngDtime_Nidx").HasFillFactor(90);

            entity.Property(e => e.ObjectId)
                .ValueGeneratedNever()
                .HasColumnName("ObjectID");
            entity.Property(e => e.AssessmentId).HasColumnName("AssessmentID");
            entity.Property(e => e.AssessmentStatus)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.ChartedFor)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.CollectedDt)
                .HasColumnType("smalldatetime")
                .HasColumnName("CollectedDT");
            entity.Property(e => e.CreatedDt)
                .HasColumnType("smalldatetime")
                .HasColumnName("CreatedDT");
            entity.Property(e => e.CreationTime).HasColumnType("smalldatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.EndDt)
                .HasColumnType("smalldatetime")
                .HasColumnName("EndDT");
            entity.Property(e => e.EnteredDt)
                .HasColumnType("smalldatetime")
                .HasColumnName("EnteredDT");
            entity.Property(e => e.FormDateTime).HasColumnType("smalldatetime");
            entity.Property(e => e.FormOid).HasColumnName("Form_oid");
            entity.Property(e => e.FormTypeId).HasColumnName("FormTypeID");
            entity.Property(e => e.FormUsage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FormUsageDisplayName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FormUsageId).HasColumnName("FormUsageID");
            entity.Property(e => e.HealthCareUnitOid).HasColumnName("HealthCareUnit_oid");
            entity.Property(e => e.InstanceHfcid).HasColumnName("InstanceHFCID");
            entity.Property(e => e.LastCngDtime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LinkContext)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LinkHfcid).HasColumnName("LinkHFCID");
            entity.Property(e => e.LinkObjectId).HasColumnName("LinkObjectID");
            entity.Property(e => e.OrderOid).HasColumnName("Order_oid");
            entity.Property(e => e.OrdersAsWrittenText)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PatientOid).HasColumnName("Patient_oid");
            entity.Property(e => e.PatientVisitOid).HasColumnName("PatientVisit_oid");
            entity.Property(e => e.PortLevel).HasDefaultValue((short)0);
            entity.Property(e => e.ReasonRevised)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReccurringOrderOid).HasColumnName("ReccurringOrder_oid");
            entity.Property(e => e.ScheduledDt)
                .HasColumnType("smalldatetime")
                .HasColumnName("ScheduledDT");
            entity.Property(e => e.UserAbbrName)
                .HasMaxLength(92)
                .IsUnicode(false);
            entity.Property(e => e.UserOid).HasColumnName("User_oid");
            entity.Property(e => e.UserTitle)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HpatientIdentifier>(entity =>
        {
            entity.HasKey(e => e.ObjectId)
                .HasName("PKCL_HPatientIdentifiers")
                .IsClustered(false);

            entity.ToTable("HPatientIdentifiers", tb => tb.HasTrigger("HPatientIdentifiers_update_trg"));

            entity.HasIndex(e => e.LastCngDtime, "LastCngDtime_Nidx").HasFillFactor(90);

            entity.HasIndex(e => e.PatientOid, "Patient_OidCIdx")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => e.Value, "Type_EntityValueNidx").HasFillFactor(90);

            entity.HasIndex(e => new { e.Type, e.EntityOid }, "idx_HPatientIdentifiers_Type_EntityOID").HasFillFactor(90);

            entity.HasIndex(e => new { e.Value, e.AssigningAuthority, e.IsDeleted, e.Type }, "idx_HPatientIdentifiers_Value_AssigningAuthority_IsDeleted_Type").HasFillFactor(90);

            entity.Property(e => e.ObjectId)
                .ValueGeneratedNever()
                .HasColumnName("ObjectID");
            entity.Property(e => e.AssigningAuthority)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AssigningAuthorityType)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.CreationTime).HasColumnType("smalldatetime");
            entity.Property(e => e.Description)
                .HasMaxLength(184)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.EntityOid).HasColumnName("EntityOID");
            entity.Property(e => e.ExpiresOn).HasColumnType("datetime");
            entity.Property(e => e.InstanceHfcid).HasColumnName("InstanceHFCID");
            entity.Property(e => e.LastCngDtime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PatientOid).HasColumnName("Patient_oid");
            entity.Property(e => e.Type)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("TYpe");
            entity.Property(e => e.ValidOn).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VerificationKey)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VerificationKeyType)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HpatientVisit>(entity =>
        {
            entity.HasKey(e => e.ObjectId)
                .HasName("PKCL_HPatientVisit")
                .IsClustered(false);

            entity.ToTable("HPatientVisit", tb => tb.HasTrigger("HPatientVisit_update_trg"));

            entity.HasIndex(e => new { e.PatientAccountId, e.AlternateVisitId }, "HPV_PatientAccountID_NIDX").HasFillFactor(90);

            entity.HasIndex(e => new { e.VisitStatus, e.PatientLocationOid, e.ObjectId }, "HPV_PatientLocation_NIDX").IsUnique();

            entity.HasIndex(e => e.LastCngDtime, "LastCngDtime_Nidx").HasFillFactor(90);

            entity.HasIndex(e => new { e.PatientLocationName, e.LatestBedName, e.EntityOid, e.VisitStatus }, "Location_NIDX").HasFillFactor(90);

            entity.HasIndex(e => e.PatientClassOid, "PatientClass_oidNidx").HasFillFactor(90);

            entity.HasIndex(e => e.PatientOid, "Patient_oidCldx")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => e.PreArrivalTrackingNo, "PreArrivalTrackingNo_Nidx").HasFillFactor(90);

            entity.HasIndex(e => e.StartingVisitOid, "StartingVisitOIDNidx").HasFillFactor(90);

            entity.HasIndex(e => new { e.Vipindicator, e.VisitStatus }, "VIPVistitStat_NIDX").HasFillFactor(90);

            entity.HasIndex(e => new { e.VisitEndDateTime, e.EntityOid, e.VisitTypeCode, e.VisitStatus }, "VisitEndDateTimeNidx")
                .IsDescending(true, false, false, false)
                .HasFillFactor(90);

            entity.HasIndex(e => e.VisitStartDateTime, "VisitStartDateTimeNidx").HasFillFactor(90);

            entity.Property(e => e.ObjectId)
                .ValueGeneratedNever()
                .HasColumnName("ObjectID");
            entity.Property(e => e.AccommodationType)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.AdmissionType)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.AdmitReasonCode)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.AdmitReasonCodingSystem)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AdmitReasonText)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AdmitSource)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.AlternateVisitId)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("AlternateVisitID");
            entity.Property(e => e.ChiefComplaintCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ChiefComplaintCodingSystem)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClinicCode)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ClinicOrgName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ClinicOrgOid).HasColumnName("ClinicOrg_oid");
            entity.Property(e => e.Comments)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Confidentiality)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.CorrectedBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreationTime).HasColumnType("smalldatetime");
            entity.Property(e => e.CriticalCare).HasDefaultValueSql("(0)");
            entity.Property(e => e.Debtor)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.DischargeDisposition)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.DischargeFormId).HasColumnName("DischargeFormID");
            entity.Property(e => e.DischargeTo)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.DischargeTypeCode)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Dnrvalue)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("DNRValue");
            entity.Property(e => e.EntityAbb)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.EntityOid).HasColumnName("Entity_oid");
            entity.Property(e => e.ExpAdmDtime)
                .HasColumnType("smalldatetime")
                .HasColumnName("ExpAdmDTime");
            entity.Property(e => e.ExpDschDtime)
                .HasColumnType("smalldatetime")
                .HasColumnName("ExpDschDTime");
            entity.Property(e => e.ExternalMidWife)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Financialclass)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.Hieconsent)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("HIEConsent");
            entity.Property(e => e.InstanceHfcid).HasColumnName("InstanceHFCID");
            entity.Property(e => e.IsAmbulatory).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsolationIndicator)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.LastCngDtime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LatestBedName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.LevelOfCare)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ModeOfArrival)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.PatientAccountId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PatientAccountID");
            entity.Property(e => e.PatientCategory1)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.PatientClassOid).HasColumnName("PatientClass_oid");
            entity.Property(e => e.PatientCondition)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.PatientLocationName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.PatientLocationOid).HasColumnName("PatientLocation_oid");
            entity.Property(e => e.PatientOid).HasColumnName("Patient_oid");
            entity.Property(e => e.PatientReasonForSeekingHc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PatientReasonForSeekingHC");
            entity.Property(e => e.PatientStatusCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitExtensionOid).HasColumnName("PatientVisitExtension_oid");
            entity.Property(e => e.PaymentMode)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.PreArrivalMergeStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PreArrivalTrackingNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PreVisitDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PresentingDateTime).HasColumnType("datetime");
            entity.Property(e => e.PresentingVisitType)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrevDschDate).HasColumnType("smalldatetime");
            entity.Property(e => e.PrivacyText)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PublicityIndicator)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ReadmissionIndicator)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReasonForVisit)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ReasonforDischarge)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ReferralSource)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.ReferringInstitutionOid).HasColumnName("ReferringInstitution_oid");
            entity.Property(e => e.RegistrationStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RestrictionIndicator)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.RtlstagId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RTLSTagId");
            entity.Property(e => e.SpecialNeeds)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.StartingVisitOid).HasColumnName("StartingVisitOID");
            entity.Property(e => e.TransportAgency)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.UnitContactedName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.UnitContactedOid).HasColumnName("UnitContacted_oid");
            entity.Property(e => e.Vipindicator)
                .HasDefaultValueSql("(0)")
                .HasColumnName("VIPIndicator");
            entity.Property(e => e.VisitCategory)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.VisitEndDateTime).HasColumnType("datetime");
            entity.Property(e => e.VisitId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("VisitID");
            entity.Property(e => e.VisitLocation)
                .HasMaxLength(64)
                .IsUnicode(false);
            entity.Property(e => e.VisitStartDateTime).HasColumnType("datetime");
            entity.Property(e => e.VisitTypeCode)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
