using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentWpfClient.Security{
    /// <summary>
    /// 
    /// </summary>
    public class OverallConfigs{
        public bool UseAppSettings { get; set; }
        public bool UseAppSecuritySettings { get; set; }
        public bool AutogenerateToken { get; set; }

        public String DefaultSearchPath { get; set; }
        public String DefaultEmail { get; set; }
    }
}
