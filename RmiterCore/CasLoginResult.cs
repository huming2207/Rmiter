using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using RmiterCore.Errors;

namespace RmiterCore
{
    /// <summary>
    /// CAS login result, will be returned after login.
    /// </summary>
    public class CasLoginResult
    {
        /// <summary>
        /// Cookie container, contains some cookies returned from CAS server
        /// </summary>
        public CookieContainer CasCookieContainer { get; set; }

        /// <summary>
        /// A brief CAS login error
        /// </summary>
        public CasLoginError CasError { get; set; }

        /// <summary>
        /// Detailed status code, helps user to solve connectivity issues
        /// </summary>
        public HttpStatusCode HttpResponseStatusCode { get; set; }
        
        /// <summary>
        /// Student/Staff ID, only exists if login success, otherwise remains null.
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Account password, only exists if login success, otherwise remains null.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// (Unused so far) Unified user agent
        /// </summary>
        public string UserAgent { get; set; }
    }
}
