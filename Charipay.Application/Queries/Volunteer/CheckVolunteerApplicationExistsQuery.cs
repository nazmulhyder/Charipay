using Charipay.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Volunteer
{
    public class CheckVolunteerApplicationExistsQuery : IRequest<ApiResponse<bool>>
    {
        public Guid VolunteerTaskId { get; set; }
        public Guid VolunteerUserId { get; set; }
    }
}
