using Microsoft.EntityFrameworkCore;

namespace RemoteApi.Model
{
    public static class SeedDatabase
    {
        public static void EnsureDbContainsData(IServiceProvider serviceProvider)
        {
            using MainDbContext dbContext = serviceProvider.GetRequiredService<MainDbContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            if (!dbContext.Users.Any())
            {
                AddUsers(dbContext);
                AddParty(dbContext);
                AddClassicConcert(dbContext);
                AddOpenAir(dbContext);
                dbContext.SaveChanges();
            }
        }

        private static void AddUsers(MainDbContext dbContext)
        {
            AppUser bob = new()
            {
                Sub = "ddaf01cb-5047-4e27-a794-91d4d8e603c4"
            };
            AppUser admin = new()
            {
                Sub = "bd8364ba-9e17-448d-86ce-5d71d5c23741"
            };
            dbContext.Users.AddRange(bob, admin);
        }

        private static void AddOpenAir(MainDbContext dbContext)
        {
            OpenAir openAir = new()
            {
                Name = "Open air 1",
                ConcertInfo = new()
                {
                    Performer = "Name 1",
                }
            };
            dbContext.OpenAirs.Add(openAir);
        }

        private static void AddClassicConcert(MainDbContext dbContext)
        {
            ClassicConcert classicConcert = new()
            {
                Name = "Classic 1",
                ConcertInfo = new()
                {
                    Performer = "Name 2"
                }
            };
            dbContext.Classics.Add(classicConcert);
        }

        private static void AddParty(MainDbContext dbContext)
        {
            Party party = new()
            {
                Name = "Party 1",
                ConcertInfo = new()
                {
                    Performer = "Name 3"
                }
            };
            dbContext.Parties.Add(party);
        }
    }
}
