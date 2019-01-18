using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model.Value_Object
{
    public class Id
    {
        private Dictionary<string, string> errors = new Dictionary<string, string>();
        private int id;

        public Id(int id)
        {
            if (ValidateId(id))
                this.id = id;
        }

        private bool ValidateId(int id)
        {
            if (id <= 0)
            {
                errors[nameof(id)] = "Id must not negative.";
                return false;
            }
            else
            {
                errors[nameof(id)] = null;
                return true;
            }
        }
    }
}
