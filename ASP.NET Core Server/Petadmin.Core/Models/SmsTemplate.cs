using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class SmsTemplate : IGenericDbEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Template { get; set; }
        public string Type { get; set; }
    }
}
