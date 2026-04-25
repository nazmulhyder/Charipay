using Charipay.Application.Interfaces.Repositories;
using Charipay.Infrastructure.Data;
using Charipay.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository Users { get; }

        public IUserRoleRepository UserRoles { get; }

        public ICharityRepository Charities { get; }

        public ICampaignRepository Campaigns { get; }
        public IDonationRepository Donations { get; }
        public IVolunteerTaskRepository VolunteerTask { get; }
        public IVolunteerUserRepository VolunteerUser { get; }
        public  IVolunteerApplicationHistoryRepository VolunteerApplicationHistory { get; }
        public IPublicRepository publicStats { get; }

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IUserRoleRepository userRoles,
            ICharityRepository _charities, ICampaignRepository _Campains, IDonationRepository _donations, IVolunteerTaskRepository _volunteerTaskRepository,
            IVolunteerUserRepository _volunteerUser, IVolunteerApplicationHistoryRepository _volunteerApplicationHistory, IPublicRepository _publicRepository
            )
        {
            _context = context;
            Users = userRepository;
            UserRoles = userRoles;
            Charities = _charities;
            Campaigns = _Campains;
            Donations = _donations;
            VolunteerTask = _volunteerTaskRepository;
            VolunteerUser = _volunteerUser;
            VolunteerApplicationHistory = _volunteerApplicationHistory;
            publicStats = _publicRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
