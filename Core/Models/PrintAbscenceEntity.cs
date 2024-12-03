using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace orch.Domain.Models
{
    public class PrintAbscenceEntity
    {
        public string PatientOid { get; set; }
        public string PatientVisitOid { get; set; }
        public string AssessmentId { get; set; }
        public string CaseNumber { get; set; }
        public string Mrn { get; set; }
        public string Clinic { get; set; }
        
    }
}
