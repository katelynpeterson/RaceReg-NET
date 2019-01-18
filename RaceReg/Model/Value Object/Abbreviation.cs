using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Abbreviation
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private string abbreviation;

        public Abbreviation(string abbrev)
        {
            if (ValidateTitleName(abbrev))
                this.abbreviation = abbrev.ToUpper();
        }

        private bool ValidateTitleName(string abbrev)
        {
            if (abbrev == null || abbrev.Equals(String.Empty))
            {
                errors[nameof(abbrev)] = "Abbreviation must not be empty.";
                return false;
            }
            else
            {
                errors[nameof(abbrev)] = null;
                return true;
            }
        }
    }
}
