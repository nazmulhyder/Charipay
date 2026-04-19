using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class RequestVolunteerApplicationCancellationCommandValidator : AbstractValidator<RequestVolunteerApplicationCancellationCommand>
    {
        public RequestVolunteerApplicationCancellationCommandValidator()
        {
            RuleFor(x => x.VolunteerUserId)
                .NotEmpty()
                .WithMessage("Application id is required.");

            RuleFor(x => x.VolunteerMessage)
                .MaximumLength(500)
                .WithMessage("Cancellation message must not exceed 500 characters.");
        }
    }
}
