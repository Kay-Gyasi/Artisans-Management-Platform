using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Processors.UserManagement;
using Microsoft.AspNetCore.Http;

namespace AMP.Application.Features.Commands.UserManagement
{
    public class UploadImage
    {
        public class Command : IRequest<SigninResponse>
        {
            public string UserId { get; }
            public IFormFile File { get; }

            public Command(IFormFile file, string userId)
            {
                UserId = userId;
                File = file;
            }
        }

        public class Handler : IRequestHandler<Command, SigninResponse>
        {
            private readonly ImageProcessor _processor;

            public Handler(ImageProcessor processor)
            {
                _processor = processor;
            }
            public async Task<SigninResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var command = new ImageCommand
                {
                    UserId = request.UserId,
                    Image = request.File
                };
                return await _processor.UploadImage(command);
            }
        }
    }
}