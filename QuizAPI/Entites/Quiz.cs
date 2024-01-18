namespace QuizAPI.Entites
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.UtcNow;

        public List<Question> Questions { get; set; }

    }
}
