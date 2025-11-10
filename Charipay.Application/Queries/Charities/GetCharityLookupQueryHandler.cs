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
    public class GetCharityLookupQueryHandler : IRequestHandler<GetCharityLookupQuery, ApiResponse<IEnumerable<CharityLookupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public GetCharityLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<CharityLookupDto>>> Handle(GetCharityLookupQuery request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Charities.GetAllAsync();

            var finalResult = _mapper.Map<IEnumerable<CharityLookupDto>>(data);

            return ApiResponse<IEnumerable<CharityLookupDto>>.SuccessResponse(finalResult,"Success");
        }
    }
}
