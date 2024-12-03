using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orch.Domain.Models;
using Core.Models;

namespace Orch.Domain.Models
{
    [Keyless]
    public partial class PatientDetails
    {
        public PatientDetails() { }
        public string Mrn { get; set; }
        public string Clinic {  get; set; }


        public virtual ICollection<AssesmentRequestDTO> AssesmentRequests { get; set; }
    }
}
