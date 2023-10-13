namespace NewAPI.Models
{
    public class Video
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? UrlFileAws { get; set; }

        public string? ComplementaryMaterial { get; set; }

        public string? ComplementaryMaterialFileNameAws { get; set; }

        public string? Url { get; set; }

        public int Position { get; set; }

        public Guid CourseModuleId { get; set; }
    }
}
 