using FluentValidation;

namespace QuizAPI.DTOs.Account
{
    public class SignUpDto //register
    {
        public required string FirstName { get; set; }   //required helps to get no warning 
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool IsAdmin { get; set; }//role
        public required bool IsUser { get; set; }//role
        public required bool IsAnOrdinaryUser { get; set; }//an ordinary use r

       public class SignUpDtoValidator : AbstractValidator<SignUpDto>
        {
            public SignUpDtoValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotNull().WithMessage("FirstName is required!")
                    .MinimumLength(3).WithMessage("FirstName can't be less than 3 characters!")
                    .MaximumLength(100).WithMessage("FirstName can't be more than 100 characters!");

                RuleFor(x => x.LastName)
                    .NotNull().WithMessage("LastName is required!")
                    .MinimumLength(3).WithMessage("LastName can't be less than 3 characters!")
                    .MaximumLength(100).WithMessage("LastName can't be more than 100 characters!");

                RuleFor(x => x.UserName)
                    .NotNull().WithMessage("UserName is required!")
                    .MinimumLength(3).WithMessage("UserName can't be less than 3 characters!")
                    .MaximumLength(100).WithMessage("UserName can't be more than 100 characters!");


                RuleFor(x => x.Email)
                    .NotNull().WithMessage("Email address is required!")
                    .EmailAddress().WithMessage("Invalid Email Address");


                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(4).WithMessage("Your password length must be at least 4.")
                    .MaximumLength(20).WithMessage("Your password length must not exceed 20.");
                //.Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                //.Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                //.Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                //.Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");



                RuleFor(x => x)
                    .Must(RoleValidation)
                    .WithMessage("Only one role can be true.");

            }

            private bool RoleValidation(SignUpDto dto)
            {
                return (dto.IsAdmin ? 1 : 0) + (dto.IsUser ? 1 : 0) + (dto.IsAnOrdinaryUser ? 1 : 0) == 1;
            }
        }
    }
}
////
///

