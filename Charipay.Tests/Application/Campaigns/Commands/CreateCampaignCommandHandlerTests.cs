using AutoMapper;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.DTOs.Campaigns;
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

namespace Charipay.Tests.Application.Campaigns.Commands
{
    public class CreateCampaignCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<ILogger<CreateCampaignCommandHandler>> _logger = new();
        private readonly Mock<ICurrentUserService> _currentUserMock = new();
        private readonly Mock<ICampaignRepository> _campaignRepositoryMock = new();
        private readonly Mock<ICharityRepository> _charityRepositoryMock = new();

        private readonly CreateCampaignCommandHandler _handler;

        public CreateCampaignCommandHandlerTests()
        {
            _handler = new CreateCampaignCommandHandler(
              _unitOfWork.Object,
              _mapperMock.Object,
              _currentUserMock.Object,
              _campaignRepositoryMock.Object,
              _logger.Object,
              _charityRepositoryMock.Object
      );

        }

        [Fact]
        public async Task Handle_Should_Return_FailedResponse_When_Campaign_Already_Exists()
        {
            //arrange
            var command = CreateValidCommand();
            _campaignRepositoryMock.Setup(x => x.GetCampaignByCharityId(command.CharityId, command.CampaignName,  It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //act

            var result = await _handler.Handle(command, CancellationToken.None);

            //assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Campaign already exists with this Charity");
            result.Errors.Should().Contain("Campaign already exists with this Charity");

            _campaignRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Campaign>()), Times.Never);
            _unitOfWork.Verify(x=>x.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Create_Campaign_When_Campaign_Does_Not_Exist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var command = CreateValidCommand();

            var campaign = new Campaign
            {
                CampaignId = Guid.NewGuid(),
                CampaignName = command.CampaignName,
                CampaignDescription = command.CampaignDescription,
                GoalAmount = command.GoalAmount,
                CurrentAmount = command.CurrentAmount,
                CampaignStartDate = command.CampaignStartDate,
                CampaignEndDate = command.CampaignEndDate,
                CharityId = command.CharityId,
                CreatedById = userId,
                IsActive = true,
                IsFeatured = false,
                CurrencyCode = "GBP"
            };

            var campaignDto = new CampaignDto
            {
                CampaignId = campaign.CampaignId,
                CampaignName = campaign.CampaignName,
                CampaignDescription = campaign.CampaignDescription,
                GoalAmount = campaign.GoalAmount,
                CurrentAmount = campaign.CurrentAmount,
                CampaignStartDate = campaign.CampaignStartDate,
                CampaignEndDate = campaign.CampaignEndDate,
                CharityId = campaign.CharityId,
                IsActive = campaign.IsActive,
                IsFeatured = campaign.IsFeatured,
                CurrencyCode = campaign.CurrencyCode
            };

            _campaignRepositoryMock
                .Setup(x => x.GetCampaignByCharityId(
                    command.CharityId,
                    command.CampaignName,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _currentUserMock
                .Setup(x => x.UserId)
                .Returns(userId);

            _mapperMock
                .Setup(x => x.Map<Campaign>(command))
                .Returns(campaign);

            _campaignRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Campaign>()))
                .ReturnsAsync(campaign);

            _unitOfWork
                .Setup(x => x.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(1);

            _mapperMock
                .Setup(x => x.Map<CampaignDto>(campaign))
                .Returns(campaignDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Campaign saved successfully!");
            result.Data.Should().NotBeNull();
            result.Data!.CampaignName.Should().Be(command.CampaignName);
            result.Data.CharityId.Should().Be(command.CharityId);

            command.CreatedById.Should().Be(userId);

            _campaignRepositoryMock.Verify(x => x.AddAsync(campaign), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        private static CreateCampaignCommand CreateValidCommand()
        {
            return new CreateCampaignCommand
            {
                CampaignName = "Food Support Campaign",
                CampaignDescription = "Helping families with food support.",
                GoalAmount = 5000,
                CurrentAmount = 0,
                CampaignStartDate = DateTime.UtcNow,
                CampaignEndDate = DateTime.UtcNow.AddDays(30),
                ImageUrl = "https://example.com/image.jpg",
                CharityId = Guid.NewGuid(),
                IsFeatured = false,
                IsActive = true,
                CurrencyCode = "GBP"
            };
        }
    }
}
