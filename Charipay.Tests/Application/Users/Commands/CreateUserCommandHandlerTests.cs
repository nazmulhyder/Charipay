using AutoMapper;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Users;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;

namespace Charipay.Tests.Application.Users.Commands
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<ILogger<CreateUserCommandHandler>> _loggerMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock = new();


        private CreateUserCommandHandler CreateHandler()
        {
            return new CreateUserCommandHandler(
                _unitOfWorkMock.Object,
                _passwordHasherMock.Object,
                _loggerMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object,
                _userRoleRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnFailedResponse_WhenEmailAlreadyExists()
        {
            // Arrange
            var command = CreateValidCommand();
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync(new User());

            var handler = CreateHandler();


            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("User already exists");

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
            _userRoleRepositoryMock.Verify(x=>x.AddAsync(It.IsAny<UserRole>()), Times.Never);
            _unitOfWorkMock.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

        }

        [Theory]
        [InlineData(2)] // donor
        [InlineData(3)] // volunteer

        public async Task Handle_ShouldCreateUser_WhenRequestIsValid(int roleId)
        {
            // arrange
            var command = CreateValidCommand();
            command.RoleID = roleId;
            var user = new User { UserID = Guid.NewGuid() , Email = command.Email};
            var userDto = new UserDto {  Email = command.Email };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?) null); // user does not exist

            _mapperMock.Setup(x => x.Map<User>(command))
                .Returns(user);

            _mapperMock.Setup(x=>x.Map<UserDto>(user)).Returns(userDto);

            var handler = CreateHandler();

            //act

            var result = await handler.Handle(command, CancellationToken.None);

            //assert

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Email.Should().Be(command.Email);

            _userRepositoryMock.Verify(x => x.AddAsync(It.Is<User>(u => u.Email == command.Email)), Times.Once);

            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.Is<UserRole>(ur => ur.UserID == user.UserID && ur.RoleID == command.RoleID)), Times.Once);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));


        }

        [Fact]
        public async Task Handle_ShouldCreateFailedResponse_WhenAdminRoleSelected()
        {
            // arrange
            var command = new CreateUserCommand();
            command.RoleID = 1; // admin
            var handler = CreateHandler();

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Invalid role selected.");

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserRole>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);

        }



        [Theory]
        [InlineData(999)]
        [InlineData(212)]
        public async Task Handle_ShouldReturnFailedResponse_WhenRoleIsInvalid(int roleId) 
        {
            //arrange
            var command = new CreateUserCommand();
            command.RoleID = roleId;
            var handler = CreateHandler();


            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Invalid role selected.");

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserRole>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact]
        public async Task Handle_ShouldHashPassword_WhenCreatingUser()
        {
            //arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid() };

            SetupUserDoesNotExistAndMapping(command, user);

            var handler = CreateHandler();

            //act
            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            _passwordHasherMock.Verify(x => x.Hash(command.Password), Times.Once);

        }

        [Fact]
        public async Task Handle_ShouldSaveChanges_WhenUserCreated()
        {
            // arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid(), Email = command.Email };

            SetupUserDoesNotExistAndMapping(command, user);

            var handler = CreateHandler();

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.Success.Should().BeTrue();
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Handle_ShouldMapCommandToUser()
        {
            // arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid(), Email = command.Email };

            SetupUserDoesNotExistAndMapping(command, user);

            var handler = CreateHandler();

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.Success.Should().BeTrue();
            _mapperMock.Verify(x => x.Map<User>(It.Is<CreateUserCommand>(c => c == command)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldMapUserToUserDto()
        {
            // arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid(), Email = command.Email };

            SetupUserDoesNotExistAndMapping(command, user);

            var handler = CreateHandler();

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            result.Success.Should().BeTrue();
            _mapperMock.Verify(x => x.Map<UserDto>(It.Is<User>(u => u == user)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldAssignRequestedRole_WhenUserCreated()
        {
            //arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid() };

            SetupUserDoesNotExistAndMapping(command, user);

            var handler = CreateHandler();

            //act

            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.Is<UserRole>(c => c.UserID == user.UserID
             && c.RoleID == command.RoleID)), Times.Once);

        }



        private static CreateUserCommand CreateValidCommand()
        {
            return new CreateUserCommand
            {

                FullName = "Test User",
                Email = "test@example.com",
                Password = "Password123!",
                Phone = "07123456789",
                AddressLine1 = "London",
                PostCode = "E1 1AA",
                DOB = new DateTime(1995, 1, 1),
                RoleID = 2 
            };
        }

        private void SetupUserDoesNotExistAndMapping(CreateUserCommand command, User user)
        {
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            _mapperMock.Setup(x => x.Map<User>(command)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(new UserDto());
        }
    }
}
