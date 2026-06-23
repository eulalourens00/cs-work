using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExxamenKval.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string HotelName { get; set; }
        public int NightsCount { get; set; }
        public decimal PricePerAdult { get; set; }
        public decimal PricePerChild { get; set; }
        public int TourOperatorId { get; set; }
        public virtual TourOperator TourOperator { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
