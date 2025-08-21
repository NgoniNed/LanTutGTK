using System;
using System.Data;
using System.Linq;
using LanTutor.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LanTutor.Database
{
    public class LanTutorContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<WordTransDef> Words { get; set; }
        public DbSet<Definition> Definitions { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=LanTutor.db");

        public void SeedData()
        {
            if (!Users.Any())
            {
                Users.Add(new User
                {
                    Username = "ressned",
                    PasswordHash = "ressned_admin",
                    Role = "Admin"
                });

                SaveChanges();
            }
        }

    }

}
