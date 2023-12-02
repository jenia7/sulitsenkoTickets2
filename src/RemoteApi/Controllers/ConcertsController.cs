using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteApi.Model;

namespace RemoteApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ConcertsController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly MainDbContext dbContext;
        public ConcertsController(IRepository repo, MainDbContext db)
        {
            repository = repo;
            dbContext = db;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Concert>), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> GetConcerts([FromQuery] string[] filters, string? pattern)
        {
            ICollection<Concert> concerts;

            concerts = await repository.SearchConcertsAsync(pattern, filters);

            return Ok(concerts);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(ICollection<Concert>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllConcerts()
        {
            ICollection<Concert> concerts = await repository.AllConcertsAsync();
            return Ok(concerts);
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Concert), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConcertById(long id)
        {
            Concert? concert = await repository.GetConcertByIdAsync(id);

            if (concert is null)
            {
                return NotFound();
            }

            //concert.ConcertInfo.Concert = null!;
            return Ok(concert);
        }

        [HttpGet("all/{pattern}")]
        [ProducesResponseType(typeof(ICollection<Concert>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConcertsByName(string pattern)
        {
            ICollection<Concert> concerts = await repository
                .GetConcertsByNameAsync(pattern);

            if (!concerts.Any())
            {
                return NotFound();
            }

            return Ok(concerts);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAllConcerts()
        {
            await dbContext.Concerts.ExecuteDeleteAsync();
            return Ok();
        }
    }
}
