﻿using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;

namespace AMP.Application.Features.Commands.UserManagement
{
    public class PostUser
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
            private readonly RegistrationProcessor _processor;

            public Handler(RegistrationProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.UserCommand);
            }
        }
    }
}