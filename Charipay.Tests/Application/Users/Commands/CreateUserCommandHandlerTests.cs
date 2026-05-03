using AutoMapper;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Commands.Users;
using Charipay.Application.DTOs.Users;
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
        public async Task Handle_ShouldReturnFailed_WhenUserAlreadyExists()
        {
            // Arrange
            var command = CreateValidCommand();
            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync(new Domain.Entities.User());

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

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenRequestIsValid()
        {
            // arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid() , Email = command.Email};
            var userDto = new UserDto {  Email = command.Email };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?) null); // user does not exist

            _mapperMock.Setup(x => x.Map<User>(command))
                .Returns(user);

            var check = _passwordHasherMock.Setup(x => x.Hash(command.Password));
            _passwordHasherMock.Setup(x => x.Hash(command.Password))
                .Returns("hashed-password");

            _mapperMock.Setup(x=>x.Map<UserDto>(user)).Returns(userDto);

            var handler = CreateHandler();

            //act

            var result = await handler.Handle(command, CancellationToken.None);

            //assert

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Email.Should().Be(command.Email);
            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserRole>()), Times.Once);

            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));


        }

        [Fact]

        public async Task Handle_ShouldHashPassword_WhenCreatingUser()
        {
            //arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid() };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);

            _mapperMock.Setup(x=>x.Map<User>(command)).Returns(user);
            _mapperMock.Setup(x=>x.Map<UserDto>(user)).Returns(new UserDto());

            var handler = CreateHandler();

            //act
            var result = await handler.Handle(command, CancellationToken.None);


            //assert
            _passwordHasherMock.Verify(x => x.Hash(command.Password), Times.Once);

        }

        [Fact]
        public async Task Handle_ShouldAssignRequestedRole_WhenUserCreated()
        {
            //arrange
            var command = CreateValidCommand();
            var user = new User { UserID = Guid.NewGuid() };

            _userRepositoryMock.Setup(x=>x.GetByEmailAsync(command.Email))
                .ReturnsAsync((User?)null);
            
            _mapperMock.Setup(x=> x.Map<User>(command)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(new UserDto());

            var handler = CreateHandler();

            //act

            var result = await handler.Handle(command, CancellationToken.None);

            //assert
            _userRoleRepositoryMock.Verify(x => x.AddAsync(It.Is<UserRole>(c => c.UserID == user.UserID
             && c.RoleID == command.RoleID)), Times.Once);



        }

        [Fact]
        public async Task Handle_ShouldCallSaveChangeTwice_WhenUserCreated()
        {
            //arrange
            var command = CreateValidCommand();
            var user = new User {  UserID = Guid.NewGuid() };

            _userRepositoryMock.Setup(c=>c.GetByEmailAsync(command.Email)).ReturnsAsync((User?)null);

            _mapperMock.Setup(x => x.Map<User>(command)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(new UserDto());

            var handler = CreateHandler();

            //act
            var result = handler.Handle(command, CancellationToken.None);

            // assert
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));



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
                RoleID = 1 
            };
        }
    }
}
