using GalaSoft.MvvmLight;
using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.ViewModel
{
    public class ParticipantViewModel : ViewModelBase
    {
        private Participant participant;
        public Participant Participant
        {
            get
            {
                return participant;
            }
            set
            {
                Set(ref participant, value);
            }
        }

        public ObservableCollection<Affiliation> affiliations;
        public ObservableCollection<Affiliation> Affiliations
        {
            get
            {
                return affiliations;
            }
            set
            {
                Set(ref affiliations, value);
            }
        }

        public enum GenderType { Male, Female, Other };
        public IEnumerable<GenderType> GenderTypes
        {
            get
            {
                return Enum.GetValues(typeof(GenderType)).Cast<GenderType>().ToList<GenderType>();
            }
        }

        public ParticipantViewModel()
        {
            Participant = new Participant();
        }
    }
}
