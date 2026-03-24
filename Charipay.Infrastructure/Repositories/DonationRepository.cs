using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using Charipay.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Infrastructure.Repositories
{
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        private readonly AppDbContext _appDbContext;
        public DonationRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }    
    }
}
