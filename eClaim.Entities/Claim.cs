using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Entities
{
    [DataContract]
    public class Claim
    {

      [DataMember]
        public int ClaimID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
        [DataMember]
        public Status Status { get; set; }
        [DataMember]
        public string BankCode { get; set; }
        [DataMember]
        public string BankAccNo{ get; set; }
        [DataMember]
        public string BankAccName { get; set; }
        [DataMember]
        public int ApproverID { get; set; }
        [DataMember]
        public DateTime  DateApproved { get; set; }
        [DataMember]
        public Decimal TotalAmount { get; set; }
        [DataMember]
        public string EmpName { get; set; }
        [DataMember]
        public DateTime DateSubmitted { get; set; }
        [DataMember]
        public List<ClaimDetail> ClaimDetails { get; set; }
    }

    public enum Status
    {
        Draft = 0,
        Submitted = 1, 
        Approved = 2, 
        Rejected = 3
    }
}
