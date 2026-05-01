using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Admin.UserFeedback;
using Charipay.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Admin.UserFeedback
{
    public class CreateFeedbackCommand : IRequest<ApiResponse<UserFeedbackDto>>
    {
        public int? Rating { get; set; }

        public FeedbackType FeedbackType { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? PageUrl { get; set; }
    }
}
