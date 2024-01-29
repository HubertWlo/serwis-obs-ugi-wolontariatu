using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zgloszenieBackend.Data;
using zgloszenieBackend.Models;

namespace zgloszenieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgloszenieOpiekunController : ControllerBase
    {
        private readonly ZgloszenieDbContext _context;
        public ZgloszenieOpiekunController(ZgloszenieDbContext context)
        {
            _context = context;
        }

        // GET: api/zgloszenie/{ogloszenieId}
        [HttpGet("{ogloszenieId}")]
        public ActionResult<IEnumerable<Zgloszenie>> GetZgloszenieByOgloszenieId(int ogloszenieId)
        {
            var item = _context.Zgloszenia
                .Where(z => z.OgloszenieId == ogloszenieId)
                .ToList();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
