using FluentValidation;
using QuizAPI.DTOs.Questions;

namespace QuizAPI.DTOs.Options
{
    public class OptionPostDto
    {
        public string Name { get; set; }
        public bool IsCorrect { get; set; }




        public class OptionPostDtoValidator : AbstractValidator<OptionPostDto>
        {
            public OptionPostDtoValidator()
            {
                RuleFor(x => x.Name)
                    .NotNull().WithMessage("Option name is required!")
                .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Option name must not consist only of white spaces!")

                    .Length(3, 100).WithMessage("Option name can't be less than 3 characters and more than 100 characters!");

               RuleFor(x => x.IsCorrect)
                    .Must(x => x == false || x == true)
                    .NotNull().WithMessage("IsCorrect field is  required!");
            }
        }


    }
}
