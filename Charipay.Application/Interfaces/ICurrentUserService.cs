using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Interfaces
{
    /// <summary>
    /// Provide the imformation about the current authenticated user.
    /// 
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the unique indentifier (GUID) for the current logged-in user.
        /// Returns null if the user is unauthenticated
        /// </summary>
        Guid? UserId { get; }
    }
}
