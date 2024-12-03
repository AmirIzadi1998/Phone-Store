using Application.CQRS.AuthenticateCQRS.Command;
using Infrastructure.Dto;
using Infrastructure.Entities;
using Application.CQRS.NotificationsCQRS;
using AutoMapper;
using Core.Context;
using Infrastructure;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.Repositories.UserRepo
{
    public class UserRepo : IUserRepo
    {
        private readonly MyContext _context;
        private readonly IMapper _mapper;
        private readonly Configs _configs;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly IMediator _mediator;

        public UserRepo(MyContext context, IMapper mapper, IOptions<Configs> configs, EncryptionUtility encryptionUtility, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _configs = configs.Value;
            _encryptionUtility = encryptionUtility;
            _mediator = mediator;
        }

        public async Task<string> Register(string UserName, string Password)
        {
            var test = await _context.Users.SingleOrDefaultAsync(x => x.UserName == UserName);
            if (test == null)
            {
                var salt = _encryptionUtility.GetNewSalt();
                var hashpassword = _encryptionUtility.GetSHA256(Password, salt);


                var user = new UserDto()
                {
                    Id = new Guid(),
                    UserName = UserName,
                    Password = hashpassword,
                    PasswordSalt = salt,
                    RegisterDate = DateTime.Now
                };
                var map = _mapper.Map<User>(user);


                await _context.Users.AddAsync(map);
                await _context.SaveChangesAsync();

                var g = "Successfully";
                return g;
            }
            else
            {
                var b = "Your username is duplicate";
                throw new Exception($"{b}");
            }
           
        }

        public async Task<RefreshTokenDto> Login(string UserName, string Password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == UserName);
            if (user == null) throw new Exception();

            var hashpassword = _encryptionUtility.GetSHA256(Password, user.PasswordSalt);
            if (user.Password != hashpassword) throw new Exception();

            var token = _encryptionUtility.GetNewToken(user.Id);
            var refreshtoken = _encryptionUtility.GetRefreshToken();

            var addrefreshtoken = new AddRefreshTokenNotification()
            {
                RefreshToken = refreshtoken,
                RefreshTokenTimeOut = _configs.RefreshTokenTimeOut,
                UserId = user.Id
            };
            await _mediator.Publish(addrefreshtoken);

            var response = new RefreshTokenDto()
            {
                UserName = user.UserName,
                Token = token,
                RefreshToken = refreshtoken,
            };

            return response;
        }

        public async Task Generate(string RefreshToken, Guid UserId, int RefreshTokenTimeOut)
        {
            var result = new UserRefreshTokenDto()
            {
                RefreshToken = RefreshToken,
                RefreshTokenTimeOut = RefreshTokenTimeOut,
                UserId = UserId
            };

            var check = await _context.UserRefreshTokens.SingleOrDefaultAsync(x => x.UserId == result.UserId);
            if (check == null)
            {
                await _context.AddAsync(result);
            }
            else
            {
                check.RefreshToken = result.RefreshToken;
                check.RefreshTokenTimeOut = result.RefreshTokenTimeOut;
                check.CreateDate = DateTime.Now;
                check.Isvalid = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshTokenDto> GenerateNewToken(string Token, string RefreshToken)
        {
            var user = await _context.UserRefreshTokens.SingleOrDefaultAsync(
                x => x.RefreshToken == RefreshToken);

            var token = _encryptionUtility.GetNewToken(user.UserId);
            var refreshtoken = _encryptionUtility.GetRefreshToken();

            var addrefreshtoken = new AddRefreshTokenNotification()
            {
                RefreshToken = refreshtoken,
                RefreshTokenTimeOut = _configs.RefreshTokenTimeOut,
                UserId = user.UserId
            };
            await _mediator.Publish(addrefreshtoken);

            var response = new RefreshTokenDto
            {
                RefreshToken = addrefreshtoken.RefreshToken,
                Token = token
            };
            return response;
        }
    }
}