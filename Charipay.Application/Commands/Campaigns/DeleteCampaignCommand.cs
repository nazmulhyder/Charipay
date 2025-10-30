using Charipay.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class DeleteCampaignCommand : IRequest<ApiResponse<string>>
    {
        public Guid CampaignId { get; set; }
    }
}
