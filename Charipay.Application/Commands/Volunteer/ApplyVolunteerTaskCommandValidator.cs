using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class ApplyVolunteerTaskCommandValidator : AbstractValidator<ApplyVolunteerTaskCommand>
    {
        public ApplyVolunteerTaskCommandValidator()
        {
            RuleFor(x => x.VolunteerTaskId)
                .NotEmpty().WithMessage("Volunteer task id is required.");

            RuleFor(x => x.VolunteerMessage)
                .MaximumLength(500).WithMessage("Volunteer message must not exceed 500 characters.");

            RuleFor(x => x.AvailabilityNote)
                .MaximumLength(500).WithMessage("Availability note must not exceed 500 characters.");
        }
    }
}
