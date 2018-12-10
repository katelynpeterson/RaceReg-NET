using NUnit.Framework;
using RaceReg.Helpers;
using RaceReg.ViewModel;
using System;
using Tests_Core;

namespace Tests_Core
{

    [TestFixture]
    public class AddParticipantViewModelTests
    {
        [TestCase("Jackson", "Porter", 1, "Male", "1997-01-02")]
        public void SaveNewParticipant(string firstName, string lastName, int affiliationId, string gender, string birthdate)
        {

        }
    }
}
