﻿using Halbot.Data.Records;
using Microsoft.EntityFrameworkCore;

namespace Halbot.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ActivityRecord> ActivityRecords { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }

        public DbSet<PlanRecord> PlanRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./App_Data/Halbot.db");
        }
    }
}