using FluentValidation;
using TaskEase.Core.Contracts.Requests.Auth;

namespace TaskEase.Core.Validation.Auth;

internal sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotNull()
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(3, 15).WithMessage("Username must be between 3 and 15 characters.");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(@"^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\D*\d)(?=[^!#%]*[!#%])[A-Za-z0-9!#%]{6,32}")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one number, one special character and be between 6 and 32 characters.");
    }
}