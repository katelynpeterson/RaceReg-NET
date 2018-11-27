using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.Helpers;
using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RaceReg.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

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

        public RegistrationViewModel(IRaceRegDB RaceRegDB,
            IDialogService dialogService)
        {
            ChildViewModels = new ObservableCollection<ChildControl>();

            _database = RaceRegDB;
            _dialogService = dialogService;

            Affiliations = new ObservableCollection<Affiliation>();
            Participants = new ObservableCollection<Participant>();
            QueryDatabase();
        }

        //Default constructor
        public RegistrationViewModel(MainWindowViewModel mainWindowViewModel) : this(new Database(), new DialogService()) {
            this.mainWindowViewModel = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
        }

        public async void QueryDatabase()
        {
            var getAffiliations = await _database.RefreshAffiliations().ConfigureAwait(true);
            Affiliations.Clear();

            foreach (Affiliation affiliation in getAffiliations)
            {
                Affiliations.Add(affiliation);
            }
            Affiliations = new ObservableCollection<Affiliation>(getAffiliations);

            var getParticipants = await _database.RefreshParticipants().ConfigureAwait(true);
            Participants.Clear();

            foreach (Participant participant in getParticipants)
            {
                Participants.Add(participant);
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
        private MainWindowViewModel mainWindowViewModel;

        public RelayCommand RefreshItems => refreshItems ?? (refreshItems = new RelayCommand(
            () =>
            {
                QueryDatabase();
            }
            ));

    }
}
