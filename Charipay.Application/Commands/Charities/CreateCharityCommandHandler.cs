using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Charities;
using Charipay.Application.Interfaces;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{

    /// <summary>
    /// 
    /// </summary>
    public class CreateCharityCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateCharityCommandHandler> logger, IMapper mapper, ICurrentUserService _currentUser)
        : IRequestHandler<CreateCharityCommand, ApiResponse<CharityDto>>
    {
        public async Task<ApiResponse<CharityDto>> Handle(CreateCharityCommand request, CancellationToken cancellationToken)
        {
            //DB Rule check (fluent validation already handles the format checks

            var emailExists = await unitOfWork.Charities.GetCharityByContactEmailAsync(request.ContactEmail, cancellationToken);
            var registrationNoExists = await unitOfWork.Charities.GetCharityByRegistrationNumberAsync(request.RegistrationNumber, cancellationToken);

            if (emailExists)
                return ApiResponse<CharityDto>.FailedResponse("Charity already exists with this Email:" + request.ContactEmail);

            if (registrationNoExists)
                return ApiResponse<CharityDto>.FailedResponse("Charity already exists with this Registration number:" + request.RegistrationNumber);


            var charity = new Charity()
            {
                CharityId = new Guid(),
                Name = request.Name,
                RegistrationNumber = request.RegistrationNumber,
                Description = request.Description,
                Website = request.Website,
                ContactEmail = request.ContactEmail,
                CreatedByUserId = _currentUser.UserId,
                IsApproved = true,
                CreatedAt = DateTime.UtcNow

            };

            await unitOfWork.Charities.AddAsync(charity);
            await unitOfWork.SaveChangesAsync();

            var responseDto = mapper.Map<CharityDto>(request);

            return ApiResponse<CharityDto>.SuccessResponse(responseDto, "Charity Created Successfully.");
        }
    }
}
