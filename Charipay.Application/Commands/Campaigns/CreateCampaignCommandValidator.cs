using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(c => c.CampaignName)
                .NotEmpty().WithMessage("Campaign name is required")
                .MaximumLength(100).WithMessage("Campaign name cannot exceed 100 characters.");

            RuleFor(c => c.CampaignDescription)
               .NotEmpty().WithMessage("Description is required.");

            RuleFor(c => c.GoalAmount)
              .GreaterThanOrEqualTo(100).WithMessage("Goal Amount must be at least 100");

            RuleFor(c => c.CampaignStartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past.");

            RuleFor(c => c.CampaignEndDate)
                .GreaterThan(c => c.CampaignStartDate).WithMessage("End date must be greater than start date.");

            RuleFor(c => c.CharityId)
                .NotEmpty().WithMessage("Please select charity for the Campaign.");


        }
    }
}
