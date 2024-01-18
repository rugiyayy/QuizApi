using FluentValidation;

namespace QuizAPI.DTOs.Account
{
    public class SignInDto //login
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }


        public class SignInDtoValidator : AbstractValidator<SignInDto>
        {
            public SignInDtoValidator()
            {
                RuleFor(x => x.UserName)
                   .NotNull().WithMessage("UserName is required!")
                   .MaximumLength(100).WithMessage("UserName can't be more than 100 characters!");

                RuleFor(x => x.Password)
                   .NotEmpty().WithMessage("Password cannot be empty")
                   .MaximumLength(20).WithMessage("Your password length must not exceed 20.");


            }
           

        }
    }
}
