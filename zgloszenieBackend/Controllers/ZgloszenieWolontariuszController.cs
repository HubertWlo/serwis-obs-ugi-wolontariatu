using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using zgloszenieBackend.Data;
using zgloszenieBackend.Models;

namespace zgloszenieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgloszenieWolontariuszController : ControllerBase
    {
        private readonly ZgloszenieDbContext _context;
        public ZgloszenieWolontariuszController(ZgloszenieDbContext context)
        {
            _context = context;
        }

        // GET: api/zgloszenie/{wolontariuszId}
        [HttpGet("{wolontariuszId}")]
        public ActionResult<IEnumerable<Zgloszenie>> GetZgloszenieByWolontariuszId(int wolontariuszId)
        {
            var item = _context.Zgloszenia
                .Where(z => z.WolontariuszId == wolontariuszId)
                .ToList();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
