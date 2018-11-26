using NUnit.Framework;
using RaceReg.Helpers;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{

    [TestFixture]
    public class ViewModelTests
    {
        [Test]
        public void TestMethod1()
        {
            Assert.Fail();

            var testDB = new TestDB();
            var testDialogService = new TestDialogService();

            var mainViewModel = new ManagementViewModel(testDB, testDialogService);
        }

        private class TestDB : IRaceRegDB
        {
            public Task<IEnumerable<Affiliation>> RefreshAffiliations()
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }
    }
}
