using FluentValidation;

namespace GameService.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommandValidation : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(200)
                .WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be non-negative.");

            RuleFor(x => x.PublisherId)
                .NotEmpty()
                .WithMessage("PublisherId is required.");

            RuleFor(x => x.GenreIds)
                .NotEmpty()
                .WithMessage("At least one genre must be specified.");
        }
    }
}
