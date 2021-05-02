using SmsSender.Interfaces;

namespace SmsSender.Models
{
    public class InforuSms : ISms
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Recipients { get; set; }
        public string Category { get; set; }
        public int MessageInterval { get; set; } = 0;
    }
}
