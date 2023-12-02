namespace RemoteApi.Model
{
    public class ConcertInfo
    {
        public long Id { get; set; }
        public string Performer { get; set; } = string.Empty;
        public Location Location { get; set; } = new();
        public int TotalTickets { get; set; }
        public int SoldTickets { get; set; } // SoldTickets <=(less than) TotalTickets

        public long ConcertId { get; set; }
        //public Concert Concert { get; set; } = null!;
    }
}
