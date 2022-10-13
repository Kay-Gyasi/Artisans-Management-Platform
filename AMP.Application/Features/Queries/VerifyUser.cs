using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using AMP.Processors.Repositories;
using MediatR;

namespace AMP.Application.Features.Queries;

public class VerifyUser
{
    public class Query : IRequest<bool>
    {
        public string Code { get; }
        public string Phone { get; }

        public Query(string phone, string code)
        {
            Code = code;
            Phone = phone;
        }
    }

    public class Handler : IRequestHandler<Query, bool>
    {
        private readonly RegistrationProcessor _registrationProcessor;

        public Handler(RegistrationProcessor registrationProcessor)
        {
            _registrationProcessor = registrationProcessor;
        }
        
        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _registrationProcessor.VerifyUser(request.Phone, request.Code);
        }
    }
}