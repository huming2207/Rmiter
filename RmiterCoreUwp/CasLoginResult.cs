using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using RmiterCoreUwp.Errors;

namespace RmiterCoreUwp
{
    public class CasLoginResult
    {
        public CookieContainer CasCookieContainer { get; set; }
        public CasLoginError CasError { get; set; }
        public HttpStatusCode HttpResponseStatusCode { get; set; }
    }
}
