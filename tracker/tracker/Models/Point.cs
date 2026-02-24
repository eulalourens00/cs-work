using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tracker.Models
{
    public class Point
    {
        public Microsoft.Maui.Devices.Sensors.Location Location { get; set; }
        public int Count { get; set; } = 1;
        public Color Heat {  get; set; }
    }
}
