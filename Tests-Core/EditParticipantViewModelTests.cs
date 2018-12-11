using NUnit.Framework;
using RaceReg.Helpers;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tests_Core;

namespace Tests_Core
{

    [TestFixture]
    public class EditParticipantViewModelTests
    {
        [TestCase("Jackson", "Porter", "Male", "1997-01-02", "1997-01-03")]
        public async Task EditParticipantAsync(string firstName, string lastName, string gender, string birthdate, string birthdateCorrected)
        {
            /** Make a new database **/
            var testDB = new TestDatabase();

            var testDialogService = new TestDialogService();

            var mainWindowVM = new MainWindowViewModel(testDB, testDialogService);

            mainWindowVM.CreateAffiliation.Affiliation.Name = "My Affiliation";
            mainWindowVM.CreateAffiliation.Affiliation.Abbreviation = "MA";

            mainWindowVM.CreateAffiliation.CreateNewAffiliation.Execute(null);

            mainWindowVM.CurrentUser.Affiliation = mainWindowVM.CreateAffiliation.Affiliation;

            mainWindowVM.Registration.AddParticipantView.Execute(null);
            var addParticipantVM = (AddParticipantViewModel) mainWindowVM.Registration.SelectedChildViewModel;

            addParticipantVM.Participant.FirstName = firstName;
            addParticipantVM.Participant.LastName = lastName;
            if (gender.Equals("Male"))
            {
                addParticipantVM.Participant.Gender = Participant.GenderType.Male;
            }
            else if (gender.Equals("Female"))
            {
                addParticipantVM.Participant.Gender = Participant.GenderType.Female;
            }
            else
            {
                addParticipantVM.Participant.Gender = Participant.GenderType.Other;
            }
            addParticipantVM.Participant.BirthDate = Convert.ToDateTime(birthdate);

            addParticipantVM.SaveNewParticipant.Execute(null);



            //** Now edit the participant **/
            mainWindowVM.Registration.AddAllParticipantsView.Execute(null);

            var allParticipantVM = (AllParticipantViewModel) mainWindowVM.Registration.SelectedChildViewModel;

            var tempParticipant = new Participant();
            tempParticipant.Id = addParticipantVM.Participant.Id;
            tempParticipant.FirstName = addParticipantVM.Participant.FirstName;
            tempParticipant.LastName = addParticipantVM.Participant.LastName;
            tempParticipant.Gender = addParticipantVM.Participant.Gender;
            tempParticipant.BirthDate = addParticipantVM.Participant.BirthDate;
            tempParticipant.Affiliation = addParticipantVM.Participant.Affiliation;


            allParticipantVM.SelectedParticipant = tempParticipant;

            allParticipantVM.EditParticipant.Execute(null);

            var editParticipantVM = (EditParticipantViewModel) mainWindowVM.Registration.SelectedChildViewModel;
            editParticipantVM.Participant.BirthDate = Convert.ToDateTime(birthdateCorrected);

            editParticipantVM.SaveParticipant.Execute(null);


            /** Verify participant excists **/
            var listParticipants = await testDB.RefreshParticipants();
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>(listParticipants);

            /** Test Equality **/
            Assert.AreEqual(participants[0].BirthDate, Convert.ToDateTime("1997-01-03"));
        }
    }
}
