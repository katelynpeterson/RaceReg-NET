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
            }
            else
            {
                //Close the view
            }
        }

        private RelayCommand saveNewParticipant;
        public RelayCommand SaveNewParticipant => saveNewParticipant ?? (saveNewParticipant = new RelayCommand(
            async () =>
            {
                await SaveNewParticipantToDatabaseAsync();
            }
            ));

        public ParticipantViewModel(IRaceRegDB db)
        {
            _database = db;

            Participant = new Participant();
        }
    }
}
