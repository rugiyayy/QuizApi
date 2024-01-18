using FluentValidation;
using QuizAPI.DTOs.Questions;

namespace QuizAPI.DTOs.Options
{
    public class OptionGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

  
}
