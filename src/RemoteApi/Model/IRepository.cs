namespace RemoteApi.Model
{
    public interface IRepository
    {
        Task<ICollection<Concert>> SearchConcertsAsync(string? searchPattern, string[] filters);
        Task<ICollection<Concert>> AllConcertsAsync();
        Task<ICollection<ClassicConcert>> AllClassicConcertsAsync();
        Task<ICollection<OpenAir>> AllOpenAirsAsync();
        Task<ICollection<Party>> AllPartiesAsync();
        Task<Concert?> GetConcertByIdAsync(long id);
        Task<ClassicConcert?> GetClassicConcertByIdAsync(long id);
        Task<OpenAir?> GetOpenAirByIdAsync(long id);
        Task<Party?> GetPartyByIdAsync(long id);
        Task<ICollection<Concert>> GetConcertsByNameAsync(string pattern);
        Task CreatePartyAsync(Party party);
        Task CreateOpenAirAsync(OpenAir openAir);
        Task CreateClassicConcertAsync(ClassicConcert classicConcert);
    }
}
