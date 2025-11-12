using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Campaigns;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Campaigns
{
    public class CreateCampaignCommand : IRequest<ApiResponse<CampaignDto>>
    {
        public string CampaignName { get; set; } = string.Empty;
        public string CampaignDescription { get; set; } = string.Empty;
        public double GoalAmount { get; set; }
        public double CurrentAmount { get; set; }
        public DateTime CampaignStartDate { get; set; }
        public DateTime CampaignEndDate { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public Guid? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CharityId { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; } = true;
        public string CurrencyCode { get; set; } = "GBP"; //default
    }
}
