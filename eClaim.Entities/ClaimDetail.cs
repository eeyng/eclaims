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

    public class ClaimDetail
    {
       [DataMember]
        public int ClaimDetailID { get; set; }
        [DataMember]
        public int ClaimID { get; set; }
       [DataMember]

        public DateTime TransactionDate { get; set; }
        [DataMember]

        public string SerialNo { get; set; }
        [DataMember]

        public string CostCenter { get; set; }
        [DataMember]
        public string GLCodeID { get; set; }
        [DataMember]
        public string ClaimDetailDesc { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public byte Status { get; set; }
        [DataMember]
        public Decimal Amount { get; set; }
        [DataMember]
        public Decimal GST { get; set; }
        [DataMember]
        public Decimal ExchangeRate { get; set; }
        [DataMember]
        public Decimal TotalAmt { get; set; }
        [DataMember]
        public DateTime DateCreated { get; set; }
    }
}
