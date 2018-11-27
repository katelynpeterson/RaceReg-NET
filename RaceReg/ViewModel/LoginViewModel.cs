using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using RaceReg.Helpers;
using System.Collections.Generic;
using System.Text;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class LoginViewModel : ChildView
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

        private string _loginMessage;
        public String LoginMessage
        {
            get
            {
                return _loginMessage;
            }
            set
            {
                Set(ref _loginMessage, value);
            }
        }

        private string _username;
        public String Username
        {
            get
            {
                return _username;
            }
            set
            {
                Set(ref _username, value);
            }
        }

        public LoginViewModel(MainWindowViewModel mainWindowViewModel, IRaceRegDB RaceRegDB, IDialogService dialogService) : base(mainWindowViewModel)
        {
            _database = RaceRegDB;
            _dialogService = dialogService;

            LoginMessage = "";
        }

        //Default constructor
        public LoginViewModel(MainWindowViewModel mainWindowViewModel) : this(mainWindowViewModel, new Database(), new DialogService()) { }

        private PasswordRelayCommand loginCommand;
        public PasswordRelayCommand LoginCommand => loginCommand ?? (loginCommand = new PasswordRelayCommand(LoginAsync));

        private RelayCommand createAccountCommand;
        public RelayCommand CreateAccountCommand => createAccountCommand ?? (createAccountCommand = new RelayCommand(
            () =>
            {
                mainWindow.SwitchToCreateAccountView();
            }
            ));

        private RelayCommand aboutMenuCommand;
        public RelayCommand AboutMenuCommand => aboutMenuCommand ?? (aboutMenuCommand = new RelayCommand(
            () =>
            {
                mainWindow.SwitchToAboutView();
            }
            ));
        

        private async void LoginAsync(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if(passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                //Do stuff with the password - not currently implemented.
            }

            //Get user details (does not support password)
            mainWindow.CurrentUser = await _database.GrabUserDetailsAsync(Username, null);

            if(mainWindow.CurrentUser.Id <= 0)
            {
                LoginMessage = "No username exists. Please Create An Account.";
            }
            else
            {
                //Change to RegistrationView after login
                mainWindow.SwitchToRegistrationView();
            }
        }
    }
}
