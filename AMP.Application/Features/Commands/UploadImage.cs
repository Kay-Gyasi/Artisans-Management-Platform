using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AMP.Application.Features.Commands
{
    public class UploadImage
    {
        public class Command : IRequest
        {
            public string UserId { get; }
            public IEnumerable<IFormFile> Files { get; }

            public Command(IEnumerable<IFormFile> files, string userId)
            {
                UserId = userId;
                Files = files;
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ImageProcessor _processor;

            public Handler(ImageProcessor processor)
            {
                _processor = processor;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var command = new ImageCommand
                {
                    UserId = request.UserId,
                    Image = request.Files.FirstOrDefault()
                };
                await _processor.UploadImage(command);
                return Unit.Value;
            }
        }
    }
}