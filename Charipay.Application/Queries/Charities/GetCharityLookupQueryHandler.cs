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
    public class GetCharityLookupQueryHandler : IRequestHandler<GetCharityLookupQuery, ApiResponse<IEnumerable<CharityLookupDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICharityRepository charityRepository;

        public GetCharityLookupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ICharityRepository _charityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            charityRepository = _charityRepository;
        } 

        public async Task<ApiResponse<IEnumerable<CharityLookupDto>>> Handle(GetCharityLookupQuery request, CancellationToken cancellationToken)
        {
            var data = await charityRepository.GetAllAsync(cancellationToken);

            var finalResult = _mapper.Map<IEnumerable<CharityLookupDto>>(data);

            return ApiResponse<IEnumerable<CharityLookupDto>>.SuccessResponse(finalResult,"Success");
        }
    }
}
