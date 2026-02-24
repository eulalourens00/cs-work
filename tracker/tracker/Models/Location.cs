using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace tracker.Models
{
    public class Location
    {
        public Location() { }
        public Location(double latitude, double longitude) { 
            Latitude = latitude;
            Longitude = longitude;
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
