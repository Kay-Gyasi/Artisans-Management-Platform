using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Queries.UserManagement;

public class VerifyUser
{
    public class Query : IRequest<Result<bool>>
    {
        public string Code { get; }
        public string Phone { get; }

        public Query(string phone, string code)
        {
            Code = code;
            Phone = phone;
        }
    }

    public class Handler : IRequestHandler<Query, Result<bool>>
    {
        private readonly RegistrationProcessor _registrationProcessor;

        public Handler(RegistrationProcessor registrationProcessor)
        {
            _registrationProcessor = registrationProcessor;
        }
        
        public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _registrationProcessor.VerifyUser(request.Phone, request.Code);
        }
    }
}