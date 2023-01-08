﻿using AMP.Processors.PageDtos.UserManagement;
using AMP.Processors.Processors.UserManagement;
using AMP.Shared.Domain.Models;

namespace AMP.Application.Features.Queries.UserManagement
{
    public class GetUserPage
    {
        public class Query : IRequest<PaginatedList<UserPageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<UserPageDto>>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<UserPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }
    }
}