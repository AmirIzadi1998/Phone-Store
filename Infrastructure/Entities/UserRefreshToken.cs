using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        [MaxLength(128)]
        public string RefreshToken { get; set; }
        public int RefreshTokenTimeOut { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Isvalid { get; set; }
    }
}
