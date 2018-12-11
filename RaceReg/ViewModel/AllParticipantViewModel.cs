using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.ViewModel
{
    public class AllParticipantViewModel : ChildControl
    {
        private MainWindowViewModel mainWindow;
        private RegistrationViewModel registrationView;

        public Participant selectedParticipant;
        public Participant SelectedParticipant
        {
            get
            {
                return selectedParticipant;
            }
            set
            {
                Set(ref selectedParticipant, value);
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

        public AllParticipantViewModel(string header, MainWindowViewModel mainWindowViewModel, RegistrationViewModel registrationView) : base(header)
        {
            mainWindow = mainWindowViewModel;
            this.registrationView = registrationView;

            this.Affiliations = mainWindow.Affiliations;
            this.participants = mainWindow.Participants;
        }

        private RelayCommand editParticipant;
        public RelayCommand EditParticipant => editParticipant ?? (editParticipant = new RelayCommand(
            () =>
            {
                registrationView.CloseTab.Execute(null);
                registrationView.EditParticipant(SelectedParticipant);
            }
            ));
    }
}
