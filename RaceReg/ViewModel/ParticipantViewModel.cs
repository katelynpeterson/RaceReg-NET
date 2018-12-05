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
    public class ParticipantViewModel : ViewModelBase
    {
        private IRaceRegDB _database;
        private MainWindowViewModel mainWindow;

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

        public async Task SaveNewParticipantToDatabaseAsync()
        {
            var result = await _database.SaveNewParticipant(Participant);

            if(result == null)
            {
                //Save to database failed
                throw new Exception("Save to database failed! Null result");
            }
            else if(result.Id < 1)
            {
                //Save to database failed
                throw new Exception("Save to database failed! Id was not updated.");
            }
            else
            {
                //CLOSE THE VIEW
            }
        }

        private RelayCommand saveNewParticipant;
        public RelayCommand SaveNewParticipant => saveNewParticipant ?? (saveNewParticipant = new RelayCommand(
            async () =>
            {
                await SaveNewParticipantToDatabaseAsync();
            }
            ));

        public ParticipantViewModel(MainWindowViewModel mainWindowViewModel, IRaceRegDB db)
        {
            _database = db;
            mainWindow = mainWindowViewModel;

            Participant = new Participant();

            this.Affiliations = mainWindow.Affiliations;

            mainWindow.QueryDatabase();
        }
    }
}
