﻿using App.Domain.Shared;
using MediatR;

namespace App.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
        public Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
        public Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
