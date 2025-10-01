using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class CreateCharityCommandValidator : AbstractValidator<CreateCharityCommand>
    {
        public CreateCharityCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Charity name is required.")
                .MaximumLength(100).WithMessage("Charity name cannot exceed 100 characters.");

            RuleFor(c => c.RegistrationNumber)
                .NotEmpty().WithMessage("Registration number is required.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(c => c.ContactEmail)
                .NotEmpty().WithMessage("Contact Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

        }
    }
}
