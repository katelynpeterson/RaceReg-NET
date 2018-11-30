using Moq;
using NUnit.Framework;
using RaceReg.Helpers;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;

namespace Tests
{

    [TestFixture]
    public class RegistrationViewModelTests
    {
        [Test]
        public void TestMethod1()
        {
            Assert.Pass();

            var testDB = new TestDB();
            var testDialogService = new TestDialogService();

            var mainWindowViewModel = new MainWindowViewModel();

            var registrationViewModel = new RegistrationViewModel(mainWindowViewModel, testDB, testDialogService);

            /** USING MOQ TO CREATE TEST DATABSE - FOR LATER DEVELOPMENT **/
            //var dbMock = new Mock<IRaceRegDB>();
            //dbMock.Setup(m => m.RefreshAffiliations()).ReturnsAsync(new List<Affiliation>());
            ////dbMock.Setup(m=>m.SaveParticipant(It.IsAny<Participant>()))

            //var main2 = new RegistrationViewModel(mainWindowViewModel, dbMock.Object, testDialogService);

        }

        private class TestDB : IRaceRegDB
        {
            public Task<Affiliation> AddNewAffiliationAsync(Affiliation affiliation)
            {
                throw new NotImplementedException();
            }

            public Task<User> GrabUserDetailsAsync(string username, SecureString password)
            {
                throw new NotImplementedException();
            }

            public async Task<IEnumerable<Affiliation>> RefreshAffiliations()
            {
                return await Task.FromResult(new List<Affiliation>());
            }

            public Task<IEnumerable<Participant>> RefreshParticipants()
            {
                throw new NotImplementedException();
            }

            public Task<string> Save(Participant updatedParticipant)
            {
                throw new NotImplementedException();
            }

            public Task<Participant> SaveParticipant(Participant updatedParticipant)
            {
                throw new NotImplementedException();
            }
        }

        private class TestDialogService : IDialogService
        {
            public void ShowMessage(string message)
            {
                //Do nothing
            }
        }
    }
}
