using System.Collections.Generic;
using System.Xml.Serialization;

namespace SmsSender.Models
{
    [XmlRoot(ElementName = "ClientNotification")]
    public class ClientNotification
    {
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "BatchSize")]
        public int BatchSize { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
