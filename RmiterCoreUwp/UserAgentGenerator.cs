using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RmiterCoreUwp
{
    internal class UserAgentGenerator
    {
        internal static string GenerateUserAgent(string version = "1.0")
        {
            string userAgent = string.Format("RmiterCoreUwp/{0}", version);
            Debug.WriteLine(userAgent);
            return userAgent;
        }
    }
}
