namespace RemoteApi.Model
{
    public class Address
    {
        public Country Country { get; set; }
        public string Region { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string Building { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
