using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class CheckVolunteerApplicationExistsQueryHandler : IRequestHandler<CheckVolunteerApplicationExistsQuery, ApiResponse<bool>>
    {

        private readonly IUnitOfWork _unitOfWork;
        
        public CheckVolunteerApplicationExistsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<bool>> Handle(CheckVolunteerApplicationExistsQuery request, CancellationToken cancellationToken)
        {
            var IsApplied = await _unitOfWork.VolunteerApplicationHistory.IsAlreadyAppliedAsync(request.VolunteerTaskId, request.VolunteerUserId);

            return ApiResponse<bool>.SuccessResponse(IsApplied, "");
        }
    }
}
