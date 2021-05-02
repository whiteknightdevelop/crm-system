using System;

namespace Petadmin.Core.Models
{
    public class SmsAppointment
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
