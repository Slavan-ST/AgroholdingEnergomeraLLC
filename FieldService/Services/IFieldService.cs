using FieldServiceApp.Models;

namespace FieldServiceApp.Services
{
    public interface IFieldService
    {
        IEnumerable<Field> GetAllFields();
        double? GetFieldSize(string id);
        double? GetDistanceFromCenter(string fieldId, Point point);
        (string id, string name)? GetFieldContainingPoint(Point point);
    }
}
