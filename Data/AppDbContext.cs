using Microsoft.EntityFrameworkCore;
using yourmomENT.Models;

namespace yourmomENT.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}