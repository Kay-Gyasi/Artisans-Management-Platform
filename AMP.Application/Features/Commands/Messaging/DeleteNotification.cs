﻿using AMP.Processors.Processors.Messaging;

namespace AMP.Application.Features.Commands.Messaging;

public class DeleteNotification
{
    public class Command : IRequest<Result<bool>>
    {
        public string Id { get; }

        public Command(string id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly NotificationProcessor _processor;

        public Handler(NotificationProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.Delete(request.Id);
        }
    }
}