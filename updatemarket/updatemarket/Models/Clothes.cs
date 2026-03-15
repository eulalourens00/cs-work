using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace updatemarket.Models
{
    public enum Size
    {
        Small,
        Medium,
        Big
    }
    class Clothes
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public double Rating { get; set; }
    }
}
