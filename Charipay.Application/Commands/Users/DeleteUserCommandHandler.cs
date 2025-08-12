using AutoMapper;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class DeleteUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteUserCommand, string>
    {
        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.Users.GetByIdAsync(request.UserId);

            if (user == null) {
                throw new Exception("User not found!");
            }

           unitOfWork.Users.Remove(user);
           await  unitOfWork.SaveChangesAsync();
           
           return "User deleted successfully.";

        }
    }
}
