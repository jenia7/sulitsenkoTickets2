using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddScopeController : ControllerBase
    {
        UserManager<ApplicationUser> manager;

        public AddScopeController(UserManager<ApplicationUser> m)
        {
            manager = m;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string userName)
        {
            ApplicationUser user = await manager.FindByNameAsync(userName);
            if (user is null)
            {
                return NotFound();
            }
            //Log.Information($"userName: {user.UserName}");
            return Ok(user);
        }

    }
}
