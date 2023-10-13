namespace NewAPI.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsViewed { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
