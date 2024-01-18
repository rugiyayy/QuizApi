using QuizAPI.DTOs.Questions;

namespace QuizAPI.DTOs.Quzzes
{
    public class QuizDetailedGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow; 
        public List<QuestionGetDto> Questions { get; set; }
    }
}
