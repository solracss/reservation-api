using FluentValidation;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using System.Text.RegularExpressions;

namespace ReservationAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Custom((email, validationContext) =>
                 {
                     var emailAlreadyTaken = dataContext.Users.Any(x => x.Email == email);
                     if (emailAlreadyTaken)
                     {
                         validationContext.AddFailure("Email already taken.");
                     }
                 });

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Custom((date, validationContext) =>
                {
                    if (date > DateTime.Now)
                    {
                        validationContext.AddFailure("Invalid Date of Birth.");
                    }
                });

            RuleFor(x => x.Password)
                .Custom((password, validationContext) =>
                {
                    var regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#@$!%*?&.])[A-Za-z\d@#$!%*?&.]{8,}$";
                    var validPassword = Regex.IsMatch(password, regex);
                    if (!validPassword)
                    {
                        validationContext.AddFailure("Password must contain contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:");
                    }
                });
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
