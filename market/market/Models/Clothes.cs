using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market.Models
{
    enum Size
    {
        Small,
        Medium,
        Large
    }
    public class Clothes
    {
        public double cost {  get; set; }
        public double rating { get; set; }
    }
}
