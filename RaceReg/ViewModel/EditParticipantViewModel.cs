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
    public class EditParticipantViewModel : ChildControl
    {
        private IRaceRegDB _database;
        private MainWindowViewModel mainWindow;
        private RegistrationViewModel registrationView;

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

        private Affiliation affiliation;
        public Affiliation Affiliation
        {
            get
            {
                return affiliation;
            }
            set
            {
                Set(ref affiliation, value);
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

        public async Task UpdateParticipantAsync()
        {
            var result = await _database.UpdateParticipantAsync(Participant);

            if(result == null)
            {
                registrationView.Message = "Save to database failed! Null result";
                //throw new Exception("Save to database failed! Null result");
            }
            else if(result.Id < 1)
            {
                registrationView.Message = "Save to database failed! No rows affected.";
                //throw new Exception("Save to database failed! Id was not updated.");
            }
            else
            {
                registrationView.Message = "Succesfully Edited Participant";
                mainWindow.QueryDatabase();
                registrationView.CloseTab.Execute(null);
            }
        }

        private RelayCommand saveParticipant;
        public RelayCommand SaveParticipant => saveParticipant ?? (saveParticipant = new RelayCommand(
            async () =>
            {
                await UpdateParticipantAsync();
            }
            ));

        public EditParticipantViewModel(string header, MainWindowViewModel mainWindowViewModel, RegistrationViewModel registrationView, IRaceRegDB db, Participant selectedParticipant) : base(header)
        {
            _database = db;
            mainWindow = mainWindowViewModel;
            this.registrationView = registrationView;

            this.Affiliations = mainWindow.Affiliations;
            this.Affiliation = mainWindow.CurrentUser.Affiliation;
            this.Participant = selectedParticipant;
        }
    }
}
