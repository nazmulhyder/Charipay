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
    public class VolunteerApplicationHistoryRepository : Repository<VolunteerApplicationHistory>, IVolunteerApplicationHistoryRepository
    {
        public VolunteerApplicationHistoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
