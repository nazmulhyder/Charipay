using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IVolunteerUserRepository : IRepository<VolunteerUser>
    {
        Task<bool> HasUserAlreadyAppliedAsync(Guid userId, Guid volunteerTaskId);
        Task<int> GetActiveApplicationCountAsync(Guid volunteerTaskId);
    }
}
