using lokalizacjaBackend.Data;
using lokalizacjaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lokalizacjaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LokalizacjaController : ControllerBase
    {
        private readonly LokalizacjaDbContext _context;
        public LokalizacjaController(LokalizacjaDbContext context)
        {
            _context = context;
        }

        // GET: api/lokalizacja
        [HttpGet]
        public ActionResult<IEnumerable<Lokalizacja>> GetLokalizacja()
        {
            return _context.Lokalizacje.ToList();
        }

        // GET: api/lokalizacja/{id}
        [HttpGet("{id}")]
        public ActionResult<Lokalizacja> GetLokalizacjaById(int id)
        {
            var item = _context.Lokalizacje.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/lokalizacja
        [HttpPost]
        public ActionResult<Lokalizacja> CreateLokalizacja(Lokalizacja oglosznie)
        {
            _context.Lokalizacje.Add(oglosznie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLokalizacjaById), new { id = oglosznie.Id }, oglosznie);
        }

        // PUT: api/lokalizacja/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateLokalizacja(int id, Lokalizacja ogloszenie)
        {
            if (id != ogloszenie.Id)
            {
                return BadRequest();
            }

            _context.Entry(ogloszenie).State = EntityState.Modified;

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

        // DELETE: api/lokalizacja/{id}
        [HttpDelete("{id}")]
        public ActionResult<Lokalizacja> DeleteLokalizacja(int id)
        {
            var item = _context.Lokalizacje.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Lokalizacje.Remove(item);
            _context.SaveChanges();

            return item;
        }
    }
}
