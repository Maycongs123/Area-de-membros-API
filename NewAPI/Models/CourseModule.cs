using System;

namespace NewAPI.Models
{
    public class CourseModule
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? CoverImage { get; set; }

        public int Position { get; set; }

        public bool IsEnable { get; set; }

        public List<Video>? Videos { get; set; }

        public List<Comment>? Comments { get; set; }

        public Guid CourseId { get; set; }
    }
}
