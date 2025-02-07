using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orch.Infrastructure.Interfaces
{
    public interface IGenericQuery
    {
        string CheckForMrn(int? patientOid);
        string CheckForClinic(int? patientOid, string type);
    }
}
