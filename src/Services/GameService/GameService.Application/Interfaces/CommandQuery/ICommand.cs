using GameService.Application.Shared;
using MediatR;

namespace GameService.Application.Interfaces.CommandQuery
{
    public interface ICommand : IRequest<Result> { }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
}
