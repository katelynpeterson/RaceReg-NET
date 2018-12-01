﻿using System;
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

            for (int i = 0; i < participants.Count(); i++)
            {
                if (users[i].Username.Equals(username))
                {
                    index = i;
                    break; //don't know if this break will work correctly, the for loop might continue. In that case, last matching user in database will be chosen.
                }
            }

            if (index > 0)
            {
                user = users[index];
            }

            return await Task.FromResult(user);
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

        public async Task<IEnumerable<Participant>> RefreshParticipants()
        {
            if (participants.Count == 0)
            {
                Random rand = new Random();

                for (int i = 0; i < rand.Next(1, 101); i++)
                {
                    Participant temp = new Participant();

                    temp.Id = currentParticipantId;
                    currentParticipantId++;

                    temp.FirstName = "Participant " + temp.Id;
                    temp.LastName = "LastName";

                    int random = rand.Next(participants.Count());
                    temp.Affiliation = affiliations[random];

                    random = rand.Next(2);
                    if(random == 0)
                    {
                        temp.Gender = Participant.GenderType.Male;
                    }
                    else if(random == 1)
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
            }

            return await Task.FromResult(participants);
        }

        public async Task<Participant> SaveParticipant(Participant updatedParticipant)
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
