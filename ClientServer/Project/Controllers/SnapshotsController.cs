using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnapshotsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public SnapshotsController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Snapshot>>> GetSnapshots()
        {
            return await _context.Snapshots.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Snapshot>> GetSnapshot(int id)
        {
            var snapshot = await _context.Snapshots.FindAsync(id);
            return snapshot == null ? NotFound() : snapshot;
        }

        [HttpPost]
        public async Task<ActionResult<Snapshot>> CreateSnapshot(int cameraId, int label)
        {
            var camera = await _context.Cameras.FindAsync(cameraId);
            if (camera == null)
            {
                return NotFound();
            }
            if (label < 0)
            {
                return BadRequest();
            }

            var snapshot = new Snapshot { CameraId=cameraId, Label=label, CreatedAt = DateTime.Now };
            _context.Snapshots.Add(snapshot);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSnapshot), new { id = snapshot.Id }, snapshot);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Snapshot>> UpdateSnapshot(int id, int label)
        {
            var snapshot = await _context.Snapshots.FindAsync(id);
            if (snapshot == null)
            {
                return NotFound();
            }
            else if (label < 0)
            {
                return BadRequest();
            }
            else
            {
                snapshot.Label = label;
                snapshot.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSnapshot), new { id = snapshot.Id }, snapshot);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSnapshot(int id)
        {
            var snapshot = await _context.Snapshots.FindAsync(id);
            if (snapshot == null)
            {
                return NotFound();
            }
            else
            {
                _context.Snapshots.Remove(snapshot);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
