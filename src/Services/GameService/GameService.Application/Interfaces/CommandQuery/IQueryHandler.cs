using GameService.Application.Shared;
using MediatR;

namespace GameService.Application.Interfaces.CommandQuery
{
    public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    { }
}
