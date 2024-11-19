using Application.Repositories.UserRepo;
using Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phone_Store.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly MyContext _context;

        public AuthenticationController(IUserRepo userRepo, MyContext context)
        {
            _userRepo = userRepo;
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string UserName , string Password)
        {
            var response = await _userRepo.Login(UserName, Password);
            return Ok(response);

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string UserName, string Password)
        {
            var response = await _userRepo.Register(UserName, Password);
            return Ok(response);

        }

        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateNewToken(string Token, string RefreshToken)
        {
            var response = await _userRepo.GenerateNewToken(Token, RefreshToken);
            return Ok(response);

        }
    }
}
