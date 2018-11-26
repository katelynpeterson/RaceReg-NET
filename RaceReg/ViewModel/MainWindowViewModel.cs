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
            SwitchView = 2;
        }
    }
}
