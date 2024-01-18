using FluentValidation;
using QuizAPI.DTOs.Questions;
using static QuizAPI.DTOs.Questions.QuestionPostDto;

namespace QuizAPI.DTOs.Quzzes
{
    public class QuizPostDto
    {
         public string Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public List<QuestionPostDto> Questions { get; set; } 
    }



    public class QuizPostDtoValidator : AbstractValidator<QuizPostDto>
    {
        public QuizPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Quiz name is required!")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Quiz name must not consist only of white spaces!")
                .Length(3, 100).WithMessage("Quiz name can't be less than 3 characters and more than 100 characters!");


            RuleFor(dto => dto.CreationDate)
                .Must(BeWithin10MinutesOfUtcNow).WithMessage("The CreationDate must be within 10 minutes of the current UTC time.");
            //.Equal(DateTime.UtcNow).WithMessage("The CreationDate must be equal to the current UTC time.");
            //.LessThanOrEqualTo(DateTime.UtcNow)
            //.WithMessage("CreationDate must not be in the future.");

            RuleFor(x => x.Questions)
                .NotNull().WithMessage("At least one question is required!")
                .ForEach(question => question.SetValidator(new QuestionPostDtoValidator()));

        }

        private bool BeWithin10MinutesOfUtcNow(DateTime creationDate)
        {
            var utcNow = DateTime.UtcNow;
            var difference = Math.Abs((utcNow - creationDate).TotalMinutes);
            return difference <= 20;
        }
    }
}
