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
    public class TestDatabase : IRaceRegDB
    {
        List<Affiliation> affiliations;
        List<Participant> participants;
        List<User> users;
        int currentAffiliationId, currentParticipantId, currentUserId;

        public TestDatabase()
        {
            affiliations = new List<Affiliation>();
            participants = new List<Participant>();
            users = new List<User>();
            currentAffiliationId = 1;
            currentParticipantId = 1;
            currentUserId = 1;
        }

        public TestDatabase(bool random)
        {
            affiliations = new List<Affiliation>();
            participants = new List<Participant>();
            users = new List<User>();
            currentAffiliationId = 1;
            currentParticipantId = 1;
            currentUserId = 1;

            if (random)
            {
                /** Randomly populate database **/
                Random rand = new Random();
                int numAffilations = rand.Next(1, 26);
                int numParticipants = rand.Next(1, 101);
                populateDatabase(numAffilations, numParticipants, numAffilations);
            }
        }

        public TestDatabase(int numAffilations, int numParticipants, int numUsers)
        {
            affiliations = new List<Affiliation>();
            participants = new List<Participant>(); 
            users = new List<User>();
            currentAffiliationId = 1;
            currentParticipantId = 1;
            currentUserId = 1;

            populateDatabase(numAffilations, numParticipants, numUsers);
        }

        private void populateDatabase(int numAffilations, int numParticipants, int numUsers)
        {
            if(numAffilations < 1)
            {
                throw new Exception("Affiliation count is lower than 1. Affiliation count MUST be greater than 1.");
            }

            Random rand = new Random();

            /** Populate the affiliations table **/
            for (int i = 0; i < numAffilations; i++)
            {
                Affiliation temp = new Affiliation();
                temp.Id = currentAffiliationId;
                currentAffiliationId++;
                temp.Name = "Affiliation " + temp.Id;
                temp.Abbreviation = "A" + temp.Id;
                affiliations.Add(temp);
            }

            /** Populate the participants table **/
            for (int i = 0; i < numParticipants; i++)
            {
                Participant temp = new Participant();

                temp.Id = currentParticipantId;
                currentParticipantId++;

                temp.FirstName = "Participant " + temp.Id;
                temp.LastName = "LastName";

                int random = rand.Next(affiliations.Count());
                temp.Affiliation = affiliations[random];

                random = rand.Next(2);
                if (random == 0)
                {
                    temp.Gender = Participant.GenderType.Male;
                }
                else if (random == 1)
                {
                    temp.Gender = Participant.GenderType.Female;
                }
                else
                {
                    temp.Gender = Participant.GenderType.Other;
                }

                temp.BirthDate = DateTime.Now.AddDays(rand.Next(1, 365));

                participants.Add(temp);
            }

            /** Populate the users table **/
            for (int i = 0; i < numUsers; i++)
            {
                User temp = new User();
                temp.Id = currentUserId;
                currentUserId++;
                temp.FirstName = "User " + temp.Id;
                temp.LastName = "LastName";
                temp.Email = temp.FirstName + "." + temp.LastName + "@email.com";
                temp.Affiliation = affiliations[i];

                users.Add(temp);
            }
        }

        public async Task<Affiliation> AddNewAffiliationAsync(Affiliation affiliation)
        {
            affiliation.Id = currentAffiliationId;
            currentAffiliationId++;
            affiliations.Add(affiliation);

            return await Task.FromResult(affiliations[affiliations.Count() - 1]);
        }

        public async Task<User> GrabUserDetailsAsync(string username, SecureString password)
        {
            User user = null;
            int index = -1;

            for (int i = 0; i < users.Count(); i++)
            {
                if (users[i].Username.Equals(username))
                {
                    index = i;
                    break; //don't know if this break will work correctly, the for loop might continue. In that case, last matching user in database will be chosen.
                }
            }

            if (index >= 0)
            {
                user = users[index];
            }

            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<Affiliation>> RefreshAffiliations()
        {
            return await Task.FromResult(affiliations);
        }

        public async Task<IEnumerable<Participant>> RefreshParticipants()
        {
            return await Task.FromResult(participants);
        }

        public async Task<Participant> SaveNewParticipant(Participant participant)
        {
            participant.Id = currentParticipantId;
            currentParticipantId++;
            participants.Add(participant);

            return await Task.FromResult(participants[participants.Count() - 1]);
        }

        public async Task<User> AddNewUserAsync(User user)
        {
            user.Id = currentUserId;
            currentUserId++;
            users.Add(user);

            return await Task.FromResult(users[users.Count() - 1]);
        }

        public Task<int> UpdateParticipantAsync(Participant participant)
        {
            foreach(Participant tempParticipant in participants)
            {
                if(tempParticipant.Id == participant.Id)
                {
                    tempParticipant.FirstName = participant.FirstName;
                    tempParticipant.LastName = participant.LastName;
                    tempParticipant.Affiliation = participant.Affiliation;
                    tempParticipant.Gender = participant.Gender;
                    tempParticipant.BirthDate = participant.BirthDate;

                    return Task.FromResult(tempParticipant.Id);
                }
            }

            participant.Id = -1;
            return Task.FromResult(participant.Id);
        }

        public Task<Meet> AddNewMeetAsync(Meet meet, User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Meet>> RefreshMeets(User user)
        {
            throw new NotImplementedException();
        }
    }
}
