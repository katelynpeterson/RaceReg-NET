using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Model
{
    public class Affiliation
    {
        //private int _id;
        //private string _name;
        //private string _abbreviation;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Affiliation;

            if (item == null)
            {
                return false;
            }

            bool areEqual = true;

            if (this.Id != item.Id ||
                !this.Name.Equals(item.Name) ||
                !this.Abbreviation.Equals(item.Abbreviation))
            {
                areEqual = false;
            }

            return areEqual;
        }
    }
}