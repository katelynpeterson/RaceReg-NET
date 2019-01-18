using GalaSoft.MvvmLight;
using RaceReg.Model.Value_Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.Model
{
    public class Meet : ViewModelBase
    {
        private int id;
        private TitleName meetName;
        private string description;
        private DateTime startDateTime;
        private DateTime endDate;
        private int userId;
        private Affiliation hostAffiliation;

        public int Id { get { return id; } set { id = value; } }
        public TitleName MeetName { get { return meetName; } set { meetName = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime StartDateTime { get { return startDateTime; } set { startDateTime = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public int UserId { get { return userId; } set { userId = value; } }
        public Affiliation HostAffiliation { get { return hostAffiliation; } set { hostAffiliation = value; } }

        public Meet()
        {
            Id = -1;
        }
    }
}
