using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace RaceReg.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _switchView;
        public int SwitchView
        {
            get
            {
                return _switchView;
            }
            set
            {
                Set(ref _switchView, value);
            }
        }

        //Default constructor (not needed)
        //public MainWindowViewModel() : this() { }

        public MainWindowViewModel()
        {
            SwitchView = 0;

            Login = new LoginViewModel(this);
            CreateAccount = new CreateAccountViewModel(this);
            Registration = new RegistrationViewModel(this);

            ChildViewModel = Login;
        }

        private LoginViewModel login;
        private CreateAccountViewModel createAccount;
        private object _childViewModel;
        private RegistrationViewModel _registration;

        public object ChildViewModel { get => _childViewModel; set => Set(ref _childViewModel, value); }
        public LoginViewModel Login { get => login; set => Set(ref login, value); }
        public CreateAccountViewModel CreateAccount { get => createAccount; set => Set(ref createAccount, value); }
        public RegistrationViewModel Registration { get => _registration; set => Set(ref _registration, value); }
    }
}
