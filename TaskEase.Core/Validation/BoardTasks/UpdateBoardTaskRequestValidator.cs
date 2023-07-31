using FluentValidation;
using TaskEase.Core.Contracts.Requests.BoardTasks;

namespace TaskEase.Core.Validation.BoardTasks;

internal sealed class UpdateBoardTaskRequestValidator : AbstractValidator<UpdateBoardTaskRequest>
{
    public UpdateBoardTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title must be between 1 and 50 characters.");
            
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.")
            .Length(1, 200).WithMessage("Description must be between 1 and 300 characters.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid Task Status.");
    }
}