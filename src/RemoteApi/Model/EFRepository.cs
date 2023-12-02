using Microsoft.EntityFrameworkCore;

namespace RemoteApi.Model
{
    public class EFRepository : IRepository
    {
        private readonly MainDbContext dbContext;
        public EFRepository(MainDbContext db)
        {
            dbContext = db;
        }

        public async Task<ICollection<Concert>> GetConcertsByNameAsync(string pattern)
        {
            return await dbContext.Concerts
                .Where(c => c.Name.Contains(pattern))
                .ToListAsync();
        }

        public async Task<ICollection<ClassicConcert>> AllClassicConcertsAsync()
        {
            return await dbContext.Classics.Include(c => c.ConcertInfo).ToListAsync();
        }

        public async Task<ICollection<Concert>> AllConcertsAsync()
        {
            return await dbContext.Concerts.Include(c => c.ConcertInfo).ToListAsync();
        }

        public async Task CreateClassicConcertAsync(ClassicConcert concert)
        {
            await dbContext.Classics.AddAsync(concert);
            await dbContext.SaveChangesAsync();
        }

        public async Task CreateOpenAirAsync(OpenAir openAir)
        {
            await dbContext.OpenAirs.AddAsync(openAir);
            await dbContext.SaveChangesAsync();
        }

        public async Task CreatePartyAsync(Party party)
        {
            await dbContext.Parties.AddAsync(party);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ClassicConcert?> GetClassicConcertByIdAsync(long id)
        {
            return await dbContext.Classics.FindAsync(id);
        }

        public async Task<Concert?> GetConcertByIdAsync(long id)
        {
            return await dbContext.Concerts
                .Include(c => c.ConcertInfo)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<OpenAir?> GetOpenAirByIdAsync(long id)
        {
            return await dbContext.OpenAirs.FindAsync(id);
        }

        public async Task<Party?> GetPartyByIdAsync(long id)
        {
            return await dbContext.Parties.FindAsync(id);
        }

        public async Task<ICollection<OpenAir>> AllOpenAirsAsync()
        {
            return await dbContext.OpenAirs.Include(c => c.ConcertInfo).ToListAsync();
        }

        public async Task<ICollection<Party>> AllPartiesAsync()
        {
            return await dbContext.Parties.Include(c => c.ConcertInfo).ToListAsync();
        }

        public async Task<ICollection<Concert>> SearchConcertsAsync(string? searcPattern, string[] filters)
        {
            IQueryable<Concert> query = dbContext.Concerts;

            if (filters.Any())
            {
                HashSet<ConcertType> parsed = new(3);
                foreach (var filter in filters)
                {
                    if (Enum.TryParse(filter, ignoreCase: true, out ConcertType concertType))
                    {
                        parsed.Add(concertType);
                    }
                }
                if (parsed.Any())
                {
                    query = query.Where(c => parsed.Contains(c.ConcertType));
                }
            }
            if (!string.IsNullOrEmpty(searcPattern))
            {
                query = query.Where(c => c.Name.Contains(searcPattern));
            }

            query = query.Include(c => c.ConcertInfo);

            return await query.ToListAsync();

        }
    }
}
