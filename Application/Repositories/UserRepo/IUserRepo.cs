using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dto;
using Infrastructure.Entities;

namespace Application.Repositories.UserRepo
{
    public interface IUserRepo
    {
        Task<RefreshTokenDto> Login(string UserName, string Password);
        Task<string> Register(string UserName, string Password);
        Task<string> Generate(string RefreshToken, Guid UserId, int RefreshTokenTimeOut);
        Task<RefreshTokenDto> GenerateNewToken(string Token, string RefreshToken);
    }
}