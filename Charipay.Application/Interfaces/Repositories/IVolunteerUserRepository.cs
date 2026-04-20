using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
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
        Task<bool> HasUserAlreadyAppliedAsync(Guid userId, Guid? volunteerTaskId);
        Task<int> GetActiveApplicationCountAsync(Guid volunteerTaskId);
        Task<(List<MyVolunteerApplicationDto> Items, int TotalCount)>  GetMyApplicationsAsync(Guid UserId,int pageNumber, int pageSize, string? search = null);
        Task<VolunteerUser?> GetByIdAndUserIdAsync(Guid volunteerUserId, Guid userId);
    }
}
