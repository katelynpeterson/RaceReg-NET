using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Name
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private string name;

        public Name(string name)
        {
            if (ValidateName(name))
                this.name = name;
        }

        private bool ValidateName(string name)
        {
            if (name == null || name.Equals(String.Empty) || name.Any(Char.IsWhiteSpace))
            {
                errors[nameof(name)] = "Name must contain no spaces, and cannot be empty.";
                return false;
            }
            else
            {
                errors[nameof(name)] = null;
                return true;
            }
        }
    }
}
