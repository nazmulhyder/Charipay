using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.Dashboard.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Admin.Dashboard.Users
{
    public class GetUserListQuery : IRequest<ApiResponse<List<UserDto>>>
    {
    }
}
