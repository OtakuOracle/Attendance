using Microsoft.EntityFrameworkCore;
using presence.data.RemoteData.RemoteDataBase.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.RemoteData.RemoteDataBase
{
    public class RemoteDatabaseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=45.67.56.214;" +
                "Port=5421;" +
                "Username=user10;" +
                "Database=user10;" +
                "Password=FY1rOnvu");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<GroupDao>().HasKey(group => group.Id);
            modelBuilder.Entity<GroupDao>().Property(group => group.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserDao>().HasKey(user => user.UserId);
            modelBuilder.Entity<UserDao>().Property(user => user.UserId).ValueGeneratedOnAdd();

            modelBuilder.Entity<PresenceDao>().HasKey(presence => presence.PresenceId);
            modelBuilder.Entity<PresenceDao>().Property(presence => presence.PresenceId).ValueGeneratedOnAdd();
            modelBuilder.Entity<PresenceDao>()
               .HasOne(presence => presence.User)
               .WithMany(user => user.Presences)
               .HasForeignKey(presence => presence.UserId);
        }


        public DbSet<GroupDao> Groups { get; set; }
        public DbSet<UserDao> Users { get; set; }
        public DbSet<PresenceDao> Presences { get; set; }
    }
}