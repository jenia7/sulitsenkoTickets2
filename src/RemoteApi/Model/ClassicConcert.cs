namespace RemoteApi.Model
{
    public class ClassicConcert : Concert
    {
        public string Conductor { get; set; } = string.Empty;
        public VocalistVoice VocalistVoice { get; set; }
    }
}
