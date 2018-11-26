using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using RaceReg.Helpers;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private PasswordRelayCommand loginCommand;
        public PasswordRelayCommand LoginCommand => loginCommand ?? (loginCommand = new PasswordRelayCommand(Login));

        private PasswordRelayCommand createAccountCommand;
        public PasswordRelayCommand CreateAccountCommand => createAccountCommand ?? (createAccountCommand = new PasswordRelayCommand(Login));


        private void Login(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if(passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                //Do stuff with the password
            }

            //Change to Managment View

            throw new NotImplementedException();
        }
    }
}
