using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orch.Domain.Models
{
    public class HpatientIdentifier
    {
        public int ObjectId { get; set; }

        public short InstanceHfcid { get; set; }

        public int? RecordId { get; set; }

        public string? Description { get; set; }

        public byte IsDeleted { get; set; }

        public bool IsVersioned { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? CreationTime { get; set; }

        public string? Type { get; set; }

        public string? Value { get; set; }

        public string? AssigningAuthorityType { get; set; }

        public string? AssigningAuthority { get; set; }

        public string? VerificationKeyType { get; set; }

        public string? VerificationKey { get; set; }

        public DateTime? ValidOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public int PatientOid { get; set; }

        public int? EntityOid { get; set; }

        public string? EntityName { get; set; }

        public DateTime? LastCngDtime { get; set; }
    }
}
