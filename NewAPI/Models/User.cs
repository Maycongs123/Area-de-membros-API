
namespace NewAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? ProfileImageBase64 { get; set; }

        public bool IsAdm { get; set; }

        public List<Course>? Courses { get; set; }

        public List<Notification>? Notifications { get; set; }
    }
}
