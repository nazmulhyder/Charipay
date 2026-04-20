using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.QueryService
{
    public interface IAdminVolunteerRequestQueryService
    {
        public Task<(List<AdminVolunteerRequestDto> Items, int TotalCount)> GetVolunteerApplicationRequestsAsync(int pageNumber, int pageSize, string? search, string? status);
    }
}
