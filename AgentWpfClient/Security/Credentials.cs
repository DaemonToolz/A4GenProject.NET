using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentWpfClient.Security{
    /// <summary>
    /// 
    /// </summary>
    public class Credentials {
        public String DefaultUserName { get; set; }
        public String DefaultPasswordProtected { get; set; }
        public String GeneratedToken { get; set; }
    }
}
