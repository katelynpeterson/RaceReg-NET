using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private LoginViewModel login;
        private AboutViewModel about;
        private CreateAccountViewModel createAccount;
        private object _childViewModel;
        private object _previousChildViewModel;
        private RegistrationViewModel _registration;
        private User currentUser;

        public object PreviousChildViewModel { get => _previousChildViewModel; set => Set(ref _previousChildViewModel, value); }
        public object ChildViewModel { get => _childViewModel; set => Set(ref _childViewModel, value); }
        public LoginViewModel Login { get => login; set => Set(ref login, value); }
        public AboutViewModel About { get => about; set => Set(ref about, value); }
        public CreateAccountViewModel CreateAccount { get => createAccount; set => Set(ref createAccount, value); }
        public RegistrationViewModel Registration { get => _registration; set => Set(ref _registration, value); }
        public User CurrentUser { get => currentUser; set => Set(ref currentUser, value); }

        public MainWindowViewModel()
        {
            Login = new LoginViewModel(this);
            About = new AboutViewModel(this);
            CreateAccount = new CreateAccountViewModel(this);
            Registration = new RegistrationViewModel(this);

            ChildViewModel = Login;
        }

        public void SwitchView(object viewModel)
        {
            PreviousChildViewModel = ChildViewModel;
            ChildViewModel = viewModel;
        }

        public void SwitchToPreviousView()
        {
            object temp = ChildViewModel;
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

        

    }
}
