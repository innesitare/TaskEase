using FluentValidation;
using TaskEase.Core.Contracts.Requests.Users;
using TaskEase.Core.Validation.BoardTasks;

namespace TaskEase.Core.Validation.Users;

internal sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(1, 50).WithMessage("Name must be between 1 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage("LastName is required.")
            .Length(1, 50).WithMessage("LastName must be between 1 and 50 characters.");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.")
            .Length(1, 100).WithMessage("Email must be between 1 and 100 characters.");

        RuleForEach(x => x.BoardTasks).SetValidator(new UpdateBoardTaskRequestValidator());
    }
}