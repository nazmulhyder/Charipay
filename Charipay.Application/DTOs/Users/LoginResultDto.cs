using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Users
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
