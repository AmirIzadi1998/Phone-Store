using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.CQRS.NotificationsCQRS;
using Core.Context;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.CQRS.AuthenticateCQRS.Command
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly MyContext _context;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly IMediator _mediator;
        private readonly Configs _configs;

        public LoginCommandHandler(MyContext context, EncryptionUtility encryptionUtility, IMediator mediator, IOptions<Configs> configs)
        {
            _context = context;
            _encryptionUtility = encryptionUtility;
            _mediator = mediator;
            _configs = configs.Value;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null) throw new Exception();

            var hashpassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
            if (user.Password != hashpassword) throw new Exception();

            var token = _encryptionUtility.GetNewToken(user.Id);
            var refreshtoken = _encryptionUtility.GetRefreshToken();

            var addrefreshtokennotification = new AddRefreshTokenNotification()
            {
                RefreshToken = refreshtoken,
                RefreshTokenTimeOut = _configs.RefreshTokenTimeOut,
                UserId = user.Id
            };
            await _mediator.Publish(addrefreshtokennotification);

            var response = new LoginCommandResponse()
            {
                UserName = user.UserName,
                Token = token,
                RefreshToken = refreshtoken,
                
            };
            return response;
        }
    }
}