using Microsoft.EntityFrameworkCore;

namespace BuildPage.Models
{
    public class DbContextModel : DbContext
    {
        public DbContextModel(DbContextOptions options) : base(options){ }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<CalendarModel> Calendar { get; set; }

    }
}
