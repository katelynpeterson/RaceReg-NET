using RaceReg.Model.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model
{
    public class User
    {
        public int Id { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Affiliation Affiliation { get; set; }
        public string Email { get; set; }
        public Participant Participant { get; set; }
        public string Username { get; set; }

        public string Name { get { return FirstName + " " + LastName; } }

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

        public static User CopyIntoUser(User userToCopyInto, User userToCopy)
        {
            userToCopyInto.Id = userToCopy.Id;
            userToCopyInto.Affiliation = userToCopy.Affiliation;
            userToCopyInto.FirstName = userToCopy.FirstName;
            userToCopyInto.LastName = userToCopy.LastName;
            userToCopyInto.Participant = userToCopy.Participant;
            userToCopyInto.Username = userToCopy.Username;
            userToCopyInto.Email = userToCopy.Email;

            return userToCopyInto;
        }
    }
}
