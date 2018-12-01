using NUnit.Framework;
using RaceReg.Model;
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
    }
}
