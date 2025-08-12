using Charipay.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Users
{
    public class GetAllUserQuery : IRequest<List<UserDto>>
    {
    }
}
