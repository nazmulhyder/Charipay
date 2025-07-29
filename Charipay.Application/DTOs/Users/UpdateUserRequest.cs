using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
    }

}
