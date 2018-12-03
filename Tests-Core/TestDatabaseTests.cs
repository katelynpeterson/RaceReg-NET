using Moq;
using NUnit.Framework;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Core
{
    [TestFixture]
    class TestDatabaseTests
    {
        public IRaceRegDB CreateDatabase()
        {
            Mock<IRaceRegDB> testMockDatabase = new Mock<IRaceRegDB>();

            var affiliations = new List<Affiliation>();
            var participants = new List<Participant>();
            var users = new List<User>();

            testMockDatabase.Setup(m => m.AddNewAffiliationAsync(It.IsAny<Affiliation>())).Callback<Affiliation>(c => affiliations.Add(c));
            //testMockDatabase.Setup(m => m.RefreshAffiliations()).ReturnsAsync(Task<IEnumerable<Affiliation>>);

            return testMockDatabase.Object;
        }


        [TestCase(5)]
        public async Task RefreshAffiliationsTest(int numAffiliations)
        {
            /** Make a new database **/
            var testDB = new TestDatabase(numAffiliations, 0, 0);

            var affiliationsList = await testDB.RefreshAffiliations();
            ObservableCollection<Affiliation> affiliations = new ObservableCollection<Affiliation>(affiliationsList);

            Assert.AreEqual(affiliations.Count, numAffiliations);
        }

        [TestCase(1, 5)]
        public async Task RefreshParticipantsTest(int numAffiliations, int numParticipants)
        {
            /** Make a new database **/
            var testDB = new TestDatabase(numAffiliations, numParticipants, 0);

            var affiliationsList = await testDB.RefreshAffiliations();
            ObservableCollection<Affiliation> affiliations = new ObservableCollection<Affiliation>(affiliationsList);

            var participantsList = await testDB.RefreshParticipants();
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>(participantsList);

            Assert.AreEqual(affiliations.Count, numAffiliations);
            Assert.AreEqual(participants.Count, numParticipants);
        }

        [TestCase("jacksonporter", "Jackson", "Porter", "jackson.porter@racereg.run", "MHS")]
        public async Task GrabUserDetailsAsyncTest(string username, string firstName, string lastName, string email, string affiliationAbbreviation)
        {
            TestDatabase testDatabase = new TestDatabase();
            TestDialogService testDialogService = new TestDialogService();

            User user = new User();
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            /** Make an affiliation with matching abbreviation and store in DB **/
            Affiliation matchingAffiliation = new Affiliation();
            matchingAffiliation.Abbreviation = affiliationAbbreviation;
            await testDatabase.AddNewAffiliationAsync(matchingAffiliation);

            user.Affiliation = matchingAffiliation;

            user = await testDatabase.AddNewUserAsync(user);

            Assert.AreEqual(user.Username, username);
            Assert.AreEqual(user.FirstName, firstName);
            Assert.AreEqual(user.LastName, lastName);
            Assert.AreEqual(user.Email, email);
            Assert.AreEqual(user.Affiliation.Abbreviation, affiliationAbbreviation);
        }

        [TestCase("Jackson", "Porter", "m", "MHS", "1997-01-01")]
        public async Task SaveNewParticipantTest(string firstName, string lastName, string gender, string affiliationAbbreviation, string birthDate)
        {
            /** Make a new database **/
            var testDB = new TestDatabase();

            /** Make the participant **/
            Participant newParticipant = new Participant();

            newParticipant.FirstName = firstName;
            newParticipant.LastName = lastName;

            if (gender.Equals("m"))
            {
                newParticipant.Gender = Participant.GenderType.Male;
            }
            else if (gender.Equals("f"))
            {
                newParticipant.Gender = Participant.GenderType.Female;
            }
            else
            {
                newParticipant.Gender = Participant.GenderType.Other;
            }

            /** Make an affiliation with matching abbreviation and store in DB **/
            Affiliation matchingAffiliation = new Affiliation();
            matchingAffiliation.Abbreviation = affiliationAbbreviation;
            await testDB.AddNewAffiliationAsync(matchingAffiliation);

            /** Store participant in database **/
            Participant returnedParticipant = await testDB.SaveNewParticipant(newParticipant);
            newParticipant.Id = returnedParticipant.Id;

            /** Forces a failed test **/
            //Participant returnedParticipant1 = await testDB.SaveNewParticipant(new Participant());

            /** Verify participant excists **/
            var listParticipants = await testDB.RefreshParticipants();
            ObservableCollection<Participant> participants = new ObservableCollection<Participant>(listParticipants);

            /** Test Equality **/
            Assert.AreEqual(participants[0], newParticipant);
        }

        [TestCase("Murray High School", "MHS")]
        public async Task AddNewAffiliationTest(string name, string abbreviation)
        {
            /** Make a new database **/
            var testDB = new TestDatabase();

            /** Make the affiliation **/
            Affiliation newAffiliation = new Affiliation();

            newAffiliation.Name = name;
            newAffiliation.Abbreviation = abbreviation;

            /** Store affiliation in database **/
            Affiliation returnedAffiliation = await testDB.AddNewAffiliationAsync(newAffiliation);
            newAffiliation.Id = returnedAffiliation.Id;

            /** Forces a failed test **/
            //Affiliation returnedAffiliation1 = await testDB.AddNewAffiliationAsync(new Affiliation());

            /** Verify affiliation excists **/
            var listAffiliations = await testDB.RefreshAffiliations();
            ObservableCollection<Affiliation> affiliations = new ObservableCollection<Affiliation>(listAffiliations);

            /** Test Equality **/
            Assert.AreEqual(affiliations[0], newAffiliation);
        }

        [TestCase("Murray High School", "MHS")]
        public async Task AddNewAffiliationTestMockAsync(string name, string abbreviation)
        {
            var testDB = CreateDatabase();

            /** Make the affiliation **/
            Affiliation newAffiliation = new Affiliation();

            newAffiliation.Name = name;
            newAffiliation.Abbreviation = abbreviation;

            /** Store affiliation in database **/
            Affiliation returnedAffiliation = await testDB.AddNewAffiliationAsync(newAffiliation);
            newAffiliation.Id = returnedAffiliation.Id;

            /** Forces a failed test **/
            //Affiliation returnedAffiliation1 = await testDB.AddNewAffiliationAsync(new Affiliation());

            /** Verify affiliation excists **/
            var listAffiliations = testDB.RefreshAffiliations();
            ObservableCollection<Affiliation> affiliations = new ObservableCollection<Affiliation>(listAffiliations);

            /** Test Equality **/
            Assert.AreEqual(affiliations[0], newAffiliation);
            
        }
    }
}
