using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class ApplyVolunteerTaskCommandHandler : IRequestHandler<ApplyVolunteerTaskCommand, ApiResponse<VolunteerUserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IVolunteerTaskRepository _volunteerTaskRepository;
        private readonly IVolunteerUserRepository volunteerUser;

        public ApplyVolunteerTaskCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IVolunteerTaskRepository volunteerTaskRepository
            , IVolunteerUserRepository _volunteerUser)
        {
            _mapper = mapper;
            _unitOfWork =  unitOfWork;  
            _currentUserService = currentUserService;
            _volunteerTaskRepository = volunteerTaskRepository;
            volunteerUser = _volunteerUser;
        }

        public async Task<ApiResponse<VolunteerUserDto>> Handle(ApplyVolunteerTaskCommand request, CancellationToken cancellationToken)
        {
           

            var task = await _volunteerTaskRepository.GetByIdAsync(request.VolunteerTaskId, cancellationToken);

            if (task == null)
                return ApiResponse<VolunteerUserDto>.FailedResponse("Task does not exists!", null);

            if (!task.IsActive)
                return ApiResponse<VolunteerUserDto>.FailedResponse("This volunteer task is no longer active!", null);


            var volunteerTaskExists = await volunteerUser.HasUserAlreadyAppliedAsync(request.VolunteerTaskId, _currentUserService.UserId);
            if (volunteerTaskExists)
               return ApiResponse<VolunteerUserDto>.FailedResponse("User already applied for this task!", null);

            var activeApplicantCount = await volunteerUser.GetActiveApplicationCountAsync(request.VolunteerTaskId);

            if(activeApplicantCount >= task.MaxVolunteer)
                return ApiResponse<VolunteerUserDto>.FailedResponse("Volunteer reached the maximum level for this task!", null);

            var application = new VolunteerUser
            {
                VolunteerUserId = Guid.NewGuid(),
                UserId = this._currentUserService.UserId.Value,
                VolunteerTaskId = request.VolunteerTaskId,
                SignupDate = DateTime.UtcNow,
                IsActive = true,
                Status = "Pending",
                VolunteerMessage = request.VolunteerMessage,
                AvailabilityNote = request.AvailabilityNote
            };

            await volunteerUser.AddAsync(application);
            await _unitOfWork.SaveChangesAsync();

            var response = new VolunteerUserDto
            {
                VolunteerUserId = application.VolunteerUserId,
                UserId = application.UserId,
                VolunteerTaskId = application.VolunteerTaskId,
                SignupDate = application.SignupDate,
                IsActive = application.IsActive,
                Status = application.Status ?? string.Empty,
                VolunteerMessage = application.VolunteerMessage,
                AvailabilityNote = application.AvailabilityNote
            };

            return ApiResponse<VolunteerUserDto>.SuccessResponse(response, "Task applied successfully");



        }
    }
}
