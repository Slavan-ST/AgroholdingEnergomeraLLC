namespace FieldServiceAPI.Models
{
    public class FieldGeometry
    {
        public Point Center { get; set; } = null!;
        public List<Point> Polygon { get; set; } = null!;
    }
}
