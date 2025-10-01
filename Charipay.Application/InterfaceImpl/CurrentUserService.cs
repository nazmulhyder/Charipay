using Charipay.Application.Interfaces;
using Charipay.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.InterfaceImpl
{
    /// <summary>
    /// Implementation of ICurrentUserService that retrieves the current User's
    /// Identify from the JWT claims with in the httpcontext
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes the new instance of the <see cref="CurrentUserService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor to the current http context</param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Gets the UserId(GUID) of the authenticated user from the JWT Token.
        /// Returns null if no user is authenticated
        /// </summary>
        public Guid? UserId {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                return userId != null ? Guid.Parse(userId) : (Guid?) null;
            }
        }
    }
}
