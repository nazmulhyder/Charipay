using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Domain.Interfaces;
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

        public DeleteCampaignCommandHandler(IUnitOfWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<ApiResponse<string>> Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            var existingData = await _unitofWork.Campaigns.GetByIdAsync(request.CampaignId);

            if (existingData == null)
                return ApiResponse<string>.FailedResponse("Data not exists.");

            _unitofWork.Campaigns.Remove(existingData);
            await _unitofWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Deleted successfully", "Deleted successfully");
        }
    }
}
