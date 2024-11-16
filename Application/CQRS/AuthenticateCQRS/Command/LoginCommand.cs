using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        public string Tokes { get; set; }
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly MyContext _context;
        private readonly EncryptionUtility _encryptionUtility;

        public LoginCommandHandler(MyContext context, EncryptionUtility encryptionUtility)
        {
            _context = context;
            _encryptionUtility = encryptionUtility;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null) throw new Exception();

            var hashpassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
            if (user.Password != hashpassword) throw new Exception();

            var token = _encryptionUtility.GetNewToken(user.Id);
            var response = new LoginCommandResponse()
            {
                UserName = user.UserName,
                Tokes = token
            };
            return response;
        }
    }
}