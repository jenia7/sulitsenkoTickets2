namespace RemoteApi.Model
{
    public abstract class Concert
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public ConcertType ConcertType { get; set; }
        public DateTime DateTime { get; set; }

        public ConcertInfo? ConcertInfo { get; set; }
        public List<AppUser> Users { get; set; } = new();
    }
}
