namespace RemoteApi.Model
{
    public class Location
    {
        public string Name { get; set; } = string.Empty;
        public Address Address { get; set; } = new();
    }
}
