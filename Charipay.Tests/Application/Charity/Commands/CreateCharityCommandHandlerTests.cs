using AutoMapper;
using Charipay.Application.Commands.Charities;
using Charipay.Application.DTOs.Charities;
using Charipay.Application.Interfaces.Common;
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

namespace Charipay.Tests.Application.Charity.Commands
{
    public class CreateCharityCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> mockUnitOfWork = new();
        private readonly Mock<ILogger<CreateCharityCommandHandler>> mockLogger = new();
        private readonly Mock<IMapper> mockMapper = new();
        private readonly Mock<ICurrentUserService> mockCurrentUser  = new();
        private readonly Mock<ICharityRepository> mockCharityRepository = new();
        private readonly CreateCharityCommandHandler _handler;


        public CreateCharityCommandHandlerTests()
        {
            _handler = new CreateCharityCommandHandler
                (
                  mockUnitOfWork.Object, 
                  mockLogger.Object, 
                  mockMapper.Object, 
                  mockCurrentUser.Object,
                  mockCharityRepository.Object
                );
        }


        [Fact]
        public async Task Handler_Should_Return_FailResponse_When_Charity_Email_Already_Exists()
        {
            //arrange
            var command =  CreateValidCharityCommand();

            mockCharityRepository.Setup(c => c
            .GetCharityByContactEmailAsync(command.ContactEmail, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            //act
            var result = await _handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be($"Charity already exists with this Email:{command.ContactEmail}");

            mockCharityRepository.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.Charity>()), Times.Never);
            mockUnitOfWork.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handler_Should_Return_False_When_Charity_Registration_Already_Exists()
        {  
            //arrange
            var command = CreateValidCharityCommand();

            mockCharityRepository.Setup (c => c
            .GetCharityByRegistrationNumberAsync(command.RegistrationNumber, CancellationToken.None))
                .ReturnsAsync(true);

            //act
            var result = await _handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be($"Charity already exists with this Registration number:{command.RegistrationNumber}");
            

        }

        //[Fact]
        //public async Task Handler_Should_Create_Chatiry_When_Request_Is_Valid()
        //{
        //    //arrange
        //    var userId = Guid.NewGuid();
        //    var command = CreateValidCharityCommand();

        //    var charity = new Domain.Entities.Charity
        //    {
        //        CharityId = Guid.NewGuid(),
        //        Name = command.Name,
        //        RegistrationNumber = command.RegistrationNumber,
        //        Description = command.Description,
        //        Website = command.Website,
        //        ContactEmail = command.ContactEmail,
        //        CreatedByUserId = Guid.NewGuid(),
        //        CreatedAt = DateTime.UtcNow,
        //        IsApproved = true
        //    };

        //    var charityDto = new CharityDto
        //    {
        //        CharityId = Guid.NewGuid(),
        //        Name = command.Name,
        //        RegistrationNumber = command.RegistrationNumber,
        //        Description = command.Description,
        //        Website = command.Website,
        //        ContactEmail = command.ContactEmail,
        //        CreatedByUserId = Guid.NewGuid(),
        //        CreatedAt = DateTime.UtcNow,
        //        IsApproved = true
        //    };


        //    mockCharityRepository.Setup(x => x
        //    .GetCharityByContactEmailAsync(command.ContactEmail, It.IsAny<CancellationToken>())).ReturnsAsync(false);


        //    mockCharityRepository.Setup(x => x
        //   .GetCharityByRegistrationNumberAsync(command.RegistrationNumber, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        //    // mockMapper.Setup(x => x.Map<Domain.Entities.Charity>(command)).Returns(charity);



        //    mockCurrentUser.Setup(x=>x.UserId).Returns(userId);

        //    mockCharityRepository.Setup(x => x
        //    .AddAsync(It.IsAny<Domain.Entities.Charity>())
        //    ).ReturnsAsync(charity);

        //    mockUnitOfWork.Setup(x =>
        //    x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            

        //    mockMapper.Setup(x => x
        //    .Map<CharityDto>(command)).Returns(charityDto);


        //    //act
        //    var result = await _handler.Handle(command, CancellationToken.None);

        //    //assert
        //    result.Success.Should().BeTrue();
        //    result.Message.Should().Contain("Charity Created Successfully.");
        //    result.Data.Should().NotBeNull();
        //    result.Data!.Name.Should().Be(command.Name);
        //    result.Data!.RegistrationNumber.Should().Be(command.RegistrationNumber);


        //    mockCharityRepository.Verify(x=>x
        //    .AddAsync(charity), Times.Once
        //    );

        //    mockUnitOfWork.Verify(x => x
        //  .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once
        //  );
        //}



        private static CreateCharityCommand CreateValidCharityCommand ()
        {
            return new CreateCharityCommand
            {
                Name = "Test Charity",
                RegistrationNumber = "002990223",
                Description = "This is charity test description",
                Website = "www.testcharity.com",
                ContactEmail = "contact.testcharity@gmail.com",
                IsApproved = true
                
            };
        }






    }
}
