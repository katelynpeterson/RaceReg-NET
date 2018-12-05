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
    public class AllParticipantViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindow;

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

        public ObservableCollection<Participant> participants;
        public ObservableCollection<Participant> Participants
        {
            get
            {
                return participants;
            }
            set
            {
                Set(ref participants, value);
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

        public AllParticipantViewModel(MainWindowViewModel mainWindowViewModel)
        {
            mainWindow = mainWindowViewModel;

            this.Affiliations = mainWindow.Affiliations;
            this.participants = mainWindow.Participants;
        }
    }
}
