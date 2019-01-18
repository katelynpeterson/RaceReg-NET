using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class TitleName
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private string title;

        public TitleName(string title)
        {
            if (ValidateTitleName(title))
                this.title = title;
        }

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
