using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zgloszenieBackend.Data;
using zgloszenieBackend.Models;

namespace zgloszenieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZgloszenieController : ControllerBase
    {
        private readonly ZgloszenieDbContext _context;
        public ZgloszenieController(ZgloszenieDbContext context)
        {
            _context = context;
        }

        // GET: api/zgloszenie
        [HttpGet]
        public ActionResult<IEnumerable<Zgloszenie>> GetZgloszenie()
        {
            return _context.Zgloszenia.ToList();
        }

        // GET: api/zgloszenie/{id}
        [HttpGet("{id}")]
        public ActionResult<Zgloszenie> GetZgloszenieById(int id)
        {
            var item = _context.Zgloszenia.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/zgloszenie
        [HttpPost]
        public ActionResult<Zgloszenie> CreateZgloszenie(Zgloszenie zglosznie)
        {
            _context.Zgloszenia.Add(zglosznie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetZgloszenieById), new { id = zglosznie.Id }, zglosznie);
        }

        // PUT: api/zgloszenie/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatZgloszenie(int id, Zgloszenie zglosznie)
        {
            if (id != zglosznie.Id)
            {
                return BadRequest();
            }

            _context.Entry(zglosznie).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/zgloszenie/{id}
        [HttpDelete("{id}")]
        public ActionResult<Zgloszenie> DeleteZgloszenie(int id)
        {
            var item = _context.Zgloszenia.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Zgloszenia.Remove(item);
            _context.SaveChanges();

            return item;
        }
    }
}
