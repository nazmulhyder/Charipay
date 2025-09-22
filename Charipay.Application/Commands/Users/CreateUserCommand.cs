using Charipay.Application.Common.Models;
using Charipay.Application.DTOs.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AddressLine1 { get; set; }
        public string? PostCode { get; set; }
        public DateTime? DOB { get; set; }
        public int RoleID {  get; set; }
    }
}
