using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace eClaim.Entities
{
    [DataContract]
    public class ExchangeRateReponse
    {
        [DataMember]
        public Boolean success { get; set; }

        [DataMember]
        public int timestamp { get; set; }

        [DataMember]
        public Boolean historical { get; set; }
        [DataMember]
        public string Base { get; set; }
        [DataMember]
        public DateTime date { get; set; }

        [DataMember]
        public Dictionary<string, string> rates { get; set; }
    }


}
