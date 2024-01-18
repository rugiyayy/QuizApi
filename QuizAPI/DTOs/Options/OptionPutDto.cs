using FluentValidation;
using QuizAPI.DTOs.Quzzes;

namespace QuizAPI.DTOs.Options
{
    public class OptionPutDto
    {
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
    }


    public class OptionPutDtoValidator : AbstractValidator<OptionPutDto>
    {
        public OptionPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Quiz name is required!")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Option name must not consist only of white spaces!")
                .Length(3, 100).WithMessage("Quiz name can't be less than 3 characters and more than 100 characters!");

            RuleFor(x => x.IsCorrect)
                .Must(x => x == false || x == true).WithMessage("IsCorrect field must be either true or false!")
                .NotNull().WithMessage("IsCorrect field is  required!");
                //.NotEmpty().WithMessage("IsCorrect field is  required!");
        }

    }
}
