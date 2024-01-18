namespace QuizAPI.DTOs.Quzzes
{
    public class QuizGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
