using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? ProfileImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string? AddressLine1 { get; set; } = null!;
        public string? PostCode { get; set; } = null!;
        public DateTime? DOB { get; set; }

        //// Navigation Properties
         public ICollection<UserRole> UserRoles { get; set; }
        //public ICollection<Charity> CreatedCharities { get; set; }
        //public ICollection<Donation> Donations { get; set; }
    }

}
