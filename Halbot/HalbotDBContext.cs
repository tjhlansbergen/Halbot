using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Halbot.Code;

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
