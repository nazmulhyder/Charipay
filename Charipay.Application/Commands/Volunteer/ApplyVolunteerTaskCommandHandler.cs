using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class ApplyVolunteerTaskCommandHandler(IUnitOfWork _unitOfWork, ICurrentUserService _currentUserService , IVolunteerTaskRepository _volunteerTaskRepository, IVolunteerUserRepository volunteerUser, ILogger<ApplyVolunteerTaskCommandHandler> logger) 
        : IRequestHandler<ApplyVolunteerTaskCommand, ApiResponse<VolunteerUserDto>>
    {
        public async Task<ApiResponse<VolunteerUserDto>> Handle(ApplyVolunteerTaskCommand request, CancellationToken cancellationToken)
        {
           

            var task = await _volunteerTaskRepository.GetByIdAsync(request.VolunteerTaskId, cancellationToken);

            if (task == null)
            {
                logger.LogWarning("Task application failed. Because it does not exists. Task ID: {taskId}", request.VolunteerTaskId);
                return ApiResponse<VolunteerUserDto>.FailedResponse("Task does not exists!", null);
            }

            if (!task.IsActive)
            {
                logger.LogWarning("Task application failed. Because it is no longer active task. Task ID: {taskId}, Title:{title}", request.VolunteerTaskId, task.Title);
                return ApiResponse<VolunteerUserDto>.FailedResponse("This volunteer task is no longer active!", null);

            }
           


            var volunteerTaskExists = await volunteerUser.HasUserAlreadyAppliedAsync(request.VolunteerTaskId, _currentUserService.UserId);
            if (volunteerTaskExists)
            {
                logger.LogWarning("Task application failed. Because, it has been applied already. Task ID: {taskId}, Title:{title}", request.VolunteerTaskId, task.Title);
                return ApiResponse<VolunteerUserDto>.FailedResponse("User already applied for this task!", null);
            }

            var activeApplicantCount = await volunteerUser.GetActiveApplicationCountAsync(request.VolunteerTaskId);

            if(activeApplicantCount >= task.MaxVolunteer)
            {
                logger.LogWarning("Task application failed. Because, it reached to the maximum volunteer level. Task ID: {taskId}, Title:{title}", request.VolunteerTaskId, task.Title);

                return ApiResponse<VolunteerUserDto>.FailedResponse("Volunteer reached the maximum level for this task!", null);
            }


            logger.LogInformation("Task application started. Task ID: {taskId}, Title:{title}", request.VolunteerTaskId, task.Title);


            var application = new VolunteerUser
            {
                VolunteerUserId = Guid.NewGuid(),
                UserId = _currentUserService.UserId.Value,
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

            logger.LogInformation("Task application successful. Task ID: {taskId}, Title:{title}", request.VolunteerTaskId, task.Title);

            return ApiResponse<VolunteerUserDto>.SuccessResponse(response, "Task applied successfully");



        }
    }
}
