using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands
{
    public record class RegisterUserCommand (User User) : IRequest<User>;

    public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, User>
    {
        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await userRepository.AddAsync(request.User);
        }
    }
}
