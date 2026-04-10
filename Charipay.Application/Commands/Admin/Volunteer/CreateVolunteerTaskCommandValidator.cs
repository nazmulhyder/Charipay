using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class CreateVolunteerTaskCommandValidator : AbstractValidator<CreateVolunteerTaskCommand>
    {
        public CreateVolunteerTaskCommandValidator()
        {
            RuleFor(x => x.CampaignId)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.MaxVolunteer)
                .GreaterThan(0);

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be earlier than end date.");
        }
    }
}
