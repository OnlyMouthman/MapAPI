using MapAPI.Models.Test;

namespace MapAPI.Models.Controller
{
    public class Geometry
    {
    }

    public partial class MyPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Chinese { get; set; }

        public string English { get; set; }

        public DateTime Date { get; set; }

        public string Describe { get; set; }

        public int Tag { get; set; }
    }

    public partial class MyPolygon
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Chinese { get; set; }

        public string English { get; set; }

        public DateTime Date { get; set; }

        public string Describe { get; set; }

        public int Tag { get; set; }
    }
}
