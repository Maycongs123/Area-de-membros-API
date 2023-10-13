namespace NewAPI.Models
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Position { get; set; }

        public List<User>? Users { get; set; }

        public List<CourseModule>? CourseModule { get; set; }
    }
}
