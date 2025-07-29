using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Domain.Entities
{
    public class Role
    {
        public int RoleID { get; set; }
        public string Name { get; set; } = null!;

        // Navigation
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
