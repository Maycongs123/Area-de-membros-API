namespace NewAPI.Models
{
    public class Doubt
    {
        public Guid Id { get; set; }
        public string IdCurso { get; set; }
        public int IdUser { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
