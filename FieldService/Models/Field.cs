namespace FieldServiceAPI.Models
{
    public class Field
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public double Size { get; set; }
        public FieldGeometry Locations { get; set; } = null!;
    }
}
