﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;

namespace AMP.Application.Features.Commands
{
    public class SaveArtisan
    {
        public class Command : IRequest<int>
        {
            public ArtisanCommand ArtisanCommand { get; }

            public Command(ArtisanCommand artisanCommand)
            {
                ArtisanCommand = artisanCommand;
            }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ArtisanProcessor _processor;

            public Handler(ArtisanProcessor processor)
            {
                _processor = processor;
            }
            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _processor.Save(request.ArtisanCommand);
            }
        }
    }
}