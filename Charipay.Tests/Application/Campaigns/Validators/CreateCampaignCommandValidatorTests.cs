using Charipay.Application.Commands.Campaigns;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Tests.Application.Campaigns.Validators
{
    public class CreateCampaignCommandValidatorTests
    {
        private readonly CreateCampaignCommandValidator _validator = new();

        [Fact]
        public void Should_Fail_When_CampaignName_Is_Empty()
        {
            // arrange
            var command = CreateValidCampaignCommand();
            command.CampaignName = "";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_CampaignDescription_Is_Empty()
        {
            // arrange
            var command = CreateValidCampaignCommand();
            command.CampaignDescription = "";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_GoalAmount_Is_Zero()
        {
            // arrange
            var command = CreateValidCampaignCommand();
            command.GoalAmount = 0;

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();

            result.Errors
           .Should()
           .Contain(x => x.ErrorMessage == "Goal Amount must be at least 100");
        }


        [Fact]
        public void Should_Fail_When_StartDate_Is_In_Past()
        {
            // arrange
            var command = CreateValidCampaignCommand();
            command.CampaignStartDate = DateTime.UtcNow.Date.AddDays(-1);

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();

            result.Errors
          .Should()
          .Contain(x => x.ErrorMessage == "Start date cannot be in the past.");
        }





        private static CreateCampaignCommand CreateValidCampaignCommand()
        {
            return new CreateCampaignCommand
            {
                CampaignName = "Winter Shelter Support",
                CampaignDescription = "Help us provide safe, temporary shelter for vulnerable individuals during the harsh winter months. Your support ensures access to warmth, basic necessities, and a place to rest with dignity.",
                GoalAmount = 1000,
                CurrentAmount = 10,
                CampaignStartDate = DateTime.UtcNow.Date.AddDays(1),
                CampaignEndDate = DateTime.UtcNow.Date.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                CharityId = Guid.NewGuid(),
            };
        }

    
    } 

    
}
