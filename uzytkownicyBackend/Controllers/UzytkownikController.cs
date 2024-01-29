using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uzytkownicyBackend.Data;
using uzytkownicyBackend.Models;

namespace uzytkownicyBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzytkownikController : ControllerBase
    {
        private readonly UzytkownikDbContext _context;
        public UzytkownikController(UzytkownikDbContext context)
        {
            _context = context;
        }

        // GET: api/uzytkownik
        [HttpGet]
        public ActionResult<IEnumerable<Uzytkownik>> GetUzytkownik()
        {
            return _context.Uzytkownicy.ToList();
        }

        // GET: api/uzytkownik/{id}
        [HttpGet("{id}")]
        public ActionResult<Uzytkownik> GetUzytkownikById(int id)
        {
            var item = _context.Uzytkownicy.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/uzytkownik
        [HttpPost]
        public ActionResult<Uzytkownik> CreateUzytkownik(Uzytkownik uzytkownik)
        {
            _context.Uzytkownicy.Add(uzytkownik);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUzytkownikById), new { id = uzytkownik.Id }, uzytkownik);
        }

        // PUT: api/uzytkownik/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatUzytkownik(int id, Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.Id)
            {
                return BadRequest();
            }

            _context.Entry(uzytkownik).State = EntityState.Modified;

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

        // DELETE: api/uzytkownik/{id}
        [HttpDelete("{id}")]
        public ActionResult<Uzytkownik> DeleteUzytkownik(int id)
        {
            var item = _context.Uzytkownicy.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Uzytkownicy.Remove(item);
            _context.SaveChanges();

            return item;
        }
    }
}
