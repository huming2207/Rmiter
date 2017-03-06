using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmiterCore.Errors
{
    public enum CasLoginError
    {
        NoError                     = 0,
        UsernameOrPasswordInvalid   = 1,
        NetworkError                = -1
    }
}
