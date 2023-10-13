using System.ComponentModel.DataAnnotations;

namespace NewAPI.Models
{
    public class UserVideoProgress
    {
        public Guid Id { get; set; }

        public Guid VideoId { get; set; }

        public Guid CourseModuleId { get; set; }

        public Guid UserId { get; set; }

        public bool IsWatched { get; set; }
    }
}
