using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donor.Donation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Queries.Donor
{
    public class GetDonationByIdQuery : IRequest<ApiResponse<DonationResponseDto>>
    {
        public Guid DonationId {get; set; }
    }
}
