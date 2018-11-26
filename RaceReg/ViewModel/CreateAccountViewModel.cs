using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Text;
using GalaSoft.MvvmLight.Command;

namespace RaceReg.ViewModel
{
    public class CreateAccountViewModel : ViewModelBase
    {
        private IRaceRegDB _database;

        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                Set(ref user, value);
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

        private RelayCommand createNewAccount;
        public RelayCommand CreateNewAccount => createNewAccount ?? (createNewAccount = new RelayCommand(
            () =>
            {
                throw new NotImplementedException();
            }
            ));


        public CreateAccountViewModel(IRaceRegDB db)
        {
            _database = db;

            User = new User();
        }
    }
}
