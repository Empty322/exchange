using System;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class UsersContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
