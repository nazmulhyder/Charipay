using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Charities;
using Charipay.Domain.Entities;
using Charipay.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class UpdateCharityCommandHandler : IRequestHandler<UpdateCharityCommand, ApiResponse<CharityDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCharityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CharityDto>> Handle(UpdateCharityCommand request, CancellationToken cancellationToken)
        {
            var existingCharity = _unitOfWork.Charities.GetByIdAsync(request.CharityId);

            if (existingCharity == null)
                return ApiResponse<CharityDto>.FailedResponse("Charity does not exists");

            var charity = _mapper.Map<Charity>(request);

            _unitOfWork.Charities.Update(charity);
            await _unitOfWork.SaveChangesAsync();

            var getUpdaetdCharity = _unitOfWork.Charities.GetByIdAsync(request.CharityId);

            var result = _mapper.Map<CharityDto>(getUpdaetdCharity);

            return ApiResponse<CharityDto>.SuccessResponse(result, "Updated successfully");
        }
    }
}
