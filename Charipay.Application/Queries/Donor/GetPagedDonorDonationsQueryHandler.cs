using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Admin.Volunteer;
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
    public class GetPagedDonorDonationsQueryHandler : IRequestHandler<GetPagedDonorDonationsQuery, ApiResponse<PageResult<DonationResponseDto>>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDonationRepository donationRepository;

        public GetPagedDonorDonationsQueryHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IMapper mapper, IDonationRepository _donationRepository)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            donationRepository = _donationRepository;
        }


        public async Task<ApiResponse<PageResult<DonationResponseDto>>> Handle(GetPagedDonorDonationsQuery request, CancellationToken cancellationToken)
        {
            var items = await donationRepository.Donations(_currentUserService.UserId.Value, request.PageNumber, request.PageSize, request.search);

            var response = _mapper.Map<List<DonationResponseDto>>(items);

            var totalCount = response.Count;

            var result = new PageResult<DonationResponseDto>(
               response,
               totalCount,
               request.PageNumber,
               request.PageSize
           );

            return ApiResponse<PageResult<DonationResponseDto>>.SuccessResponse(result, "Success");
        }
    }
}
