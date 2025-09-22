using AutoMapper;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Users;
using Charipay.Application.Common.Models;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Charipay.Tests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepo;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<CreateUserCommandHandler>> _mockLogger;

        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUserRepo = new Mock<IUserRepository>();
            _mockUserRoleRepo = new Mock<IUserRoleRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<CreateUserCommandHandler>>();

            // Setup UnitOfWork to return mocked repositories
            _mockUnitOfWork.Setup(u => u.Users).Returns(_mockUserRepo.Object);
            _mockUnitOfWork.Setup(u => u.UserRoles).Returns(_mockUserRoleRepo.Object);

            _handler = new CreateUserCommandHandler(
                _mockUnitOfWork.Object,
                _mockPasswordHasher.Object,
                _mockLogger.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenUserAlreadyExists()
        {
            // Arrange: setup the request
            var request = new CreateUserCommand
            {
                FullName = "Nazmul Hyder",
                Email = "nazmul@gmail.com",
                Password = "Nazmul496",
                RoleID = 1
            };

            // Arrange: mock existing user
            _mockUserRepo.Setup(r => r.GetByEmailAsync(request.Email))
                         .ReturnsAsync(new User { Email = request.Email });

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: ensure the handler returns error
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User already exists with this email!");
            result.Errors.Should().ContainSingle().Which.Should().Be("User already exists!");

            // Assert: ensure no database operations were performed
            _mockUserRepo.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Never);
            _mockUserRoleRepo.Verify(r => r.AddAsync(It.IsAny<UserRole>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Never);
        }


        [Fact]
        public async Task Handle_Should_CreateUser_WhenUserDoesNotExists()
        {
            // Arrange
            var request = new CreateUserCommand
            {
                FullName = "Nazmul Hyder",
                Email = "nazmul@gmail.com",
                Password = "Nazmul496",
                RoleID = 1
            };

            var newUser = new User { UserID = Guid.NewGuid(), Email = request.Email };
            var userDto = new UserDto {  UserId = Guid.NewGuid(),Email = newUser.Email };

            //mock no existing user
            _mockUnitOfWork.Setup(u => u.Users.GetByEmailAsync(request.Email)).ReturnsAsync((User) null);
            _mockPasswordHasher.Setup(c => c.Hash(request.Password)).Returns("HashedPassword");

            _mockMapper.Setup(c=>c.Map<User>(request)).Returns(newUser);
            _mockMapper.Setup(c=>c.Map<UserDto>(newUser)).Returns(userDto);

            // Act
            var result = await _handler.Handle(request, cancellationToken: CancellationToken.None);


            //Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("User created successfully!");
            result.Data.Should().BeEquivalentTo(userDto);

            _mockUnitOfWork.Verify(c => c.Users.AddAsync(It.IsAny<User>()), times: Times.Once);
            _mockUnitOfWork.Verify(c => c.UserRoles.AddAsync(It.IsAny<UserRole>()), Times.Once);
            _mockUnitOfWork.Verify(c=>c.SaveChangesAsync(), times: Times.Exactly(2));
            _mockPasswordHasher.Verify(c => c.Hash("Nazmul496"), times: Times.Once);

        }


        //[Fact]
        //public async Task Handle_Should_HashPassword_WhenCreatingUser()
        //{
        //    throw new NotImplementedException();
        //}

        //[Fact]
        //public async Task Handle_Should_AssignRole_WhenUserCreated()
        //{
        //    throw new NotImplementedException();
        //}


        //[Fact]
        //public async Task Handle_Should_LogInformation_WhenUserCreated()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
