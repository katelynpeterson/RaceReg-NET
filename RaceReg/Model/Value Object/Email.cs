using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Email
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();

        private bool ValidateTitleName(string title)
        {
            if (title == null || title.Equals(String.Empty))
            {
                errors[nameof(title)] = "Title must not be empty.";
                return false;
            }
            else
            {
                errors[nameof(title)] = null;
                return true;
            }
        }
    }
}
