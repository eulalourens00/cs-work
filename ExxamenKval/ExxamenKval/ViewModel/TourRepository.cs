using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExxamenKval.Models;
namespace ExxamenKval.ViewModel
{
    public class TourRepository
    {
        private readonly AppDbContext _context;

        public TourRepository()
        {
            _context = new AppDbContext();
        }

        public List<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        public List<Tour> GetAllTours()
        {
            return _context.Tours.Include(t => t.TourOperator).ToList();
        }

        public Tour GetTourById(int id)
        {
            return _context.Tours.Find(id);
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
