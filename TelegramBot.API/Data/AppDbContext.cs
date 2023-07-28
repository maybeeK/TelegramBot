using Microsoft.EntityFrameworkCore;
using TelegramBot.API.Entities;

namespace TelegramBot.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
    }
}
