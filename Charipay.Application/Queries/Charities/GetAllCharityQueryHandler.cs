using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Charities;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Charities
{
    public class GetAllCharityQueryHandler : IRequestHandler<GetAllCharityQuery, ApiResponse<PageResult<CharityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICharityRepository charityRepository;
        public GetAllCharityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICharityRepository _charityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           charityRepository = _charityRepository;
        }

        public async Task<ApiResponse<PageResult<CharityDto>>> Handle(GetAllCharityQuery request, CancellationToken cancellationToken)
        {
            var (charities, totalCount) = await charityRepository.GetPagedCharities(request.PageNumber, request.PageSize, request.Search);

            var mapperCharityList = _mapper.Map<List<CharityDto>>(charities);

            var result = new PageResult<CharityDto>(mapperCharityList, totalCount, request.PageNumber, request.PageSize);

            return ApiResponse<PageResult<CharityDto>>.SuccessResponse(result, "Fetched Successfully");
        }


    }
}
