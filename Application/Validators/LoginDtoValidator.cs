using Contracts.Dto;
using FluentValidation;

namespace Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email should not be empty");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Password should not be empty");
        }
    }
}
