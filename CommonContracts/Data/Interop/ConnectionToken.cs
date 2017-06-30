using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Data.Interop
{
    /// <summary>
    /// Summary description for Token
    /// </summary>
    [DataContract]
    public class ConnectionToken{
        [DataMember(IsRequired = false)]
        public Boolean Status { get; set; }

        [DataMember(IsRequired = false)]
        public String Infos { get; set; }

        [DataMember(IsRequired = false)]
        public Object[] Data { get; set; }

        [DataMember(IsRequired = true)]
        public String Operation { get; set; }

        [DataMember(IsRequired = true)]
        public String AppToken { get; set; }

        [DataMember(IsRequired = false)]
        public String UserToken { get; set; }
    }
}