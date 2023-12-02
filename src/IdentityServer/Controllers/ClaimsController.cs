using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ClaimsController(UserManager<ApplicationUser> m, RoleManager<IdentityRole> r, ILogger<ClaimsController> log)
        {
            userManager = m;
            roleManager = r;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClaims(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                return NotFound();
            }
            var claims = (await userManager.GetClaimsAsync(user)).Select(c => new { c.Type, c.Value });

            return Ok(claims);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateScope(string name)
        {
            ApplicationUser user = await userManager.FindByNameAsync(name);
            if (user is null)
            {
                return BadRequest();
            }
            var claims = HttpContext.User.Claims;
            List<string> strings = new(36);
            foreach (var claim in claims)
            {
                strings.Add($"type: {claim.Type} value: {claim.Value}");
            }

            return Created("", strings);
        }
    }
}
