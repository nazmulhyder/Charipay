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
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<List<UserDto>>>
    {
        public GetUserListQueryHandler()
        {
            
        }
        public Task<ApiResponse<List<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
