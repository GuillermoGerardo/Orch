using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orch.Domain.Models;

public partial class Hassessment
{
    [NotMapped]
    public int ObjectId { get; set; }

    public short InstanceHfcid { get; set; }

    public int? RecordId { get; set; }

    public string? Description { get; set; }

    public bool IsVersioned { get; set; }

    public int? CreatedUserId { get; set; }

    public DateTime? CreationTime { get; set; }

    public int AssessmentId { get; set; }

    public short? Version { get; set; }

    public DateTime? EndDt { get; set; }

    public byte AssessmentStatusCode { get; set; }

    public string AssessmentStatus { get; set; } = null!;

    public DateTime CreatedDt { get; set; }

    public DateTime EnteredDt { get; set; }

    public DateTime CollectedDt { get; set; }

    public string? ChartedFor { get; set; }

    public string? ReasonRevised { get; set; }

    public int? NoteLink { get; set; }

    public DateTime? FormDateTime { get; set; }

    public string? FormUsage { get; set; }

    public DateTime? ScheduledDt { get; set; }

    public string? UserTitle { get; set; }

    public string? UserAbbrName { get; set; }

    public int? FormTypeId { get; set; }

    public int? FormUsageId { get; set; }

    public int FormOid { get; set; }

    public int? OrderOid { get; set; }

    public int PatientOid { get; set; }

    public int? PatientVisitOid { get; set; }

    public int? HealthCareUnitOid { get; set; }

    public int? UserOid { get; set; }

    public string? FormUsageDisplayName { get; set; }

    public int? LinkHfcid { get; set; }

    public int? DocumentId { get; set; }

    public int? DocumentVersion { get; set; }

    public string? LinkContext { get; set; }

    public short? PortLevel { get; set; }

    public short? Reviewed { get; set; }

    public DateTime? LastCngDtime { get; set; }

    public string? OrdersAsWrittenText { get; set; }

    public long? LinkObjectId { get; set; }

    public long? ReccurringOrderOid { get; set; }
}
