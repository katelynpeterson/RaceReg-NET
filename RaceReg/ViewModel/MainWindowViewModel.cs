using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Login = new LoginViewModel(this);
            CreateAccount = new CreateAccountViewModel(this);
            Registration = new RegistrationViewModel(this);

            ChildViewModel = Login;
        }

        private LoginViewModel login;
        private CreateAccountViewModel createAccount;
        private object _childViewModel;
        private RegistrationViewModel _registration;
        private User currentUser;

        public object ChildViewModel { get => _childViewModel; set => Set(ref _childViewModel, value); }
        public LoginViewModel Login { get => login; set => Set(ref login, value); }
        public CreateAccountViewModel CreateAccount { get => createAccount; set => Set(ref createAccount, value); }
        public RegistrationViewModel Registration { get => _registration; set => Set(ref _registration, value); }
        public User CurrentUser { get => currentUser; set => Set(ref currentUser, value); }
    }
}
