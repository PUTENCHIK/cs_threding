using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Route("")]
    public class PagesController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public PagesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public ContentResult Index()
        {
            var filePath = Path.Combine(_env.WebRootPath, "index.html");
            var html = System.IO.File.ReadAllText(filePath);
            return Content(html, "text/html");
        }

        [HttpGet("c")]
        public ContentResult Cameras()
        {
            var filePath = Path.Combine(_env.WebRootPath, "cameras.html");
            var html = System.IO.File.ReadAllText(filePath);
            return Content(html, "text/html");
        }

        [HttpGet("s")]
        public ContentResult Snapshots()
        {
            var filePath = Path.Combine(_env.WebRootPath, "snapshots.html");
            var html = System.IO.File.ReadAllText(filePath);
            return Content(html, "text/html");
        }
    }
}
