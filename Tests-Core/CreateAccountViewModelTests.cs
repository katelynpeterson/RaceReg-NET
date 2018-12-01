using NUnit.Framework;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_Core
{
    [TestFixture]
    class CreateAccountViewModelTests
    {
        [TestCase("jacksonporter", "Jackson", "Porter", "jackson.porter@racereg.run", "MHS")]
        public void CreateNewAccountTest(string username, string firstName, string lastName, string email, string affiliationAbbreviation)
        {
            TestDatabase testDatabase = new TestDatabase();
            TestDialogService testDialogService = new TestDialogService();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(testDatabase, testDialogService);

            User user = new User();
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            /** Make an affiliation with matching abbreviation and store in DB **/
            Affiliation matchingAffiliation = new Affiliation();
            matchingAffiliation.Abbreviation = affiliationAbbreviation;
            user.Affiliation = matchingAffiliation;

            mainWindowViewModel.CreateAccount.User = user;
            mainWindowViewModel.CreateAccount.CreateNewAccount.Execute(null);

            user = mainWindowViewModel.CurrentUser;

            Assert.AreEqual(user.Username, username);
            Assert.AreEqual(user.FirstName, firstName);
            Assert.AreEqual(user.LastName, lastName);
            Assert.AreEqual(user.Email, email);
            Assert.AreEqual(user.Affiliation.Abbreviation, affiliationAbbreviation);
        }
    }
}
