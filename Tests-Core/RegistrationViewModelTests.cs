using NUnit.Framework;
using RaceReg.Helpers;
using System;

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

        

        private class TestDialogService : IDialogService
        {
            public void ShowMessage(string message)
            {
                //Do nothing
            }
        }
    }
}
