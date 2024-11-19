using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Models
{
    [Keyless]
    public class AssesmentRequestDTO
    {
        [NotMapped]
        public int? PatientOid  { get; set; }
        [NotMapped]
        public int? PatientVisitOid { get; set; }
        [NotMapped]
        public int? AssessmentId { get; set; }
    }
}