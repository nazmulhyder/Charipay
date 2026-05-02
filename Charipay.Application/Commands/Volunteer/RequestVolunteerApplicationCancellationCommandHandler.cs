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
    public class RequestVolunteerApplicationCancellationCommandHandler : IRequestHandler<RequestVolunteerApplicationCancellationCommand, ApiResponse<VolunteerUserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IVolunteerUserRepository volunteerUser;
        public RequestVolunteerApplicationCancellationCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper, IVolunteerUserRepository _volunteerUser)
        {
            this._unitOfWork    = unitOfWork;
            this._currentUserService = currentUserService;
            _mapper = mapper;
            volunteerUser = _volunteerUser;
        }

        public async Task<ApiResponse<VolunteerUserDto>> Handle(RequestVolunteerApplicationCancellationCommand request, CancellationToken cancellationToken)
        {
            var application = await volunteerUser.GetByIdAndUserIdAsync(request.VolunteerUserId, _currentUserService.UserId.Value);


            var currentStatus = application.Status?.Trim() ?? string.Empty;

            if (currentStatus.ToLower() != "pending" && currentStatus.ToLower() != "approved")
                return ApiResponse<VolunteerUserDto>.FailedResponse("Cancellation request is not allowed for this application!");

            application.Status = "CancelRequested";


            application.VolunteerMessage = request.VolunteerMessage;

            await _unitOfWork.SaveChangesAsync();


            var response = _mapper.Map<VolunteerUserDto>(application);

            return ApiResponse<VolunteerUserDto>.SuccessResponse(response, "Cancellation request submitted successfully.");


        }
    }
}
