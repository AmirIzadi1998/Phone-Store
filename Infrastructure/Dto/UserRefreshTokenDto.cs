using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Infrastructure.Dto
{
    public class UserRefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenTimeOut { get; set; }
    }
}
