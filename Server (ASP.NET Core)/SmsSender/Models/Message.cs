using System;
using System.Xml.Serialization;

namespace SmsSender.Models
{
    public class Message
    {
        [XmlElement(ElementName = "Type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement(ElementName = "Network")]
        public string Network { get; set; }

        [XmlElement(ElementName = "Status")]
        public int Status { get; set; }

        [XmlElement(ElementName = "StatusDescription")]
        public string StatusDescription { get; set; }

        [XmlElement(ElementName = "CustomerMessageId")]
        public int CustomerMessageId { get; set; }

        [XmlElement(ElementName = "CustomerParam")]
        public string CustomerParam { get; set; }

        [XmlElement(ElementName = "SenderNumber")]
        public string SenderNumber { get; set; }

        [XmlElement(ElementName = "SegmentsNumber")]
        public int SegmentsNumber { get; set; }

        [XmlElement(ElementName = "NotificationDate")]
        public DateTime NotificationDate { get; set; }

        [XmlElement(ElementName = "SentMessage")]
        public string SentMessage { get; set; }
    }
}
