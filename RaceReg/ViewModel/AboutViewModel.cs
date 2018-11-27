using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using RaceReg.Helpers;
using System.Collections.Generic;
using System.Text;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel mainWindow;

        public AboutViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindow = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
        }

        private RelayCommand command;
        public RelayCommand Command => command ?? (command = new RelayCommand(
            () =>
            {
                
            }
            ));
    }
}
