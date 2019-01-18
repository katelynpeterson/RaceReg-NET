using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Username
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private string username;

        public Username(string username)
        {
            if (ValidateUsername(username))
                this.username = username;
        }

        private bool ValidateUsername(string username)
        {
            if (username == null || username.Equals(String.Empty))
            {
                errors[nameof(username)] = "Username must not be empty.";
                return false;
            }
            else
            {
                errors[nameof(username)] = null;
                return true;
            }
        }
    }
}
