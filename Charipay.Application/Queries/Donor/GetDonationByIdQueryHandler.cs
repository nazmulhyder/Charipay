using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donor.Donation;
using Charipay.Application.Interfaces.Common;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Donor
{
    public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, ApiResponse<DonationResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetDonationByIdQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<DonationResponseDto>> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Donations.Donation(request.DonationId, _currentUserService.UserId.Value);

            var response = _mapper.Map<DonationResponseDto>(item);

            return ApiResponse<DonationResponseDto>.SuccessResponse(response, "Success");
        }
    }
}
