using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Model
{
    class Constants
    {
        private const string DATABASE_NAME = "raceregdb";
        private const string USERNAME = "csharpapp";
        private const string PASSWORD = "csharpapp";
        private const string SERVER = "db.racereg.run";
        public const string CONNECTION_STRING = "Server=" + SERVER + "; database=" + DATABASE_NAME + "; UID=" + USERNAME + "; password=" + PASSWORD;

        public const string PARTICIPANT = "Participant";
        public const string AFFILIATION = "Affiliation";
        public const string EVENT_ENTRY = "EventEntry";
        public const string HEAT = "Heat";
        public const string ROUND = "Round";
        public const string MEET_EVENT = "MeetEvent";
        public const string EVENT = "Event";
        public const string MEET = "Meet";

    }
}
