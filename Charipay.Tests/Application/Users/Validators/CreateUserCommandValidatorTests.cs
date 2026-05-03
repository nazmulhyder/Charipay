using Charipay.Application.Commands.Users;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Tests.Application.Users.Validators
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator = new();

        [Fact]
        public void Should_Fail_When_Email_Is_Empty()
        {
            // arrange
            var command = CreateValidCommand();
            command.Email = "" ;
            
            //act
            var result = _validator.Validate(command);
            
            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_Email_Is_InValid()
        {
            // arrange
            var command = CreateValidCommand();
            command.Email = "1234#";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        
        }

        [Fact]
        public void Should_Fail_When_Password_Is_Empty()
        {
            // arrange
            var command = CreateValidCommand();
            command.Password = "";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Pass_When_Password_Is_Valid()
        {
            // arrange

            var command = CreateValidCommand();
            command.Password = "!Naul32432";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Fail_When_Phone_Is_Empty()
        {
            // arrange
            var command = CreateValidCommand();
            command.Phone = "";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Should_Fail_When_Phone_Is_In_Valid()
        {
            // arrange
            var command = CreateValidCommand();
            command.Phone = "jlds93";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_Full_Name_Is_Empty()
        {
            // arrange
            var command = new CreateUserCommand { FullName = "" };

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Fail_When_Full_Name_Is_In_Valid()
        {
            // arrange
            var command = new CreateUserCommand { FullName = "N@zmul" };

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Should_Pass_When_Full_Name_Is_Valid()
        {
            // arrange
            var command = CreateValidCommand();
            command.FullName = "Nazmul Hyder";

            //act
            var result = _validator.Validate(command);

            //assert
            result.IsValid.Should().BeTrue();
        }


        private static CreateUserCommand CreateValidCommand()
        {
            return new CreateUserCommand
            {
                FullName = "Nazmul Hyder",
                Email = "test@example.com",
                Password = "Password123!",
                Phone = "07123456789",
                RoleID = 1
            };
        }
    }


}
