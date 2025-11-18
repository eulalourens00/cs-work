using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CRUDoperations
{
    internal class ApplicationContext:DbContext
    {
        public DbSet<User>Users { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
            Database.EnsureDeleted();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = hell.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Ignore<Country>();

            //modelBuilder.Entity<User>().Property("Id").HasField("id");

            //modelBuilder.Entity<User>().HasKey(u => u.Id).HasName("PK_Id");

            modelBuilder.Entity<User>().ToTable(t => t.HasCheckConstraint("Name",
                "Len(name <= 50)"));

            modelBuilder.Entity<User>().Property(n => n.Name).HasMaxLength(30);


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Anna",
                    PhoneNumber = "111"
                });
        }
    }
}
