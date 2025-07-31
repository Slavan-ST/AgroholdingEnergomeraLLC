
namespace FieldServiceAPI.Services
{
    using System.Xml.Linq;
    using global::FieldServiceAPI.Models;
    using NetTopologySuite.Geometries;
    using NetTopologySuite.Operation.Distance;
    using Point = Models.Point;

    public class FieldService : IFieldService
    {
        private readonly List<Field> _fields;
        private readonly Dictionary<string, Geometry> _fieldGeometries;

        public FieldService(string centroidsPath, string fieldsPath)
        {
            _fields = new List<Field>();
            _fieldGeometries = new Dictionary<string, Geometry>();

            LoadData(centroidsPath, fieldsPath);
        }

        private void LoadData(string centroidsPath, string fieldsPath)
        {
            // Загрузка центроидов
            var centroidsDoc = XDocument.Load(centroidsPath);
            var centroids = centroidsDoc.Descendants()
                .Where(e => e.Name.LocalName == "Placemark")
                .ToDictionary(
                    e => e.Descendants().First(d => d.Name.LocalName == "name").Value,
                    e => ParsePoint(e.Descendants().First(d => d.Name.LocalName == "coordinates").Value)
                );

            // Загрузка полей
            var fieldsDoc = XDocument.Load(fieldsPath);
            var geometryFactory = new GeometryFactory();

            foreach (var placemark in fieldsDoc.Descendants().Where(e => e.Name.LocalName == "Placemark"))
            {
                var id = placemark.Descendants().First(d => d.Name.LocalName == "name").Value;

                var coordinates = placemark.Descendants()
                    .First(d => d.Name.LocalName == "coordinates")
                    .Value;

                var polygonPoints = ParsePolygon(coordinates);
                var polygon = geometryFactory.CreatePolygon(polygonPoints
                    .Select(p => new Coordinate(p.Longitude, p.Latitude))
                    .ToArray());

                // Рассчитываем площадь в гектарах
                var area = polygon.Area * 10000; // Конвертация из квадратных градусов в гектары

                _fields.Add(new Field
                {
                    Id = id,
                    Name = $"Field {id}",
                    Size = area,
                    Locations = new FieldGeometry
                    {
                        Center = centroids[id],
                        Polygon = polygonPoints
                    }
                });

                _fieldGeometries[id] = polygon;
            }
        }

        private Point ParsePoint(string coordinates)
        {
            var parts = coordinates.Trim().Split(',');
            return new Point(double.Parse(parts[1]), double.Parse(parts[0]));
        }

        private List<Point> ParsePolygon(string coordinates)
        {
            return coordinates.Trim()
                .Split(' ')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s =>
                {
                    var parts = s.Split(',');
                    return new Point(double.Parse(parts[1]), double.Parse(parts[0]));
                })
                .ToList();
        }

        public IEnumerable<Field> GetAllFields() => _fields;

        public double? GetFieldSize(string id)
            => _fields.FirstOrDefault(f => f.Id == id)?.Size;

        public double? GetDistanceFromCenter(string fieldId, Point point)
        {
            var field = _fields.FirstOrDefault(f => f.Id == fieldId);
            if (field == null) return null;

            var coord1 = new Coordinate(field.Locations.Center.Longitude, field.Locations.Center.Latitude);
            var coord2 = new Coordinate(point.Longitude, point.Latitude);

            var distanceInDegrees = coord1.Distance(coord2);
            // Упрощенная конвертация градусов в метры (для WGS84)
            return distanceInDegrees * 111320;
        }

        public (string id, string name)? GetFieldContainingPoint(Point point)
        {
            var testPoint = new GeometryFactory().CreatePoint(
                new Coordinate(point.Longitude, point.Latitude));

            foreach (var field in _fields)
            {
                if (_fieldGeometries[field.Id].Contains(testPoint))
                {
                    return (field.Id, field.Name);
                }
            }

            return null;
        }
    }

}
