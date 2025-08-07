using AutoMapper;
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
    public record class RegisterUserCommand (
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string ProfileImageUrl,
        DateTime CreatedAt,
        string AddressLine1,
        string PostCode,
        DateTime? DOB

        ) : IRequest<User>;

    public class RegisterUserCommandHandler(IUnitOfWork _unitOfWork, IPasswordHasher _passwordHasher) : IRequestHandler<RegisterUserCommand, User>
    {
        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists!");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                DOB = request.DOB,
                CreatedAt = DateTime.Now
                ,AddressLine1 = request.AddressLine1
                ,PostCode = request.PostCode,
                ProfileImageUrl = request.ProfileImageUrl,
                PhoneNumber = request.PhoneNumber
            };


            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return await _unitOfWork.Users.GetByEmailAsync(request.Email);

        }
    }
}
