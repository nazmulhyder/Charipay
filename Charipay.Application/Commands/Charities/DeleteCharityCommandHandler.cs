using AutoMapper;
using Charipay.Application.Common.Models;
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
    public class DeleteCharityCommandHandler : IRequestHandler<DeleteCharityCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCharityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(DeleteCharityCommand request, CancellationToken cancellationToken)
        {
            var existingCharity = await _unitOfWork.Charities.GetByIdAsync(request.CharityId);

            if (existingCharity == null)
                return ApiResponse<string>.FailedResponse("Data not exists");

            var data = _mapper.Map<Charity>(existingCharity);

            _unitOfWork.Charities.Remove(data);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Data deleted successfully", "Data deleted successfully");

        }
    }
}
