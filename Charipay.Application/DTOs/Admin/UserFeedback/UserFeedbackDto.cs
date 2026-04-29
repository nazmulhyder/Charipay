using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Admin.UserFeedback
{
    public class UserFeedbackDto
    {
        public Guid? Id { get; set; }
        public int? Rating { get; set; }
        public string FeedbackType { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
