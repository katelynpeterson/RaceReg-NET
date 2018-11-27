using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using RaceReg.Helpers;
using System.Collections.Generic;
using System.Text;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel mainWindow;
        private IRaceRegDB _database;
        private IDialogService _dialogService;

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

        public LoginViewModel(IRaceRegDB RaceRegDB,
            IDialogService dialogService)
        {
            _database = RaceRegDB;
            _dialogService = dialogService;
        }

        //Default constructor
        public LoginViewModel(MainWindowViewModel mainWindowViewModel) : this(new Database(), new DialogService())
        {
            this.mainWindow = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
        }

        private PasswordRelayCommand loginCommand;
        public PasswordRelayCommand LoginCommand => loginCommand ?? (loginCommand = new PasswordRelayCommand(LoginAsync));

        private RelayCommand exitCommand;
        public RelayCommand ExitCommand => exitCommand ?? (exitCommand = new RelayCommand(
            () =>
            {
                Console.WriteLine("Program is closing now.");

                //Clean up program here!!

                Environment.Exit(0);
            }
            ));

        private RelayCommand aboutCommand;
        public RelayCommand AboutCommand => aboutCommand ?? (aboutCommand = new RelayCommand(
            () =>
            {
                throw new NotImplementedException();
            }
            ));

        private RelayCommand createAccountCommand;
        public RelayCommand CreateAccountCommand => createAccountCommand ?? (createAccountCommand = new RelayCommand(
            () =>
            {
                //Change to Create Account View view
                mainWindow.ChildViewModel = mainWindow.CreateAccount;
            }
            ));

        private RelayCommand aboutMenuCommand;
        public RelayCommand AboutMenuCommand => aboutMenuCommand ?? (aboutMenuCommand = new RelayCommand(
            () =>
            {
                //Change to Create Account View view
                mainWindow.ChildViewModel = mainWindow.About;
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
                throw new Exception("USER HAS NO ID. DATABASE DIDN'T RESPOND OR DIDN'T RESPOND CORRECTLY.");
            }

            //Change to RegistrationView after login
            mainWindow.ChildViewModel = mainWindow.Registration;
        }
    }
}
