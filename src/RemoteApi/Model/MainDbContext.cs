using Microsoft.EntityFrameworkCore;

namespace RemoteApi.Model
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> opts)
            : base(opts)
        {

        }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<ClassicConcert> Classics { get; set; }
        public DbSet<OpenAir> OpenAirs { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>()
                .HasDiscriminator(c => c.ConcertType)
                .HasValue<ClassicConcert>(ConcertType.Classic)
                .HasValue<OpenAir>(ConcertType.OpenAir)
                .HasValue<Party>(ConcertType.Party);
            modelBuilder.Entity<OpenAir>();
            modelBuilder.Entity<Party>();
            modelBuilder.Entity<ClassicConcert>();

            modelBuilder.Entity<ConcertInfo>().OwnsOne(ci => ci.Location, l =>
            {
                l.OwnsOne(l => l.Address);
            });

            modelBuilder.Entity<AppUser>().HasKey(user => user.Sub);
        }
    }
}
