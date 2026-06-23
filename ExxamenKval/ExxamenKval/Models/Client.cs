using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExxamenKval.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsVip { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
