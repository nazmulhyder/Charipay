using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Volunteer;
using Charipay.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class ApplyVolunteerTaskCommand : IRequest<ApiResponse<VolunteerUserDto>>
    {
        public Guid VolunteerTaskId { get; set; }
        public string? VolunteerMessage { get; set; }
        public string? AvailabilityNote { get; set; }
    }
}
