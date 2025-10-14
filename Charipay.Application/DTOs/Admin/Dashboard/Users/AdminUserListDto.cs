using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Admin.Dashboard.Users
{
    public class AdminUserListDto
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string PostCode { get; set; } = null!;
        public DateTime DOB { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProfileImageUrl { get; set; } = null!;
        public string Role {  get; set; } = null!;
    }
}
