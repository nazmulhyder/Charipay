using AutoMapper;
using Charipay.Application.Common.Models;
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
    public class GetByIdCharityQueryHandler : IRequestHandler<GetByIdCharityQuery, ApiResponse<CharityDto>>
    {
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;
        private readonly ICharityRepository charityRepository;
        public GetByIdCharityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICharityRepository _charityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            charityRepository = _charityRepository;
        }

        public async Task<ApiResponse<CharityDto>> Handle(GetByIdCharityQuery request, CancellationToken cancellationToken)
        {
            var data = await charityRepository.GetByIdAsync(request.id, cancellationToken);

            var mapResult = _mapper.Map<CharityDto>(data);

            return ApiResponse<CharityDto>.SuccessResponse(mapResult, "Successful");
        }
    }
}
