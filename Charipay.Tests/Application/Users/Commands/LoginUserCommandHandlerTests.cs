using Charipay.Application.Commands.Users;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Charipay.Tests.Application.Users.Commands
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();
        private readonly Mock<IPasswordHasher> _mockIPasswordHasher = new();
        private readonly Mock<IJwtTokenService> _mockJwtTokenService = new();
        private readonly Mock<IUserRepository> _mockUserRepository = new();
        private readonly Mock<ILogger<LoginUserCommandHandler>> _mockLogging = new();


        public LoginUserCommandHandler CreateHandler()
        {
            return new LoginUserCommandHandler
                (
                  _mockUnitOfWork.Object,
                  _mockIPasswordHasher.Object,
                  _mockLogging.Object,
                  _mockJwtTokenService.Object,
                  _mockUserRepository.Object
                );
        }


        [Fact]
        public void Handle_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            // arrange
            var email = "test@login.com";
            var password = "Password123!";

            var user = new User
            {
                UserID = Guid.NewGuid(),
                Email = email,
                PasswordHash = "hashed",
                FullName = "Test User",
                ProfileImageUrl = "http://example.com/img.png",
                UserRoles = new List<UserRole>
                {
                    new UserRole { Role = new Role { Name = "donor" } }
                }
            };

            _mockUserRepository.Setup(x => x.GetByEmailAsync(email)).ReturnsAsync(user);
            _mockIPasswordHasher.Setup(x => x.Verify(user.PasswordHash, password)).Returns(true);
            _mockJwtTokenService.Setup(x => x.GenerateToken(user, It.IsAny<IEnumerable<string>>())).Returns("token123");

            var handler = CreateHandler();

            // act
            var result = handler.Handle(new LoginUserCommand(email, password), default);

            // assert
            result.Should().NotBeNull();
            result.IsCompletedSuccessfully.Should().BeTrue();

            var response = result.Result;
            response.Success.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Token.Should().Be("token123");
            _mockJwtTokenService.Verify(x => x.GenerateToken(user, It.Is<IEnumerable<string>>(r => r.Contains("donor"))), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldReturnFailed_WhenUserDoesNotExist()
        {
            // arrange
            var command = new LoginUserCommand(Email: "abc@gmail.com", Password: "Abc123");

            _mockUserRepository.Setup(x => x.GetByEmailAsync(command.Email)).ReturnsAsync((User?)null);

            var handler = CreateHandler();

            // act
            var response = await handler.Handle(command, CancellationToken.None);

            // assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Invalid User Email!");
            response.Errors.Should().Contain("User not found!");

            _mockIPasswordHasher.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockJwtTokenService.Verify(x => x.GenerateToken(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }



        [Fact]
        public async Task Handle_ShouldReturnFailed_WhenPasswordIsInvalid()
        {
            // arrange
            var passwordHash = "password-hash";
            var command = new LoginUserCommand(Email: "abc@gmail.com", Password: "Abc123");

            var user = new User
            {
                UserID = Guid.NewGuid(),
                Email = command.Email,
                PasswordHash = passwordHash,
                FullName = "Test User",
                UserRoles = new List<UserRole>() // roles not necessary for this test but kept for completeness
            };

            _mockUserRepository.Setup(x => x.GetByEmailAsync(command.Email)).ReturnsAsync(user);
            _mockIPasswordHasher.Setup(x => x.Verify(passwordHash, command.Password)).Returns(false);

            var handler = CreateHandler();

            // act
            var response = await handler.Handle(command, CancellationToken.None);

            // assert
            response.Should().NotBeNull();
            response.Success.Should().BeFalse();
            response.Message.Should().Be("Invalid User Password!");
            response.Data.Should().BeNull();
            response.Errors.Should().Contain("User not found!");

            _mockIPasswordHasher.Verify(x => x.Verify(passwordHash, command.Password), Times.Once);
            _mockJwtTokenService.Verify(x => x.GenerateToken(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Never);
        }

    }
}
