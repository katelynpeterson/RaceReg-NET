using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Model
{
    public class RaceRegDatabase : IRaceRegDB
    {
        public async Task<IEnumerable<Participant>> RefreshParticipants()
        {
            var affiliations = new ObservableCollection<Affiliation>( await RefreshAffiliations());

            List<Participant> participants = new List<Participant>();
            string getParticipantsQuery = "SELECT * FROM " + Constants.PARTICIPANT + " WHERE active = 1;";

            using (var connection = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                await connection.OpenAsync();
                using (var cmd = new MySqlCommand(getParticipantsQuery, connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Participant temp = new Participant();
                        temp.Id = reader.GetInt32(0);
                        temp.FirstName = reader.GetString(1);
                        temp.LastName = reader.GetString(2);
                        int affId = reader.GetInt32(3);
                        foreach (Affiliation tempAffiliation in affiliations)
                        {
                            if (tempAffiliation.Id == affId)
                            {
                                temp.Affiliation = tempAffiliation;
                                break;
                            }
                        }

                        var genderChar = reader.GetString(4);
                        if(String.Equals(genderChar, "m"))
                        {
                            temp.Gender = Participant.GenderType.Male;
                        }
                        else if(String.Equals(genderChar, "f"))
                        {
                            temp.Gender = Participant.GenderType.Male;
                        }
                        else
                        {
                            temp.Gender = Participant.GenderType.Other;
                        }

                        temp.BirthDate = reader.GetDateTime(5);
                        participants.Add(temp);
                    }
            }

            return participants;
        }

        public async Task<IEnumerable<Affiliation>> RefreshAffiliations()
        {
            List<Affiliation> affiliations = new List<Affiliation>();
            string getAffiliationsQuery = "SELECT * FROM " + Constants.AFFILIATION + " WHERE active = 1;";

            using (var connection1 = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                await connection1.OpenAsync();
                using (var cmd = new MySqlCommand(getAffiliationsQuery, connection1))
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (await reader.ReadAsync())
                    {
                        Affiliation temp = new Affiliation();
                        temp.Id = reader.GetInt32(0);
                        temp.Name = reader.GetString(1);
                        temp.Abbreviation = reader.GetString(2);
                        affiliations.Add(temp);
                    }
            }

            return affiliations;
        }

        public async Task<Participant> SaveParticipant(Participant updatedParticipant)
        {
            string saveParticipantStatement = "INSERT INTO " + Constants.PARTICIPANT + " ("
                                                                        + "firstname, "
                                                                        + "lastname, "
                                                                        + "affiliationid, "
                                                                        + "gender, "
                                                                        + "birthdate, "
                                                                        + "active"
                                                                        + ") VALUES ("
                                                                        + "@firstname, "
                                                                        + "@lastname, "
                                                                        + "@affiliationid, "
                                                                        + "@gender, "
                                                                        + "@birthdate, "
                                                                        + "@active"
                                                                        + ");" 
                                                                        + " SELECT last_insert_id();";
            using (var connection1 = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                await connection1.OpenAsync();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection1;
                    cmd.CommandText = saveParticipantStatement;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@firstname", updatedParticipant.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", updatedParticipant.LastName);
                    cmd.Parameters.AddWithValue("@affiliationid", updatedParticipant.Affiliation.Id);
                    if(updatedParticipant.Gender == Participant.GenderType.Male)
                    {
                        cmd.Parameters.AddWithValue("@gender", "m");
                    }
                    else if(updatedParticipant.Gender == Participant.GenderType.Female)
                    {
                        cmd.Parameters.AddWithValue("@gender", "f");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@gender", "o");
                    }


                    cmd.Parameters.AddWithValue("@birthdate", updatedParticipant.BirthDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@active", 1);

                    await cmd.ExecuteNonQueryAsync();

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    if (cmd.LastInsertedId != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    {
                        cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));
                    }

                    var participantId = Convert.ToInt32(cmd.Parameters["@newId"].Value);
                    updatedParticipant.Id = participantId;
                }

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (updatedParticipant.Id == 0 || updatedParticipant.Id == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return null;
                }
                else
                {
                    return updatedParticipant;
                }
            }
        }

        /**
         * Developer note: THIS METHOD DOES NOT SUPPORT PASSWORD CHECKING YET
         */
        public async Task<User> GrabUserDetailsAsync(string username, SecureString password)
        {
            var user = new User();
            var affiliationId = -1;
            var participantId = -1;
            var affiliations = new ObservableCollection<Affiliation>(await RefreshAffiliations());
            var participants = new ObservableCollection<Participant>(await RefreshParticipants());

            string getUserDetails = "SELECT * FROM " + Constants.USER + " WHERE username = @username;";

            using (var connection1 = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                await connection1.OpenAsync();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection1;
                    cmd.CommandText = getUserDetails;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@username", username);
                    //Add password parameter in the near future

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        user.Id = reader.GetInt32(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        affiliationId = reader.GetInt32(3);
                        user.Email = reader.GetString(5);

                        if(!(await reader.IsDBNullAsync(6)))
                        {
                            participantId = reader.GetInt32(6);
                        }
                    }

                    //In the future, this should be connected to a list of affiliations in that the whole program shares
                    foreach (Affiliation tempAffiliation in affiliations)
                    {
                        if (tempAffiliation.Id == affiliationId)
                        {
                            user.Affiliation = tempAffiliation;
                            break;
                        }
                    }

                    //In the future, this should be connected to a list of participants in that the whole program shares
                    foreach (Participant participant in participants)
                    {
                        if (participant.Id == participantId)
                        {
                            user.Participant = participant;
                            break;
                        }
                    }
                }
            }
            return user;
        }

        public async Task<Affiliation> AddNewAffiliationAsync(Affiliation affiliation)
        {
            var affiliations = new ObservableCollection<Affiliation>(await RefreshAffiliations());
            string saveAffiliationStatement = "INSERT INTO " + Constants.AFFILIATION + " ("
                                                                        + "name, "
                                                                        + "abbreviation, "
                                                                        + "active"
                                                                        + ") VALUES ("
                                                                        + "@name, "
                                                                        + "@abbreviation, "
                                                                        + "@active"
                                                                        + ");"
                                                                        + " SELECT last_insert_id();";
            using (var connection1 = new MySqlConnection(Constants.CONNECTION_STRING))
            {
                await connection1.OpenAsync();
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection1;
                    cmd.CommandText = saveAffiliationStatement;
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@name", affiliation.Name);
                    cmd.Parameters.AddWithValue("@abbreviation", affiliation.Abbreviation);
                    cmd.Parameters.AddWithValue("@active", 1);

                    await cmd.ExecuteNonQueryAsync();

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    if (cmd.LastInsertedId != null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                    {
                        cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));
                    }

                    var affiliationId = Convert.ToInt32(cmd.Parameters["@newId"].Value);
                    affiliation.Id = affiliationId;
                }

#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (affiliation.Id == 0 || affiliation.Id == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                {
                    return null;
                }
                else
                {
                    foreach (Affiliation tempAffiliation in affiliations)
                    {
                        if (affiliation.Name == tempAffiliation.Name || affiliation.Abbreviation == tempAffiliation.Abbreviation)
                        {
                            affiliation.Id = -1;
                        }
                    }

                    return affiliation;
                }
            }
        }
    }
}
