using Charipay.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class UploadCampaignImageCommand : IRequest<ApiResponse<string>>
    {
        public Guid CampaignId { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}
