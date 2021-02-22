using account.data.Models;
using Microsoft.EntityFrameworkCore;

namespace account.data
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get;set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
