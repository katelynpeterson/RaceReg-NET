using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using RaceReg.Model;
using RaceReg.ViewModel;

namespace Tests_Core
{
    public class TestDatabase
    {
        List<Affiliation> affiliations;
        List<Participant> participants;
        int currentAffiliationId;
        int currentParticipantId;

        public TestDatabase()
        {
            affiliations = new List<Affiliation>();
            currentAffiliationId = 1;
            currentParticipantId = 1;
        }

        public async Task<Affiliation> AddNewAffiliationAsync(Affiliation affiliation)
        {
            affiliation.Id = currentAffiliationId;
            currentAffiliationId++;
            affiliations.Add(affiliation);

            return await Task.FromResult(affiliations[affiliations.Count() - 1]);
        }

        public Task<User> GrabUserDetailsAsync(string username, SecureString password)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Affiliation>> RefreshAffiliations()
        {
            if(affiliations.Count == 0)
            {
                Random rand = new Random();

                for (int i = 0; i < rand.Next(1, 51); i++)
                {
                    Affiliation temp = new Affiliation();
                    temp.Id = currentAffiliationId;
                    currentAffiliationId++;
                    temp.Name = "Affiliation " + temp.Id;
                    temp.Abbreviation = "A" + temp.Id;
                    affiliations.Add(temp);
                }
            }

            return await Task.FromResult(affiliations);
        }

        public Task<IEnumerable<Participant>> RefreshParticipants()
        {
            throw new NotImplementedException();
        }

        public Task<string> Save(Participant updatedParticipant)
        {
            throw new NotImplementedException();
        }

        public async Task<Participant> SaveParticipantAsync(Participant updatedParticipant)
        {
            int index = -1;
            for(int i = 0;  i < participants.Count(); i++)
            {
                if (participants[i].Id == updatedParticipant.Id)
                {
                    participants[i] = updatedParticipant;
                    index = i;
                    break;
                }
            }

            if(index < 0)
            {
                Participant toReturn = null;
                return await Task.FromResult(toReturn);
            }
            else
            {
                return await Task.FromResult(participants[index]);
            }
        }
    }
}
