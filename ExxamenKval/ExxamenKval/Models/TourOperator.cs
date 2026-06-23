using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExxamenKval.Models
{
    public class TourOperator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string INN { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
