using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Home;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Home
{
    public class GetHomeStatsQueryHandler : IRequestHandler<GetHomeStatsQuery, ApiResponse<HomeStatsDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublicRepository publicRepository;

        public GetHomeStatsQueryHandler(IUnitOfWork unitOfWork, IPublicRepository _publicRepository)
        {
            _unitOfWork = unitOfWork;
            publicRepository = _publicRepository;
        }

        public async Task<ApiResponse<HomeStatsDto>> Handle(GetHomeStatsQuery request, CancellationToken cancellationToken)
        {
            var result =  await publicRepository.GetHomeStatsAsync();

            return ApiResponse<HomeStatsDto>.SuccessResponse(result,"Success");

        }
    }
}
