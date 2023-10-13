namespace NewAPI.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public Guid CourseModuleId { get; set; }

        public Guid UserId { get; set; }

        public string? DoubtFileNameAws { get; set; }

        public string? ProfileImageBase64 { get; set; }

        public string? UserName { get; set; }

        public string Text { get; set; }

        public bool IsDoubt { get; set; }

        public bool IsResponse { get; set; }

        public Guid? IdRootComment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
