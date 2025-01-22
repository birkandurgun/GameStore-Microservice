using FluentValidation;

namespace GameService.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommandValidation : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidation()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("PublisherId is required.");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("At least one GenreId is required.");
        }
    }
}
