namespace Petadmin.Models
{
    public class Backup
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
