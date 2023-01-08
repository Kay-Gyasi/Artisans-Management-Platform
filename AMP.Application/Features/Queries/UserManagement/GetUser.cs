using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries.UserManagement
{
    public class GetUser
    {
        public class Query : IRequest<Result<UserDto>>
        {
            public string Id { get; }

            public Query(string id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _processor.Get(request.Id);
            }
        }
    }
}