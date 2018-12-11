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
    public class AddParticipantViewModelTests
    {
        [TestCase("Jackson", "Porter", "Male", "1997-01-02")]
        public async Task SaveNewParticipantAsync(string firstName, string lastName, string gender, string birthdate)
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

            /** Verify participant excists **/
            var listParticipants = await testDB.RefreshParticipants();
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>(listParticipants);

            /** Test Equality **/
            Assert.AreEqual(participants[0], addParticipantVM.Participant);
        }
    }
}
