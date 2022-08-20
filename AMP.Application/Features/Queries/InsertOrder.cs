using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using AMP.Processors.Responses;
using AutoMapper.Configuration;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class InsertOrder
    {
        public class Query : IRequest<InsertOrderResponse>
        {
            public OrderCommand Command { get; }

            public Query(OrderCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, InsertOrderResponse>
        {
            private readonly OrderProcessor _processor;

            public Handler(OrderProcessor processor)
            {
                _processor = processor;
            }
            public async Task<InsertOrderResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Insert(request.Command);
            }
        }
    }
}
