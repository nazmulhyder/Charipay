using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ProfileImageUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        //// Navigation Properties
       // public ICollection<UserRole> UserRoles { get; set; }
        //public ICollection<Charity> CreatedCharities { get; set; }
        //public ICollection<Donation> Donations { get; set; }
    }

}
