using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Processors;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AMP.Application.Features.Commands
{
    public class UploadImage
    {
        public class Command : IRequest
        {
            public int UserId { get; }
            public IFormFile File { get; }

            public Command(IFormFile file, int userId)
            {
                UserId = userId;
                File = file;
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
                var command = new ImageCommand()
                {
                    UserId = request.UserId,
                    Image = request.File
                };
                await _processor.UploadImage(command);
                return Unit.Value;
            }
        }
    }
}