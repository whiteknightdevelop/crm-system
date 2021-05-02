namespace SmsSender.Interfaces
{
    public interface ISms
    {
        string Id { get; set; }
        string Message { get; set; }
        string Recipients { get; set; }
        string Category { get; set; }
        int MessageInterval { get; set; }
    }
}
