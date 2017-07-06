using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // for IdentityDbContext
using Microsoft.EntityFrameworkCore; // for DbContextOptionsBuilder
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Models
{
    // 负责同数据库打交道。
    public class LoginDemoDbContext : IdentityDbContext<User>
    {
        public LoginDemoDbContext(DbContextOptions<LoginDemoDbContext> options)
            : base(options) { }
    }
}
