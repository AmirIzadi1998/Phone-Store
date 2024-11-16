using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<PhoneProduct> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
