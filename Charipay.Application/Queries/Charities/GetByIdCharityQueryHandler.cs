using AutoMapper;
using Charipay.Application.Common.Models;
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
    public class GetByIdCharityQueryHandler : IRequestHandler<GetByIdCharityQuery, ApiResponse<CharityDto>>
    {
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        public GetByIdCharityQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<CharityDto>> Handle(GetByIdCharityQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Charities.GetByIdAsync(request.id);

            var mapResult = _mapper.Map<CharityDto>(data);

            return ApiResponse<CharityDto>.SuccessResponse(mapResult, "Successful");
        }
    }
}
