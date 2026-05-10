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
    public class RequestVolunteerApplicationCancellationCommandHandler (IUnitOfWork _unitOfWork, ICurrentUserService _currentUserService, IMapper _mapper, IVolunteerUserRepository volunteerUser, ILogger<RequestVolunteerApplicationCancellationCommandHandler> logger)
        : IRequestHandler<RequestVolunteerApplicationCancellationCommand, ApiResponse<VolunteerUserDto>>
    {
     
        public async Task<ApiResponse<VolunteerUserDto>> Handle(RequestVolunteerApplicationCancellationCommand request, CancellationToken cancellationToken)
        {
            var application = await volunteerUser.GetByIdAndUserIdAsync(request.VolunteerUserId, _currentUserService.UserId.Value);


            var currentStatus = application.Status?.Trim() ?? string.Empty;

            if (currentStatus.ToLower() != "pending" && currentStatus.ToLower() != "approved")
            {
                logger.LogWarning("Application cancellation request failed. Because, running task cannot be cancellable. Volunteer task ID: {taskid}, task name: {taskname}, user id: {userid}", application.VolunteerTaskId, application.VolunteerTask.Title, application.UserId);
                return ApiResponse<VolunteerUserDto>.FailedResponse("Cancellation request is not allowed for this application!");

            }

            logger.LogInformation("Application cancellation request started. volunteer task ID: {taskid}, task name: {taskname}, user id: {userid}", application.VolunteerTaskId, application.VolunteerTask.Title, application.UserId);


            application.Status = "CancelRequested";


            application.VolunteerMessage = request.VolunteerMessage;

            await _unitOfWork.SaveChangesAsync();

            logger.LogInformation("Application cancellation request has been made successfully. volunteer task ID: {taskid}, task name: {taskname}, user id: {userid}", application.VolunteerTaskId, application.VolunteerTask.Title, application.UserId);



            var response = _mapper.Map<VolunteerUserDto>(application);

            return ApiResponse<VolunteerUserDto>.SuccessResponse(response, "Cancellation request submitted successfully.");


        }
    }
}
