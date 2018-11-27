using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Text;
using GalaSoft.MvvmLight.Command;
using RaceReg.Helpers;

namespace RaceReg.ViewModel
{
    public class CreateAccountViewModel : ChildView
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

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

        private RelayCommand backToLoginCommand;
        public RelayCommand BackToLoginCommand => backToLoginCommand ?? (backToLoginCommand = new RelayCommand(
            () =>
            {
                mainWindow.SwitchToLoginView();
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
        private readonly MainWindowViewModel mainWindowViewModel;

        public RelayCommand RefreshAffiliations => refreshAffiliations ?? (refreshAffiliations = new RelayCommand(
            () =>
            {
                RefreshAffiliationsAsync();
            }
            ));

        public CreateAccountViewModel(MainWindowViewModel mainWindowViewModel, IRaceRegDB db, IDialogService dialogService) : base(mainWindowViewModel)
        {
            Affiliations = new ObservableCollection<Affiliation>();
            User = new User();

            _database = db;
            _dialogService = dialogService;

            RefreshAffiliationsAsync();
        }

        //Default constructor
        public CreateAccountViewModel(MainWindowViewModel mainWindowViewModel) : this(mainWindowViewModel, new Database(), new DialogService()) { }
    }
}
