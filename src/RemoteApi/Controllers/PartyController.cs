using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteApi.Model;

namespace RemoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly IRepository repository;
        public PartyController(IRepository repo)
        {
            repository = repo;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ICollection<Party>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllParties()
        {
            ICollection<Party> parties = await repository.AllPartiesAsync();
            if (!parties.Any())
            {
                return NotFound();
            }
            return Ok(parties);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Party), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPartyById(long id)
        {
            Party? party = await repository.GetPartyByIdAsync(id);

            if (party is null)
            {
                return NotFound();
            }
            return Ok(party);
        }

        [Authorize("Admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(Party), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateParty(Party party)
        {
            await repository.CreatePartyAsync(party);

            return CreatedAtAction(nameof(GetPartyById), new { party.Id }, party);
        }
    }
}
