using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Infrastructure.Entities;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AuthenticateCQRS.Command
{
    public class RegisterCommand: IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly MyContext _context;
        private readonly EncryptionUtility _encryptionUtility;

        public RegisterCommandHandler(MyContext context, EncryptionUtility encryptionUtility)
        {
            _context = context;
            _encryptionUtility = encryptionUtility;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var test = await _context.Users.SingleOrDefaultAsync(x => x.UserName == request.UserName);
            if (test == null)
            {
                var salt = _encryptionUtility.GetNewSalt();
                var hashpassword = _encryptionUtility.GetSHA256(request.Password, salt);

                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    Password = hashpassword,
                    PasswordSalt = salt,
                    RegisterDate = DateTime.Now
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }

            throw new Exception("Your username is duplicate");

        }
    }
}
