using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Entities;

namespace Project.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CamerasController : Controller
    {
        private readonly ApplicationContext _context;

        public CamerasController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camera>>> GetCameras()
        {
            return await _context.Cameras.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Camera>> GetCamera(int id)
        {
            var camera = await _context.Cameras.FindAsync(id);
            return camera == null ? NotFound() : camera;
        }

        [HttpPost]
        public async Task<ActionResult<Camera>> CreateCamera(string address)
        {
            var camera = new Camera { Address = address, CreatedAt = DateTime.Now };
            _context.Cameras.Add(camera);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCamera), new { id = camera.Id}, camera);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Camera>> UpdateCamera(int id, string address)
        {
            var camera = await _context.Cameras.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            else
            {
                camera.Address = address;
                camera.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCamera), new { id = camera.Id }, camera);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamera(int id)
        {
            var camera = await _context.Cameras.FindAsync(id);
            if (camera == null)
            {
                return NotFound();
            }
            else
            {
                _context.Cameras.Remove(camera);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
