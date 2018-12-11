using GalaSoft.MvvmLight;

namespace RaceReg.ViewModel
{
    public class ChildControl : ViewModelBase
    {
        public ChildControl(string header)
        {
            Header = header;
        }

        public string Header { get; set; }
        public object ViewModel { get { return this; } }
    }
}
