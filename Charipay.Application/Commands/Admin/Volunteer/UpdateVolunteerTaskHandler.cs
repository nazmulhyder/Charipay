using AutoMapper;
using Charipay.Application.Commands.Campaigns;
using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.Volunteer;
using Charipay.Application.DTOs.Campaigns;
using Charipay.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class UpdateVolunteerTaskHandler : IRequestHandler<UpdateVolunteerTaskCommand, ApiResponse<VolunteerTaskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateVolunteerTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
    
        public async Task<ApiResponse<VolunteerTaskDto>> Handle(UpdateVolunteerTaskCommand request, CancellationToken cancellationToken)
        {
            var existingTask = await _unitOfWork.VolunteerTask.GetByIdAsync(request.VolunteerTaskId, cancellationToken);

            if (existingTask == null)
                return ApiResponse<VolunteerTaskDto>.FailedResponse("Data not exists!");

            _mapper.Map(request, existingTask);

            await _unitOfWork.SaveChangesAsync();

            var updatedVolunteerTask = await _unitOfWork.VolunteerTask.GetByIdAsync(request.VolunteerTaskId, cancellationToken);

            var result = _mapper.Map<VolunteerTaskDto>(updatedVolunteerTask);

            return ApiResponse<VolunteerTaskDto>.SuccessResponse(result, "success");
        }
    }
}
