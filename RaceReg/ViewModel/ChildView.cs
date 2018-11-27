using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.ViewModel
{
    public class ChildView : ViewModelBase
    {
        protected readonly MainWindowViewModel mainWindow;

        public ChildView(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindow = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
        }

        /** Shared Commands **/
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
                mainWindow.SwitchToAboutView();
            }
            ));

        private RelayCommand goBackCommand;
        public RelayCommand GoBackCommand => goBackCommand ?? (goBackCommand = new RelayCommand(
            () =>
            {
                mainWindow.SwitchToPreviousView();
            }
            ));
    }
}
