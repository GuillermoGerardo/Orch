using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orch.Domain.Models;

namespace Core.Models
{
    [Keyless]
    public partial class AssesmentRequestDTO
    {
        public AssesmentRequestDTO() 
        { 
            PatientDetails = new HashSet<PatientDetails>();
            CaseDetails = new HashSet<CaseDetails>();
        }
        public int? PatientOid  { get; set; }
        public int? PatientVisitOid { get; set; }
        public int? AssessmentId { get; set; }

        public ICollection<PatientDetails> PatientDetails { get; set; }
        public ICollection<CaseDetails> CaseDetails { get; set; }
    }
}