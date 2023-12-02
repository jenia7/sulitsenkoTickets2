namespace RemoteApi.Model
{
    public class CartItem
    {
        public long ConcertId { get; set; }
        public ConcertType ConcertType { get; set; }
        public byte Amount { get; set; }
    }
}
