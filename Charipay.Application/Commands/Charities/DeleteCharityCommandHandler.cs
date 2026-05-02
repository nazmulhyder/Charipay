using AutoMapper;
using Charipay.Application.Common.Models;
using Charipay.Application.Interfaces.Repositories;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class DeleteCharityCommandHandler : IRequestHandler<DeleteCharityCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICharityRepository _charityRepository;

        public DeleteCharityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICharityRepository charityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _charityRepository = charityRepository;
        }

 
        public async Task<ApiResponse<string>> Handle(DeleteCharityCommand request, CancellationToken cancellationToken)
        {
            var existingCharity = await _charityRepository.GetByIdAsync(request.CharityId, cancellationToken);

            if (existingCharity == null)
                return ApiResponse<string>.FailedResponse("Data not exists");

            var data = _mapper.Map<Charity>(existingCharity);

            _charityRepository.Remove(data, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Data deleted successfully", "Data deleted successfully");

        }
    }
}
