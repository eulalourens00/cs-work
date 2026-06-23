using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExxamenKval.Models
{
    public class Order
    {
        public int Id { get; set; }

        // внешниик
        public int ClientId { get; set; }
        public int TourId { get; set; }

        public DateTime DepartureDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public virtual Client Client { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
