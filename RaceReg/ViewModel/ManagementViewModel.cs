using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.Helpers;
using RaceReg.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace RaceReg.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ManagementViewModel : ViewModelBase
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ManagementViewModel(IRaceRegDB RaceRegDB,
            IDialogService dialogService)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            ChildViewModels = new ObservableCollection<ChildControl>();

            _database = RaceRegDB;
            _dialogService = dialogService;

            Affiliations = new ObservableCollection<Affiliation>();
            Participants = new ObservableCollection<Participant>();
            QueryDatabase();
        }

        public async void QueryDatabase()
        {
            var getAffiliations = await _database.RefreshAffiliations().ConfigureAwait(true);
            Affiliations.Clear();
            
            foreach(Affiliation affiliation in getAffiliations)
            {
                Affiliations.Add(affiliation);
            }
            Affiliations = new ObservableCollection<Affiliation>(getAffiliations);

            var getParticipants = await _database.RefreshParticipants().ConfigureAwait(true);
            Participants.Clear();

            foreach(Participant participant in getParticipants)
            {
                Participants.Add(participant);
            }
        }

        //Default constructor
        public ManagementViewModel() : this(new Database(), new DialogService()) { }

        private ObservableCollection<ChildControl> childViewModels;
        public ObservableCollection<ChildControl> ChildViewModels
        {
            get
            {
                return childViewModels;
            }
            set
            {
                Set(ref childViewModels, value);
            }
        }

        private ChildControl selectedChildViewModel;
        public ChildControl SelectedChildViewModel
        {
            get
            {
                return selectedChildViewModel;
            }
            set
            {
                Set(ref selectedChildViewModel, value);
            }
        }
        

        private RelayCommand addParticipantView;
        public RelayCommand AddParticipantView => addParticipantView ?? (addParticipantView = new RelayCommand(
            () =>
            {
                ParticipantViewModel participantEditorViewModel = new ParticipantViewModel(_database);
                participantEditorViewModel.Affiliations = this.Affiliations;
                ChildViewModels.Add(new ChildControl("Participant Editor", participantEditorViewModel));
                SelectedChildViewModel = ChildViewModels.Last();
            }
            ));

        private RelayCommand addAllParticipantsView;
        public RelayCommand AddAllParticipantView => addAllParticipantsView ?? (addAllParticipantsView = new RelayCommand(
            () =>
            {
                AllParticipantViewModel allParticipantEditorViewModel = new AllParticipantViewModel();
                allParticipantEditorViewModel.Affiliations = this.Affiliations;
                allParticipantEditorViewModel.Participants = this.Participants;

                ChildViewModels.Add(new ChildControl("All Participants", allParticipantEditorViewModel));
                SelectedChildViewModel = ChildViewModels.Last();
            }
            ));

        private RelayCommand refreshItems;
        public RelayCommand RefreshItems => refreshItems ?? (refreshItems = new RelayCommand(
            () =>
            {
                QueryDatabase();
            }
            ));

        

        public ObservableCollection<Affiliation> Affiliations { get; set; }

        public ObservableCollection<Participant> Participants { get; set; }

        private Participant _selectedParticipant;
        public Participant SelectedParticipant
        {
            get
            {
                return _selectedParticipant;
            }
            set
            {
                if (_selectedParticipant == value)
                {
                    return;
                }

                _selectedParticipant = value;
                RaisePropertyChanged("SelectedParticipant");
            }
        }
     

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if(EqualityComparer<T>.Default.Equals(field, value))
        //    {
        //        return false;
        //    }
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
    }
}