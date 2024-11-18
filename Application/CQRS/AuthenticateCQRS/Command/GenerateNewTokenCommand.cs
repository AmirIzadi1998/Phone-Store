using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.NotificationsCQRS;
using Core.Context;
using Infrastructure;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.CQRS.AuthenticateCQRS.Command
{
    public class GenerateNewTokenCommand : IRequest<GenerateNewTokenCommandResponse>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class GenerateNewTokenCommandResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class
        GenerateNewTokenCommandHandler : IRequestHandler<GenerateNewTokenCommand, GenerateNewTokenCommandResponse>
    {
        private readonly MyContext _context;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly IMediator _mediator;
        private readonly Configs _configs;

        public GenerateNewTokenCommandHandler(MyContext context, EncryptionUtility encryption, IMediator mediator, IOptions<Configs> configs)
        {
            _context = context;
            _encryptionUtility = encryption;
            _mediator = mediator;
            _configs = configs.Value;
        }
        public async Task<GenerateNewTokenCommandResponse> Handle(GenerateNewTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.UserRefreshTokens.SingleOrDefaultAsync(
                x => x.RefreshToken == request.RefreshToken);

            var token = _encryptionUtility.GetNewToken(user.UserId);
            var refreshtoken = _encryptionUtility.GetRefreshToken();

            var addrefreshtokennotification = new AddRefreshTokenNotification()
            {
                RefreshToken = refreshtoken,
                RefreshTokenTimeOut = _configs.RefreshTokenTimeOut,
                UserId = user.UserId
            };
            await _mediator.Publish(addrefreshtokennotification);

            var response = new GenerateNewTokenCommandResponse
            {
                RefreshToken = addrefreshtokennotification.RefreshToken,
                Token = token
            };
            return response;
        }
    }
}
