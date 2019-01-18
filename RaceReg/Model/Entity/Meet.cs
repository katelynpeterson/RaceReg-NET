using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model
{
    public class Meet : ViewModelBase
    {
        private int id;
        private string name;
        private string description;
        private DateTime startDateTime;
        private DateTime endDate;
        private int userId;

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime StartDateTime { get { return startDateTime; } set { startDateTime = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public int UserId { get { return userId; } set { userId = value; } }

        public Meet()
        {
            Id = -1;
        }
    }
}
