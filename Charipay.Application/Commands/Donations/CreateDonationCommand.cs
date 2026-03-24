using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Donation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Donations
{
    public class CreateDonationCommand : IRequest<ApiResponse<DonationResponseDto>>
    {
        public Guid CampaignId { get; set; }
        public double Amount { get; set; }
        public string? Message { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
