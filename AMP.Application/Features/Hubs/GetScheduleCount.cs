using System;
using AMP.Processors.Hubs.Enums;

namespace AMP.Application.Features.Hubs;

public class GetCount
{
    public class Command : IRequest<int>
    {
        public DataCountType Type { get; }
        public string UserId { get; }

        public Command(DataCountType type, string userId)
        {
            Type = type;
            UserId = userId;
        }
    }

    public class Handler : IRequestHandler<Command, int>
    {
        private readonly OrderProcessor _processor;

        public Handler(OrderProcessor processor)
        {
            _processor = processor;
        }
        
        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            return request.Type switch
            {
                DataCountType.Schedule => await _processor.GetScheduleCount(request.UserId),
                DataCountType.JobRequests => 0,
                DataCountType.Payments => 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}