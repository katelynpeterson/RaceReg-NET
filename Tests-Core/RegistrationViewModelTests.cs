using NUnit.Framework;
using RaceReg.Helpers;
using RaceReg.ViewModel;
using System;
using Tests_Core;

namespace Tests
{

    [TestFixture]
    public class RegistrationViewModelTests
    {
        

        /** USING MOQ TO CREATE TEST DATABSE - FOR LATER DEVELOPMENT **/
        //var dbMock = new Mock<IRaceRegDB>();
        //dbMock.Setup(m => m.RefreshAffiliations()).ReturnsAsync(new List<Affiliation>());
        ////dbMock.Setup(m=>m.SaveParticipant(It.IsAny<Participant>()))
        //var main2 = new RegistrationViewModel(mainWindowViewModel, dbMock.Object, testDialogService);
    }
}
