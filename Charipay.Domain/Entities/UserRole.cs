using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class UserRole
    {
        public Guid UserID { get; set; }
        public int RoleID { get; set; }

        // Navigation
        public User User { get; set; }
        public Role Role { get; set; }
    }

}
