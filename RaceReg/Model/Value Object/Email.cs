using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Email
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private string email;

        public Email(string email)
        {
            if (ValidateEmail(email))
                this.email = email;
        }

        private bool ValidateEmail(string email)
        {
            if (email == null || email.Equals(String.Empty) || email.Any(Char.IsWhiteSpace))
            {
                errors[nameof(email)] = "Email must not be empty.";
                return false;
            }
            else
            {
                errors[nameof(email)] = null;
                return true;
            }
        }
    }
}
