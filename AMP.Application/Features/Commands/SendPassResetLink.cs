﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands;

public class SendPassResetLink
{
    public class Command : IRequest<Result<bool>>
    {
        public string Phone { get; }

        public Command(string phone)
        {
            Phone = phone;
        }
    }

    public class Handler : IRequestHandler<Command, Result<bool>>
    {
        private readonly UserProcessor _processor;

        public Handler(UserProcessor processor)
        {
            _processor = processor;
        }
        public async Task<Result<bool>> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _processor.SendPasswordResetLink(request.Phone);
        }
    }
}