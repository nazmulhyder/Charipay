using Charipay.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.Volunteer
{
    public class ReviewVolunteerApplicationCommand : IRequest<ApiResponse<string>>
    {
        public Guid VolunteerUserId { get; set; }
        public string Action {  get; set; } = string.Empty;
        public string? AdminNote { get; set; }
    }
}
