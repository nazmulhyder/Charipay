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
    public class GetAllCharityQueryHandler : IRequestHandler<GetAllCharityQuery, ApiResponse<List<CharityDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCharityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CharityDto>>> Handle(GetAllCharityQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Charities.GetAllAsync();

            var result = _mapper.Map<List<CharityDto>>(users);

            return ApiResponse<List<CharityDto>>.SuccessResponse(result, "Successful");
        }
    }
}
