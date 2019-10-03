using Halbot.Code;
using Microsoft.EntityFrameworkCore;

namespace Halbot
{
    public class HalbotDBContext : DbContext
    {
        public DbSet<HalbotActivity> DBActivities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./App_Data/HalbotActivities.db");
        }
    }
}