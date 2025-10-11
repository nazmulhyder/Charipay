using Microsoft.AspNetCore.Mvc;

namespace Charipay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminDashboardController : ControllerBase
    {
        public AdminDashboardController()
        {
            
        }

        [HttpPost("UserList")]
        public Task<IActionResult> UserList(CancellationToken token)
        {

        }
    }
}
