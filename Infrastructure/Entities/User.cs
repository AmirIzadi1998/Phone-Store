using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required,MaxLength(64)]
        public string UserName { get; set; }
        [Required,MaxLength(64)]
        public string Password { get; set; }
        [MaxLength(64)]
        public string PasswordSalt { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
