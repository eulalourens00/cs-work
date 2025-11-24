using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherViewer.models
{
    public class WeatherData
    {
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
