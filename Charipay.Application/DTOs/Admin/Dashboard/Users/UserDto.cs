using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.DTOs.Admin.Dashboard.Users
{
    public class UserDto
    {
        public Guid UserId { get; }
        public string FullName { get;  } = null!;
        public string Email { get;  } = null!;
        public string Phone { get;  } = null!;
        public string AddressLine1 { get;  } = null!;
        public string PostCode { get;  } = null!;
        public DateTime DOB { get;  }
        public DateTime CreatedAt { get;  }
        public string ProfileImageUrl { get;  } = null!;
        public string Role {  get;  } = null!;
    }
}
