using Charipay.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces.Repositories
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, IEnumerable<string> roles);
    }
}
