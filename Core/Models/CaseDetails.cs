using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orch.Domain.Models
{
    public partial class CaseDetails
    {
        public CaseDetails() { }
        public int? caseNumber { get; set; } 
        public virtual ICollection<AssesmentRequestDTO> AssesmentRequests { get; set; }
    }
}
