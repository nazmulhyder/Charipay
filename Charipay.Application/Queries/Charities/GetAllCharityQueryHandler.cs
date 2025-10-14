using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Common.Pagination;
using Charipay.Application.DTOs.Charities;
using Charipay.Domain.Interfaces;
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
        public GetAllCharityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PageResult<CharityDto>>> Handle(GetAllCharityQuery request, CancellationToken cancellationToken)
        {
            var (charities, totalCount) = await _unitOfWork.Charities.GetPagedCharities(request.PageNumber, request.PageSize, request.Search);

            var mapperCharityList = _mapper.Map<List<CharityDto>>(charities);

            var result = new PageResult<CharityDto>(mapperCharityList, totalCount, request.PageNumber, request.PageSize);

            return ApiResponse<PageResult<CharityDto>>.SuccessResponse(result, "Fetched Successfully");
        }


    }
}
