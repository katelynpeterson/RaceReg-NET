using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using System.Text;
using GalaSoft.MvvmLight.Command;
using RaceReg.Helpers;

namespace RaceReg.ViewModel
{
    public class CreateAffiliationViewModel : ChildView
    {
        private IRaceRegDB _database;
        private IDialogService _dialogService;

        private readonly MainWindowViewModel mainWindowViewModel;

        private string _affiliationMessage;
        public String AffiliationMessage
        {
            get
            {
                return _affiliationMessage;
            }
            set
            {
                Set(ref _affiliationMessage, value);
            }
        }

        private Affiliation _affiliation;
        public Affiliation Affiliation
        {
            get
            {
                return _affiliation;
            }
            set
            {
                Set(ref _affiliation, value);
            }
        }

        public ObservableCollection<Affiliation> affiliations;
        public ObservableCollection<Affiliation> Affiliations
        {
            get
            {
                return affiliations;
            }
            set
            {
                Set(ref affiliations, value);
            }
        }

        private RelayCommand backToCreateAccountCommand;
        public RelayCommand BackToCreateAccountCommand => backToCreateAccountCommand ?? (backToCreateAccountCommand = new RelayCommand(
            () =>
            {
                mainWindow.SwitchToCreateAccountView();
            }
            ));

        private RelayCommand createNewAffiliation;
        public RelayCommand CreateNewAffiliation => createNewAffiliation ?? (createNewAffiliation = new RelayCommand(
            async () =>
            {
                Affiliation = await _database.AddNewAffiliationAsync(Affiliation);
                if(Affiliation == null)
                {
                    AffiliationMessage = "Database Communication Error";
                }
                else if (Affiliation.Id <= 0)
                {
                    AffiliationMessage = "Affiliation Already Excists";
                }
                else
                {
                    mainWindow.SwitchToCreateAccountView();
                }
            }
            ));


        private RelayCommand refreshAffiliations;

        public RelayCommand RefreshAffiliations => refreshAffiliations ?? (refreshAffiliations = new RelayCommand(
            () =>
            {
                mainWindow.QueryDatabase();
            }
            ));

        public CreateAffiliationViewModel(MainWindowViewModel mainWindowViewModel, IRaceRegDB db, IDialogService dialogService) : base(mainWindowViewModel)
        {
            Affiliations = new ObservableCollection<Affiliation>();
            Affiliation = new Affiliation();

            _database = db;
            _dialogService = dialogService;
        }

        //Default constructor
        public CreateAffiliationViewModel(MainWindowViewModel mainWindowViewModel) : this(mainWindowViewModel, new RaceRegDatabase(), new DialogService()) { }
    }
}
