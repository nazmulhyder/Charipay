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
        Task<int> SaveChangesAsync();
    }
}
