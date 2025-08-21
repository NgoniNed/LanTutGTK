using System;
using System.Data;
using System.Linq;
using LanTutor.DataModels;
using Microsoft.EntityFrameworkCore;

namespace LanTutor.Database
{
    public class LanTutorContext : DbContext
    {
        public DbSet<User> Users
        {
            get;
            set;
        }
        public DbSet<Session> Sessions
        {
            get;
            set;
        }
        public DbSet<WordTransDef> Words
        {
            get;
            set;
        }
        public DbSet<Definition> Definitions
        {
            get;
            set;
        }
        public DbSet<Score> Scores
        {
            get;
            set;
        }
        public DbSet<WordScore> WordScores
        {
            get;
            set;
        }
        public DbSet<DescriptionScore> DescriptionScores
        {
            get;
            set;
        }
        public LanTutorContext()
        {

        }

        public LanTutorContext(DbContextOptions<LanTutorContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=LanTutor.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordTransDef>()
                .HasOne(w => w.lWordScore)
                .WithOne(s => s.WordTransDef)
                .HasForeignKey<WordScore>(s => s.WordTransDefId);

            modelBuilder.Entity<WordTransDef>()
                .HasOne(w => w.lDescriptionScore)
                .WithOne(d => d.WordTransDef)
                .HasForeignKey<DescriptionScore>(d => d.WordTransDefId);

            base.OnModelCreating(modelBuilder);
        }
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
