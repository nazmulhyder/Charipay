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
    public class UserFeedbackRepository : Repository<UserFeedback>, IUserFeedbackRepository
    {
        public UserFeedbackRepository(AppDbContext context) : base(context)
        {
        }
    }
}
