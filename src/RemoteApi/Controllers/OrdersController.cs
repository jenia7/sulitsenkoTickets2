using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteApi.Model;

namespace RemoteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MainDbContext dbContext;
        public OrdersController(MainDbContext db)
        {
            dbContext = db;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Purchase(Order order)
        {
            AppUser? user = await GetUser(order.Sub);
            if (user is null)
            {
                return BadRequest();
            }
            order.ConcertIds.ExceptWith(AlreadyAssociatedWith(user));
            order.ConcertIds.IntersectWith(await StoredConcerts());
            if (order.ConcertIds.Any())
            {
                List<Concert> concerts = await GetConcerts(order.ConcertIds);

                user.Concerts.AddRange(concerts);
                await dbContext.SaveChangesAsync();

            }
            return CreatedAtAction(nameof(GetConcertsByUserId), new { sub = user.Sub }, null);


            static List<long> AlreadyAssociatedWith(AppUser user)
            {
                return user.Concerts.Select(c => c.Id).ToList();
            }

            async Task<List<long>> StoredConcerts()
            {
                return await dbContext.Concerts.Select(c => c.Id).ToListAsync();
            }

            async Task<List<Concert>> GetConcerts(HashSet<long> ids)
            {
                return await dbContext
                                .Concerts
                                .Where(c => ids.Contains(c.Id))
                                .ToListAsync();
            }

            async Task<AppUser?> GetUser(string sub)
            {
                return await dbContext.Users
                                .Include(u => u.Concerts)
                                .FirstOrDefaultAsync(u => u.Sub == sub);
            }
        }

        [HttpGet("{sub}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Concert>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConcertsByUserId(string sub)
        {
            AppUser? user = await dbContext.Users
                .Include(u => u.Concerts)
                .FirstOrDefaultAsync(u => u.Sub == sub);
            if (user is null)
            {
                return NotFound();
            }
            foreach (var concert in user.Concerts)
            {
                concert.Users = null!;
            }
            return Ok(user.Concerts);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AppUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await dbContext.Users.ToListAsync();

            return Ok(users);
        }
    }
}
