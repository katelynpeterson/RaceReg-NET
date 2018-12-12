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
    public class AddMeetViewModelTests
    {
        [TestCase("Snow College Track Invite", "Test Track Invite for Any Affiliation", "2018-12-10 10:00:00", "2018-12-11")]
        public async Task SaveNewMeetAsync(string name, string description, string startDateTime, string endDate)
        {
            /** Make a new database **/
            var testDB = new TestDatabase();

            var testDialogService = new TestDialogService();

            var mainWindowVM = new MainWindowViewModel(testDB, testDialogService);

            mainWindowVM.CreateAffiliation.Affiliation.Name = "My Affiliation";
            mainWindowVM.CreateAffiliation.Affiliation.Abbreviation = "MA";

            mainWindowVM.CreateAffiliation.CreateNewAffiliation.Execute(null);

            /** Make a temporary user **/
            var user = new User();
            user.FirstName = "Database";
            user.LastName = "Tester";
            user.Username = "databasetester";
            user.Email = "databasetest@racereg.run";
            user.Affiliation = mainWindowVM.CreateAffiliation.Affiliation;

            user = await testDB.AddNewUserAsync(user);

            mainWindowVM.CurrentUser = user;

            mainWindowVM.Registration.AddAddMeetView.Execute(null);
            var addMeetVM = (AddMeetViewModel) mainWindowVM.Registration.SelectedChildViewModel;

            addMeetVM.Meet.Name = name;
            addMeetVM.Meet.Description = description;
            addMeetVM.Meet.StartDateTime = Convert.ToDateTime(startDateTime);
            addMeetVM.Meet.EndDate = Convert.ToDateTime(endDate);

            addMeetVM.StartDateTimeString = addMeetVM.Meet.StartDateTime.ToString();
            addMeetVM.SaveNewMeet.Execute(null);

            /** Get the meet(s) **/
            var meetsIEnum = await testDB.RefreshMeetsAsync(user);
            ObservableCollection<Meet> meets = new ObservableCollection<Meet>(meetsIEnum);

            var theMeet = meets.Last();

            /** Test Equality **/
            Assert.AreEqual(theMeet.Name, name);
            Assert.AreEqual(theMeet.Description, description);
            Assert.AreEqual(theMeet.StartDateTime, Convert.ToDateTime(startDateTime));
            Assert.AreEqual(theMeet.EndDate, Convert.ToDateTime(endDate));
            Assert.AreEqual(theMeet.Id, meets.Count());
            Assert.AreEqual(theMeet.UserId, user.Id);            
        }
    }
}
