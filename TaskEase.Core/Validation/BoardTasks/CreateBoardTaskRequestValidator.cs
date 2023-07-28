using FluentValidation;
using TaskEase.Core.Contracts.Requests.BoardTasks;

namespace TaskEase.Core.Validation.BoardTasks;

internal sealed class CreateBoardTaskRequestValidator : AbstractValidator<CreateBoardTaskRequest>
{
    public CreateBoardTaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");
            
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Description is required.")
            .Length(1, 200).WithMessage("Description must be between 1 and 200 characters.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid Task Status.");
    }
}