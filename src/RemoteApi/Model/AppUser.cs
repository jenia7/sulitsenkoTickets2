namespace RemoteApi.Model
{
    public class AppUser
    {
        public string Sub { get; set; } = string.Empty;

        public List<Concert> Concerts { get; } = new();
    }
}
