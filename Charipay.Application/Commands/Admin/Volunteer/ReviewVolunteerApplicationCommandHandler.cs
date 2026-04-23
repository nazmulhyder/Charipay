using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class ReviewVolunteerApplicationCommandHandler : IRequestHandler<ReviewVolunteerApplicationCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly ICurrentUserService _currentUserService;
        public ReviewVolunteerApplicationCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public  async Task<ApiResponse<string>> Handle(ReviewVolunteerApplicationCommand request, CancellationToken cancellationToken)
        {
            var applicationRequest = await _unitOfWork.VolunteerUser.GetByIdAsync(request.VolunteerUserId, cancellationToken);

            if (applicationRequest == null)
                return ApiResponse<string>.FailedResponse("Application not found.");

            if (applicationRequest.Status.ToLower() != "pending")
                return ApiResponse<string>.FailedResponse("Only pending can be reviwed");

            if(request.Action.ToLower()=="approve")
            {
                applicationRequest.Status = "Approved";
                applicationRequest.IsActive = true;
            }

            else if (request.Action.ToLower() == "reject")
            {
                applicationRequest.Status = "Rejected";
                applicationRequest.IsActive = false;
            }

            else
            {
                return ApiResponse<string>.FailedResponse("Invalid Action.");
            }

            applicationRequest.AdminNote = request.AdminNote;
            applicationRequest.ReviewedAt = DateTime.UtcNow;
            applicationRequest.ReviewedByAdminId = _currentUserService.UserId.Value;

            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse(null, $"Applicaion {request.Action.ToLower()}ed successfully");


        }
    }
}
