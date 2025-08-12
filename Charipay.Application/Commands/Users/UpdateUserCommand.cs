using Charipay.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public record UpdateUserCommand
    (
        Guid UserId,
        string FullName,
        string Email,     
        string Phone,
        string ProfileImageUrl,
        DateTime CreatedAt,
        string AddressLine1,
        string PostCode,
        DateTime DOB

    ) : IRequest<UserDto>;
}
