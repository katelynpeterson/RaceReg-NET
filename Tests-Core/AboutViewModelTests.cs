using NUnit.Framework;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests_Core
{
    [TestFixture]
    class AboutViewModelTests
    {
        [Test]
        public async System.Threading.Tasks.Task SwitchToPreviousViewTestAsync()
        {
            TestDatabase testDatabase = new TestDatabase();
            TestDialogService testDialogService = new TestDialogService();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(testDatabase, testDialogService);

            mainWindowViewModel.ChildViewModel = mainWindowViewModel.Login;
            mainWindowViewModel.Login.AboutCommand.Execute(null);

            ChildView childView = mainWindowViewModel.ChildViewModel;
            childView.GoBackCommand.Execute(null);
            

            Assert.AreEqual(mainWindowViewModel.Login.GetType(), mainWindowViewModel.ChildViewModel.GetType());
        }
    }
}
