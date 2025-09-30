using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialAPI.Data;
using PrimerParcialAPI.Models;

namespace PrimerParcialAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public EventsController(AppDbContext db) => _db = db;

        // GET: api/events?from=2025-09-01&to=2025-09-30&isOnline=true&location=La Paz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] bool? isOnline,
            [FromQuery] string? location,
            [FromQuery] string? title)
        {
            var q = _db.Events.AsNoTracking().AsQueryable();

            if (from.HasValue) q = q.Where(e => e.StartAt >= from.Value);
            if (to.HasValue)   q = q.Where(e => e.StartAt <= to.Value);
            if (isOnline.HasValue) q = q.Where(e => e.IsOnline == isOnline.Value);
            if (!string.IsNullOrWhiteSpace(location)) q = q.Where(e => e.Location == location);
            if (!string.IsNullOrWhiteSpace(title))    q = q.Where(e => e.Title.Contains(title));

            var list = await q.OrderBy(e => e.StartAt).ThenBy(e => e.Id).ToListAsync();
            return Ok(list);
        }

        // GET: api/events/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Event>> GetOne(int id)
        {
            var e = await _db.Events.FindAsync(id);
            return e is null ? NotFound() : Ok(e);
        }

        // POST: api/events
        [HttpPost]
        public async Task<ActionResult<Event>> Create([FromBody] Event input)
        {
            if (input.Id != 0) input.Id = 0;
            _db.Events.Add(input);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOne), new { id = input.Id }, input);
        }

        // PUT: api/events/5  (reemplazo completo)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event input)
        {
            if (id != input.Id) return BadRequest("El ID de la ruta y del cuerpo deben coincidir.");
            var exists = await _db.Events.AnyAsync(e => e.Id == id);
            if (!exists) return NotFound();

            _db.Entry(input).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/events/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Events.FindAsync(id);
            if (e is null) return NotFound();
            _db.Events.Remove(e);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
