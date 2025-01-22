using GameService.Application.Shared;
using MediatR;

namespace GameService.Application.Interfaces.CommandQuery
{
    public interface IQuery : IRequest<Result> { }

    public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
}
