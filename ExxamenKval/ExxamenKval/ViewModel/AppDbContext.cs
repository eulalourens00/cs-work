using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExxamenKval.Models;
namespace ExxamenKval.ViewModel
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourOperator> TourOperators { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // в папке с .exe
            optionsBuilder.UseSqlite("Data Source=TourAgency.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // тест
            modelBuilder.Entity<TourOperator>().HasData(
                new TourOperator { Id = 1, Name = "Pegas Touristik", INN = "7712345678", Phone = "8-800-555-01-01" },
                new TourOperator { Id = 2, Name = "Coral Travel", INN = "7722334455", Phone = "8-800-555-02-02" }
            );

            modelBuilder.Entity<Tour>().HasData(
                new Tour { Id = 1, Name = "Выходные в Сочи", Country = "Россия", City = "Сочи", HotelName = "Radisson", NightsCount = 3, PricePerAdult = 25000, PricePerChild = 15000, TourOperatorId = 1 },
                new Tour { Id = 2, Name = "Пляжный Египет", Country = "Египет", City = "Хургада", HotelName = "Sunrise", NightsCount = 7, PricePerAdult = 60000, PricePerChild = 40000, TourOperatorId = 2 }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, FullName = "Иван Петров", PassportNumber = "4510 123456", Phone = "+7-999-111-22-33", BirthDate = new DateTime(1990, 05, 15), IsVip = false },
                new Client { Id = 2, FullName = "Мария Смирнова", PassportNumber = "4510 654321", Phone = "+7-999-444-55-66", BirthDate = new DateTime(1985, 10, 20), IsVip = true }
            );
        }
    }
}
