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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
