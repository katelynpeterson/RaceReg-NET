using NUnit.Framework;
using RaceReg.Model;
using RaceReg.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests_Core
{
    [TestFixture]
    class LoginViewModelTests
    {
        [Test]
        public async Task SwitchViewLoginToRegistrationTestAsync()
        {
            TestDatabase testDatabase = new TestDatabase();
            TestDialogService testDialogService = new TestDialogService();

            User user = new User();
            user.Username = "username";
            await testDatabase.AddNewUserAsync(user);

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(testDatabase, testDialogService);
            mainWindowViewModel.Login.Username = "username";

            mainWindowViewModel.Login.LoginCommand.Execute(null);

            var cvm = mainWindowViewModel.ChildViewModel;

            Assert.AreEqual(mainWindowViewModel.Registration, cvm);
        }

        [Test]
        public async Task CheckLoginTest()
        {
            TestDatabase testDatabase = new TestDatabase();
            TestDialogService testDialogService = new TestDialogService();

            User user = new User();
            user.Username = "username";
            await testDatabase.AddNewUserAsync(user);

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(testDatabase, testDialogService);
            mainWindowViewModel.Login.Username = "username";

            mainWindowViewModel.Login.LoginCommand.Execute(null);

            user = mainWindowViewModel.CurrentUser;
            Assert.AreEqual(user.Username, "username");
        }
    }
}
