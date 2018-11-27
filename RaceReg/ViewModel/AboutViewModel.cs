using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using RaceReg.Helpers;
using System.Collections.Generic;
using System.Text;
using RaceReg.Model;

namespace RaceReg.ViewModel
{
    public class AboutViewModel : ChildView
    {
        public AboutViewModel(MainWindowViewModel mainWindowViewModel) : base(mainWindowViewModel)
        {

        }

        private RelayCommand visitWebsiteCommand;
        public RelayCommand VisitWebsiteCommand => visitWebsiteCommand ?? (visitWebsiteCommand = new RelayCommand(
            () =>
            {
                System.Diagnostics.Process.Start("https://racereg.run");
            }
            ));
    }
}
