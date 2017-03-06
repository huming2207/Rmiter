using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using RmiterCore.Errors;

namespace RmiterCore
{
    public class CasLoginResult
    {
        public CookieContainer CasCookieContainer { get; set; }
        public CasLoginError CasError { get; set; }
        public HttpStatusCode HttpResponseStatusCode { get; set; }
    }
}
