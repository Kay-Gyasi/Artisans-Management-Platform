using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos;
using AMP.Processors.PageDtos.UserManagement;
using AMP.Processors.Processors;
using AMP.Processors.Processors.UserManagement;
using AMP.Shared.Domain.Models;
using MediatR;

namespace AMP.Application.Features.Queries
{
    public class GetCustomerPage
    {
        public class Query : IRequest<PaginatedList<CustomerPageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<GetCustomerPage.Query, PaginatedList<CustomerPageDto>>
        {
            private readonly CustomerProcessor _processor;

            public Handler(CustomerProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<CustomerPageDto>> Handle(GetCustomerPage.Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }
    }
}