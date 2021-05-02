using System.Xml.Serialization;

namespace SmsSender.Models
{
    [XmlRoot(ElementName = "Messages")]
    public class Messages
    {
        [XmlElement(ElementName = "Message")]
        public Message[] Message { get; set; }    
    }
}
