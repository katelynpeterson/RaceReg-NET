using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaceReg.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private int switchView;
        public int SwitchView
        {
            get
            {
                return switchView;
            }
            set
            {
                Set(ref switchView, value);
            }
        }

        //Default constructor (not needed)
        //public MainWindowViewModel() : this() { }

        public MainWindowViewModel()
        {
            SwitchView = 0;   
        }
    }
}
