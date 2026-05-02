using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class CreateVolunteerTaskCommandHandler : IRequestHandler<CreateVolunteerTaskCommand, ApiResponse<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly ICampaignRepository _campaignRepository;
        private readonly IVolunteerTaskRepository _volunteerTask;
        public CreateVolunteerTaskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser, ICampaignRepository campaignRepository,
             IVolunteerTaskRepository volunteerTask)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
            _campaignRepository = campaignRepository;
            _volunteerTask = volunteerTask;
        }

        public async Task<ApiResponse<Guid>> Handle(CreateVolunteerTaskCommand request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (campaign == null)
            {
                return ApiResponse<Guid>.FailedResponse("Campaign not found");
            }

            var volunteerTask = new VolunteerTask
            {
                VolunteerTaskId = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MaxVolunteer = request.MaxVolunteer,
                CampaignId = request.CampaignId,
                IsActive = true,
                VolunteerUsers = new List<VolunteerUser>()
            };

            await _volunteerTask.AddAsync(volunteerTask);
            await _unitOfWork.SaveChangesAsync();


            return ApiResponse<Guid>.SuccessResponse(volunteerTask.VolunteerTaskId, "Task Saved successfully!");
        }
    }
}
