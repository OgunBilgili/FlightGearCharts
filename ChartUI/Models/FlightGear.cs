using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartUI.Models
{
    public class FlightGear
    {
        public int ID { get; set; }
        public DateTime FlightDate { get; set; }
        public float Altitude { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Roll { get; set; }
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Speed { get; set; }
    }
}
