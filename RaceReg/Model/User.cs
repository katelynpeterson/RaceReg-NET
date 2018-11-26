using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Affiliation Affiliation { get; set; }

        public User()
        {
            
        }
    }
}
