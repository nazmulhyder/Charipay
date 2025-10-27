using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Charities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class UpdateCharityCommand : IRequest<ApiResponse<CharityDto>>
    {
        public Guid CharityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }
        public bool IsApproved { get; set; }
        public string RegistrationNumber { get; set; }

    }
}
