using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Helpers
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }
}
