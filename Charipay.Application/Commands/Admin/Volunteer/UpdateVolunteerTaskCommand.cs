using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.Volunteer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class UpdateVolunteerTaskCommand : IRequest<ApiResponse<VolunteerTaskDto>>
    {
        public Guid VolunteerTaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxVolunteer { get; set; }
        public bool IsActive { get; set; }
    }
}
