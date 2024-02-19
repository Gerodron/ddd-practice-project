using App.Domain.Shared;
using MediatR;

namespace App.Application.Abstraccions.Messaging
{
    public interface IQuery<TReponse> : IRequest<Result<TReponse>>
    {
    }
}
