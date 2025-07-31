using Microsoft.AspNetCore.Mvc;

namespace FieldServiceAPI.Controllers
{
    using FieldServiceAPI.Models;
    using FieldServiceAPI.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public IActionResult GetAllFields()
        {
            return Ok(_fieldService.GetAllFields());
        }

        [HttpGet("{id}/size")]
        public IActionResult GetFieldSize(string id)
        {
            var size = _fieldService.GetFieldSize(id);
            if (size == null) return NotFound();
            return Ok(size);
        }

        [HttpGet("{id}/distance")]
        public IActionResult GetDistanceFromCenter(string id, [FromQuery] double lat, [FromQuery] double lng)
        {
            var distance = _fieldService.GetDistanceFromCenter(id, new Point(lat, lng));
            if (distance == null) return NotFound();
            return Ok(distance);
        }

        [HttpGet("contains")]
        public IActionResult GetFieldContainingPoint([FromQuery] double lat, [FromQuery] double lng)
        {
            var result = _fieldService.GetFieldContainingPoint(new Point(lat, lng));
            if (result == null) return Ok(false);
            return Ok(new { result.Value.id, result.Value.name });
        }
    }
}
