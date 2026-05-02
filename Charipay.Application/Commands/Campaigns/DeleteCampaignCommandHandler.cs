using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly ICampaignRepository _campaignRepository;

        public DeleteCampaignCommandHandler(IUnitOfWork unitofWork, IMapper mapper, ICampaignRepository campaignRepository)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
            _campaignRepository = campaignRepository;
        }

        public async Task<ApiResponse<string>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingData = await _campaignRepository.GetByIdAsync(request.CampaignId, cancellationToken);

            if (existingData == null)
                return ApiResponse<string>.FailedResponse("Data not exists.");

            _campaignRepository.Remove(existingData, cancellationToken);
            await _unitofWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Deleted successfully", "Deleted successfully");
        }
    }
}
