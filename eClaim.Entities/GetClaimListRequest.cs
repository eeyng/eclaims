using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Entities
{
    [DataContract]
    public partial class GetClaimListRequest
    {
        [DataMember]
        public Employee Employee { get; set; }
        [DataMember]
        public string SearchCondition { get; set; }
    }
}
