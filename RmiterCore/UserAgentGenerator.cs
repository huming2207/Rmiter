﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RmiterCore
{
    internal class UserAgentGenerator
    {
        internal static string GenerateUserAgent(string version = "1.0")
        {
            string userAgent = string.Format("RmiterCore/{0} (OS: {1}; CLR: {2})", version, Environment.OSVersion.ToString(), Environment.Version.ToString());
            Debug.WriteLine(userAgent);
            return userAgent;
        }
    }
}
