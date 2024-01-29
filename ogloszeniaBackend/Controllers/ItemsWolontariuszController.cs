using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogloszeniaBackend.Data;
using ogloszeniaBackend.Models;

namespace ogloszeniaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsWolontariuszController : ControllerBase
    {
        private readonly YourDbContext _context;

        public ItemsWolontariuszController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/wolontariusz/{wolontariuszId}
        [HttpGet("{wolontariuszId}")]
        public ActionResult<IEnumerable<Ogloszenie>> GetItemsByWolontariuszId(int id)
        {
            var item = _context.Ogloszenia
                .Where(o => o.WolontariuszId == id)
                .ToList();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
    }
}
