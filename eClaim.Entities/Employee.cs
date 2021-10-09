using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Entities
{
    [DataContract]

    public class Employee
    {
        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        public string EmpName { get; set; }

        [DataMember]
        public string BranchCode { get; set; }

        [DataMember]
        public string EmpEmail { get; set; }

        [DataMember]
        public string EmpPass { get; set; }

        [DataMember]
        public bool IsApprover { get; set; }
    }
}
