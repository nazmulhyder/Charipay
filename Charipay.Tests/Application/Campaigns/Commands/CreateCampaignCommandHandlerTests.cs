using AutoMapper;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using FluentAssertions;
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
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ICurrentUserService> _currentUserMock;
        private readonly Mock<ICampaignRepository> _campaignRepositoryMock;

        private readonly CreateCampaignCommandHandler _handler;

        public CreateCampaignCommandHandlerTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _currentUserMock = new Mock<ICurrentUserService>();
            _campaignRepositoryMock = new Mock<ICampaignRepository>();

            _handler = new CreateCampaignCommandHandler(
                _unitOfWork.Object,
                _mapperMock.Object,
                _currentUserMock.Object,
                _campaignRepositoryMock.Object
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
            result.Message.Should().Be("Campain already exists with this Charity");
            result.Errors.Should().Contain("Campain already exists with this Charity");

            _campaignRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Campaign>()), Times.Never);
            _unitOfWork.Verify(x=>x.SaveChangesAsync(CancellationToken.None), Times.Never);
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
