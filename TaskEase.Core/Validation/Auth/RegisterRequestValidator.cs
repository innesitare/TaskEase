using FluentValidation;
using TaskEase.Core.Contracts.Requests.Auth;

namespace TaskEase.Core.Validation.Auth;

internal sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(1, 50).WithMessage("Last name must be between 1 and 50 characters.");

        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(3, 15).WithMessage("Username must be between 3 and 15 characters.");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid Email Address.")
            .Length(1, 100).WithMessage("Email must be between 1 and 100 characters.");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(@"^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\D*\d)(?=[^!#%]*[!#%])[A-Za-z0-9!#%]{6,32}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one number, one special character and be between 6 and 32 characters.");
    }
}