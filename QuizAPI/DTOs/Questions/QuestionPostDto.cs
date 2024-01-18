using FluentValidation;
using QuizAPI.DTOs.Options;
using static QuizAPI.DTOs.Options.OptionPostDto;

namespace QuizAPI.DTOs.Questions
{
    public class QuestionPostDto //add
    {
        public string Name { get; set; }
        public decimal Points { get; set; }

        public List<OptionPostDto> Options { get; set; }

        public class QuestionPostDtoValidator : AbstractValidator<QuestionPostDto>
        {
            public QuestionPostDtoValidator()
            {
                RuleFor(x => x.Name)
                  .NotNull().WithMessage("Question (name) is required!")
                  .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Question name must not consist only of white spaces!")
                  .Length(3, 100).WithMessage("Question can't be less than 3  more than 100  characters!");

                RuleFor(x => x.Points)
                    .NotNull().WithMessage("Points (name) is required!")
                    .Must(x => x > 0 && x <= 100).WithMessage("Point must be greater than 0 and less than or equal to 100")
                    //.GreaterThan(0).WithMessage( "Point must be greater than 0" )
                    .ScalePrecision(2, 4).WithMessage("Invalid number format or too many decimal places. Maximum 2 decimal places allowed.");


                RuleFor(x => x.Options)
                    .NotNull().WithMessage("At least one question is required!")
                    .ForEach(question => question.SetValidator(new OptionPostDtoValidator()));

            }
        }
    }
}
