using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        ICharityRepository Charities { get; }
        ICampaignRepository Campaigns { get; }
        IDonationRepository Donations { get; }
        IVolunteerTaskRepository VolunteerTask { get; }
        IVolunteerUserRepository VolunteerUser { get; }
        IVolunteerApplicationHistoryRepository VolunteerApplicationHistory { get; }
        IPublicRepository publicStats { get; }
        Task<int> SaveChangesAsync();
    }
}
