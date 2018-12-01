using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceReg.Model
{
    public interface IRaceRegDB
    {
        Task<User> GrabUserDetailsAsync(string username, System.Security.SecureString password);
        Task<IEnumerable<Affiliation>> RefreshAffiliations();
        Task<IEnumerable<Participant>> RefreshParticipants();
        Task<Participant> SaveNewParticipant(Participant updatedParticipant);
        Task<Affiliation> AddNewAffiliationAsync(Affiliation affiliation);
    }
}
