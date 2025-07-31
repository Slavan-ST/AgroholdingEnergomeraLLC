namespace FieldServiceAPI.Models
{
    public class Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Point(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }
    }
}
