namespace RemoteApi.Model
{
    public class Order
    {
        public string Sub { get; set; } = string.Empty;
        public HashSet<long> ConcertIds { get; set; } = new();
    }
}
