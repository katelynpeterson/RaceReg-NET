using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RaceReg.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.ViewModel
{
    public class AddMeetViewModel : ChildControl
    {
        private IRaceRegDB _database;
        private MainWindowViewModel mainWindow;
        private RegistrationViewModel registrationView;

        private Meet meet;
        public Meet Meet
        {
            get
            {
                return meet;
            }
            set
            {
                Set(ref meet, value);
            }
        }

        private Affiliation affiliation;
        public Affiliation Affiliation
        {
            get
            {
                return affiliation;
            }
            set
            {
                Set(ref affiliation, value);
            }
        }

        private string startDateTimeString;
        public string StartDateTimeString
        {
            get
            {
                return startDateTimeString;
            }
            set
            {
                Set(ref startDateTimeString, value);
            }
        }

        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                Set(ref currentUser, value);
            }
        }

        public async Task SaveNewMeetToDatabaseAsync()
        {
            Meet.StartDateTime = Convert.ToDateTime(StartDateTimeString);
            Meet.StartDateTime = Meet.StartDateTime.AddSeconds((-1) * Meet.StartDateTime.Second);

            var result = await _database.AddNewMeetAsync(Meet, CurrentUser);

            if(result == null)
            {
                registrationView.Message = "Save to database failed! Null result";
                //throw new Exception("Save to database failed! Null result");
            }
            else if(result.Id < 1)
            {
                registrationView.Message = "Save to database failed! Id was not updated.";
                //throw new Exception("Save to database failed! Id was not updated.");
            }
            else
            {
                registrationView.Message = "Succesfully Added New Meet";
                registrationView.CloseTab.Execute(null);
            }
        }

        private RelayCommand saveNewMeet;
        public RelayCommand SaveNewMeet => saveNewMeet ?? (saveNewMeet = new RelayCommand(
            async () =>
            {
                await SaveNewMeetToDatabaseAsync();
            }
            ));

        public AddMeetViewModel(string header, MainWindowViewModel mainWindowViewModel, RegistrationViewModel registrationView, IRaceRegDB db) : base(header)
        {
            _database = db;
            mainWindow = mainWindowViewModel;
            this.registrationView = registrationView;

            Meet = new Meet();

            this.Affiliation = mainWindow.CurrentUser.Affiliation;
            this.CurrentUser = mainWindow.CurrentUser;

            mainWindow.QueryDatabase();
        }
    }
}
