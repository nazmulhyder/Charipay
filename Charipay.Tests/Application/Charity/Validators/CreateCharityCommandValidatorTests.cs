using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Commands.Charities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Tests.Application.Charity.Validators
{
    public class CreateCharityCommandValidatorTests
    {

        private readonly CreateCharityCommandValidator validator = new();

        public static CreateCharityCommand CreateValidCharityCommand()
        {
            return new CreateCharityCommand()
            {
                Name = "Shelter Aid Network",
                RegistrationNumber = "1165821",
                ContactEmail = "help@shelteraid.org.uk",
                Website = "www.google.com",
                

            };
        }

        [Fact]
        public void Should_Return_False_When_Name_Is_Empty() { 
           
            //arrange
            var command = CreateValidCharityCommand();
            command.Name = string.Empty;


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Charity name is required.");
        
        }


        [Fact]
        public void Should_Return_False_When_Name_Exceed_100_characters()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.Name = new string('A', 101);


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Charity name cannot exceed 100 characters.");

        }


        [Fact]
        public void Should_Return_False_When_RegistrationNumber_Is_Empty()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.RegistrationNumber = string.Empty;


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Registration number is required.");
        }

        [Fact]
        public void Should_Return_False_When_Description_Is_Empty()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.Description = string.Empty;


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Description is required.");

        }


        [Fact]
        public void Should_Return_False_When_ContactEmail_Is_Empty()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.ContactEmail = string.Empty;


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Contact Email is required.");

        }


        [Fact]
        public void Should_Return_False_When_Website_Is_Empty()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.Website = string.Empty;


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Website is required.");

        }


        [Fact]
        public void Should_Return_False_When_Website_Is_Invalid()
        {

            //arrange
            var command = CreateValidCharityCommand();
            command.Website = "abc123";


            //act
            var result = validator.Validate(command);


            //assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(x => x.ErrorMessage == "Invalid website url.");

        }


    }
}
