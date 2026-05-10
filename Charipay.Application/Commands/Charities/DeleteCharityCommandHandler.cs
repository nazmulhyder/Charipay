using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class DeleteCharityCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper , ICharityRepository _charityRepository, ILogger<DeleteCharityCommandHandler> logger) 
        : IRequestHandler<DeleteCharityCommand, ApiResponse<string>>
    {

        public async Task<ApiResponse<string>> Handle(DeleteCharityCommand request, CancellationToken cancellationToken)
        {
            var existingCharity = await _charityRepository.GetByIdAsync(request.CharityId, cancellationToken);

            if (existingCharity == null)
            {
                logger.LogWarning("Charity deletion failed. Charity does not exists. Charity ID: {charityId}", request.CharityId);
                return ApiResponse<string>.FailedResponse("Data not exists");
            }



            logger.LogInformation("Charity deletion started. Charity ID: {charityId}, Charity Name: {charityName}", request.CharityId, existingCharity.Name);
            var data = _mapper.Map<Charity>(existingCharity);

            _charityRepository.Remove(data, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            logger.LogInformation("Charity deleted successfully. Charity ID: {charityId}, Charity Name: {charityName}", request.CharityId, existingCharity.Name);


            return ApiResponse<string>.SuccessResponse("Data deleted successfully", "Data deleted successfully");

        }
    }
}
