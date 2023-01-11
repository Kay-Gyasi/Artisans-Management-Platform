﻿using AMP.Processors.Processors.BusinessManagement;

namespace AMP.Application.Features.Commands.BusinessManagement
{
    public class DeleteDispute
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
            private readonly DisputeProcessor _processor;

            public Handler(DisputeProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Delete(request.Id);
            }
        }
    }
}