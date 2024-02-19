using App.Domain.Shared;
using MediatR;

namespace App.Application.Abstraccions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IQuery<TResponse>

    {
    }
}
