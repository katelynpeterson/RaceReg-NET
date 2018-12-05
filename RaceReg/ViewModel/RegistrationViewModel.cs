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
    public class RegistrationViewModel : ChildView
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

        public RegistrationViewModel(MainWindowViewModel mainWindowViewModel, IRaceRegDB RaceRegDB,
            IDialogService dialogService) : base(mainWindowViewModel)
        {
            ChildViewModels = new ObservableCollection<ChildControl>();

            _database = RaceRegDB;
            _dialogService = dialogService;

            mainWindow.QueryDatabase();

            
        }

        //Default constructor
        public RegistrationViewModel(MainWindowViewModel mainWindowViewModel) : this(mainWindowViewModel, new RaceRegDatabase(), new DialogService()) {}

        

        private RelayCommand addParticipantView;
        public RelayCommand AddParticipantView => addParticipantView ?? (addParticipantView = new RelayCommand(
            () =>
            {
                ParticipantViewModel participantEditorViewModel = new ParticipantViewModel(mainWindow, _database);
                ChildViewModels.Add(new ChildControl("Participant Editor", participantEditorViewModel));
                SelectedChildViewModel = ChildViewModels.Last();
            }
            ));

        private RelayCommand addAllParticipantsView;
        public RelayCommand AddAllParticipantView => addAllParticipantsView ?? (addAllParticipantsView = new RelayCommand(
            () =>
            {
                AllParticipantViewModel allParticipantEditorViewModel = new AllParticipantViewModel(mainWindow);
                ChildViewModels.Add(new ChildControl("All Participants", allParticipantEditorViewModel));
                SelectedChildViewModel = ChildViewModels.Last();
            }
            ));

        private RelayCommand refreshItems;
        private MainWindowViewModel mainWindowViewModel;

        public RelayCommand RefreshItems => refreshItems ?? (refreshItems = new RelayCommand(
            () =>
            {
                mainWindow.QueryDatabase();
            }
            ));

    }
}
