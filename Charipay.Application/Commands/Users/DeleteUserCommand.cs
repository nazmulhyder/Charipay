using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public record DeleteUserCommand(Guid UserId) : IRequest<string>;
   
 
}
