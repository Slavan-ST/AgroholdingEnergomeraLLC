namespace FieldServiceApp.Models
{
    public class Field
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Size { get; set; }
        public FieldGeometry Locations { get; set; }
    }
}
