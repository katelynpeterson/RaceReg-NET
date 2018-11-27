using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Affiliation Affiliation { get; set; }
        public string Email { get; set; }
        public Participant Participant { get; set; }
        public string Username { get; set; }

        public User()
        {
            Id = -1;
        }

        public bool IsLocalUser()
        {
            if (Id <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
