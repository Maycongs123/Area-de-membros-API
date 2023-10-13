namespace NewAPI.Models
{
    public class Banner
    {
        public Guid? Id { get; set; }

        public string ImageBase64 { get; set; }

        public Guid? UserId { get; set; }
    }
}
