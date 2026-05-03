using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x=>x.FullName).NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email).NotEmpty()
               .EmailAddress();

            RuleFor(x => x.Password).NotEmpty()
               .MinimumLength(6);

            RuleFor(x => x.Phone).NotEmpty();

            RuleFor(x => x.RoleID).NotEmpty()
               .GreaterThan(0);

            RuleFor(x => x.FullName)
            .NotEmpty()
            .Matches(@"^[a-zA-Z\s]+$")
            .WithMessage("Name should not contain special characters");

            RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^07\d{9}$")
            .WithMessage("Phone number must be a valid UK mobile number");
        }
    }
}
