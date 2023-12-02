using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemoteApi.Model;

namespace RemoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAirController : ControllerBase
    {
        private readonly IRepository repository;
        public OpenAirController(IRepository repo)
        {
            repository = repo;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ICollection<OpenAir>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOpenAirs()
        {
            ICollection<OpenAir> openAirs = await repository.AllOpenAirsAsync();
            if (!openAirs.Any())
            {
                return NotFound();
            }
            return Ok(openAirs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OpenAir), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOpenAirById(long id)
        {
            OpenAir? openAir = await repository.GetOpenAirByIdAsync(id);

            if (openAir is null)
            {
                return NotFound();
            }
            return Ok(openAir);
        }

        [Authorize("Admin")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(OpenAir), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateOpenAir(OpenAir openAir)
        {
            await repository.CreateOpenAirAsync(openAir);

            return CreatedAtAction(nameof(GetOpenAirById), new { openAir.Id }, openAir);
        }
    }
}
