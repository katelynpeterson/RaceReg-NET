using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.Model;
using RaceReg.Helpers;

namespace RaceReg.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

        private LoginViewModel login;
        private AboutViewModel about;
        private CreateAccountViewModel createAccount;
        private ChildView _childViewModel;
        private ChildView _previousChildViewModel;
        private RegistrationViewModel _registration;
        private CreateAffiliationViewModel _createAffiliation;
        private User currentUser;

        public ChildView PreviousChildViewModel { get => _previousChildViewModel; set => Set(ref _previousChildViewModel, value); }
        public ChildView ChildViewModel { get => _childViewModel; set => Set(ref _childViewModel, value); }
        public LoginViewModel Login { get => login; set => Set(ref login, value); }
        public AboutViewModel About { get => about; set => Set(ref about, value); }
        public CreateAccountViewModel CreateAccount { get => createAccount; set => Set(ref createAccount, value); }
        public RegistrationViewModel Registration { get => _registration; set => Set(ref _registration, value); }
        public CreateAffiliationViewModel CreateAffiliation { get => _createAffiliation; set => Set(ref _createAffiliation, value); }
        public User CurrentUser { get => currentUser; set => Set(ref currentUser, value); }

        public ObservableCollection<Affiliation> Affiliations { get; set; }
        public ObservableCollection<Participant> Participants { get; set; }

        public MainWindowViewModel()
        {
            _database = new RaceRegDatabase();
            _dialogService = new DialogService();

            Affiliations = new ObservableCollection<Affiliation>();
            Participants = new ObservableCollection<Participant>();

            Login = new LoginViewModel(this);
            About = new AboutViewModel(this);
            CreateAccount = new CreateAccountViewModel(this);
            CreateAccount.Affiliations = this.Affiliations;
            Registration = new RegistrationViewModel(this);
            CreateAffiliation = new CreateAffiliationViewModel(this);

            ChildViewModel = Login;
        }

        public MainWindowViewModel(IRaceRegDB database, IDialogService dialogService)
        {
            _database = database;
            _dialogService = dialogService;

            Affiliations = new ObservableCollection<Affiliation>();
            Participants = new ObservableCollection<Participant>();

            Login = new LoginViewModel(this, _database, _dialogService);
            About = new AboutViewModel(this);
            CreateAccount = new CreateAccountViewModel(this);
            CreateAccount.Affiliations = this.Affiliations;
            Registration = new RegistrationViewModel(this);
            CreateAffiliation = new CreateAffiliationViewModel(this);

            ChildViewModel = Login;
        }

        /** COMMON SHARED METHODS **/
        public async void QueryDatabase()
        {
            var getAffiliations = await _database.RefreshAffiliations().ConfigureAwait(true);
            Affiliations.Clear();

            foreach (Affiliation affiliation in getAffiliations)
            {
                Affiliations.Add(affiliation);
            }

            var getParticipants = await _database.RefreshParticipants().ConfigureAwait(true);
            Participants.Clear();

            foreach (Participant participant in getParticipants)
            {
                Participants.Add(participant);
            }
        }

        public void SwitchView(object viewModel)
        {
            QueryDatabase();

            PreviousChildViewModel = ChildViewModel;
            ChildViewModel = (ChildView) viewModel;
        }

        public void SwitchToPreviousView()
        {
            ChildView temp = ChildViewModel;
            ChildViewModel = PreviousChildViewModel;
            PreviousChildViewModel = temp;
        }

        /** METHODS TO CHANGE VIEWS THAT ALL CHILD VIEWS CAN HAVE ACCESS TO **/
        public void SwitchToLoginView()
        {
            SwitchView(Login);
        }

        public void SwitchToAboutView()
        {
            SwitchView(About);
        }

        public void SwitchToCreateAccountView()
        {
            SwitchView(CreateAccount);
        }

        public void SwitchToRegistrationView()
        {
            SwitchView(Registration);
        }

        public void SwitchToCreateAffiliationView()
        {
            SwitchView(CreateAffiliation);
        }
    }
}
