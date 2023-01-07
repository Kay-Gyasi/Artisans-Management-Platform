using AMP.Processors.PageDtos;
using AMP.Shared.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.PageDtos.UserManagement;
using AMP.Processors.Processors;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries
{
    public class GetArtisanPage
    {
        public class Query : IRequest<PaginatedList<ArtisanPageDto>>
        {
            public PaginatedCommand Command { get; }

            public Query(PaginatedCommand command)
            {
                Command = command;
            }
        }

        public class Handler : IRequestHandler<Query, PaginatedList<ArtisanPageDto>>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<PaginatedList<ArtisanPageDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.GetPage(request.Command);
            }
        }

        //public sealed class PaginatedCommandValidator : AbstractValidator<PaginatedCommand>
        //{
        //    public PaginatedCommandValidator()
        //    {
        //        RuleFor(x => x.PageNumber).NotEmpty();

        //        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);

        //        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        //    }
        //}
    }
}