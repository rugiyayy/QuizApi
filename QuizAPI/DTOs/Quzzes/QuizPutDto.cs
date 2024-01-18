using FluentValidation;

namespace QuizAPI.DTOs.Quzzes
{
    public class QuizPutDto
    {
        public string Name { get; set; }
    }


    public class QuizPutDtoValidator : AbstractValidator<QuizPutDto>
    {
        public QuizPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Quiz name is required!")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Quiz name must not consist only of white spaces!")
                .Length(3, 100).WithMessage("Quiz name can't be less than 3 characters and more than 100 characters!");

        }
    }
}
