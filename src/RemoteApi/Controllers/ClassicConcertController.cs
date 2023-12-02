using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteApi.Model;

namespace RemoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassicConcertController : ControllerBase
    {
        private readonly IRepository repository;
        public ClassicConcertController(IRepository repo)
        {
            repository = repo;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ICollection<ClassicConcert>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClassicConcerts()
        {
            ICollection<ClassicConcert> concerts = await repository.AllClassicConcertsAsync();

            if (!concerts.Any())
            {
                return NotFound();
            }

            return Ok(concerts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClassicConcert), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClassicConcertById(long id)
        {
            ClassicConcert? concert = await repository.GetClassicConcertByIdAsync(id);

            if (concert is null)
            {
                return NotFound();
            }

            return Ok(concert);
        }

        [Authorize("Admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ClassicConcert), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateClassicConcert(ClassicConcert concert)
        {
            await repository.CreateClassicConcertAsync(concert);

            return CreatedAtAction(nameof(GetClassicConcertById), new { concert.Id }, concert);
        }
    }
}
