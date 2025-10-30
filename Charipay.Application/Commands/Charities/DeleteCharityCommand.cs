using Charipay.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Charities
{
    public class DeleteCharityCommand : IRequest<ApiResponse<string>>
    {
        public Guid CharityId { get; set; }
    }
}
