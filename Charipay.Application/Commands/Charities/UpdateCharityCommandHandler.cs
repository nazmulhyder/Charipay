using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Charities;
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
    public class UpdateCharityCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper, ICharityRepository _charityRepository, ILogger<UpdateCharityCommandHandler> logger) : IRequestHandler<UpdateCharityCommand, ApiResponse<CharityDto>>
    {

        public async Task<ApiResponse<CharityDto>> Handle(UpdateCharityCommand request, CancellationToken cancellationToken)
        {
            var existingCharity = await _charityRepository.GetByIdAsync(request.CharityId, cancellationToken);

            if (existingCharity == null)
            {
                logger.LogWarning("Charity update failed. Charity does not exists. Charity ID: {charityId}", request.CharityId);
                return ApiResponse<CharityDto>.FailedResponse("Charity does not exists");
            }


            logger.LogInformation("Charity update started. Charity ID: {charityId}, Charity Name {charityName}", request.CharityId, existingCharity.Name);

            _mapper.Map(request, existingCharity);

           // _unitOfWork.Charities.Update(charity);
            await _unitOfWork.SaveChangesAsync();

            var getUpdaetdCharity = await _charityRepository.GetByIdAsync(request.CharityId, cancellationToken);

            var result = _mapper.Map<CharityDto>(getUpdaetdCharity);

            logger.LogInformation("Charity updated successfully. Charity ID: {charityId}, Charity Name {charityName}", request.CharityId, existingCharity.Name);


            return ApiResponse<CharityDto>.SuccessResponse(result, "Updated successfully");
        }
    }
}
