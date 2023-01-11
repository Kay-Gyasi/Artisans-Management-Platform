﻿using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement
{
    public class UpdateUser
    {
        public class Command : IRequest<Result<string>>
        {
            public UserCommand UserCommand { get; }

            public Command(UserCommand artisanCommand)
            {
                UserCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly UserProcessor _processor;

            public Handler(UserProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Update(request.UserCommand);
            }
        }
    }
}