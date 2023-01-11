﻿using AMP.Processors.PageDtos.BusinessManagement;
using AMP.Processors.Processors.BusinessManagement;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.BusinessManagement
{
    public class GetServicePage
    {
        public class Query : IRequest<PaginatedList<ServicePageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<ServicePageDto>>
        {
            private readonly ServiceProcessor _processor;

            public Handler(ServiceProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<ServicePageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }
    }
}