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

        public async void RefreshAffiliationsAsync()
        {
            var getAffiliations = await _database.RefreshAffiliations().ConfigureAwait(true);
            Affiliations.Clear();

            foreach (Affiliation affiliation in getAffiliations)
            {
                Affiliations.Add(affiliation);
            }
            Affiliations = new ObservableCollection<Affiliation>(getAffiliations);
        }

        private RelayCommand refreshAffiliations;
        public RelayCommand RefreshAffiliations => refreshAffiliations ?? (refreshAffiliations = new RelayCommand(
            () =>
            {
                RefreshAffiliationsAsync();
            }
            ));

        public CreateAccountViewModel(IRaceRegDB db)
        {
            Affiliations = new ObservableCollection<Affiliation>();
            User = new User();

            _database = db;

            RefreshAffiliationsAsync();
        }

        //Default constructor
        public CreateAccountViewModel() : this(new Database()) { }
    }
}
