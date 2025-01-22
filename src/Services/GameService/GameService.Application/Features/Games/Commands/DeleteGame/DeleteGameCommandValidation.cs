using FluentValidation;

namespace GameService.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommandValidation : AbstractValidator<DeleteGameCommand>
    {
        public DeleteGameCommandValidation()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required.");
        }
    }
}
