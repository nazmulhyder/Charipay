using Charipay.Application.Common.Models;
using Charipay.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Volunteer
{
    public class UpdateVolunteerApplicationStatusCommand : IRequest<ApiResponse<string>>
    {
        public Guid VolunteerUserId { get; set; }
        public VolunteerApplicationAction Action { get; set; }
        public string? Message { get; set; }
    }
}
