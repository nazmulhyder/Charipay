using Charipay.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class UserFeedback
    {
        public int UserFeedbackId { get; set; }

        public string? UserId { get; set; }

        public int? Rating { get; set; }

        public FeedbackType FeedbackType { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? PageUrl { get; set; }

        public FeedbackStatus Status { get; set; } = FeedbackStatus.New;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ReviewedAt { get; set; }

        public string? AdminNote { get; set; }
    }
}
