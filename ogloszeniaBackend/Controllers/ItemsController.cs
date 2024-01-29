using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogloszeniaBackend.Models;
using ogloszeniaBackend.Data;
using ogloszeniaBackend.Models;

namespace ogloszeniaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly YourDbContext _context;

        public ItemsController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Ogloszenie>> GetItems()
        {
            return _context.Ogloszenia.ToList();
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Ogloszenie> GetItemById(int id)
        {
            var item = _context.Ogloszenia.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Ogloszenie> CreateItem(Ogloszenie oglosznie)
        {
            _context.Ogloszenia.Add(oglosznie);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetItemById), new { id = oglosznie.Id }, oglosznie);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Ogloszenie ogloszenie)
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

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public ActionResult<Ogloszenie> DeleteItem(int id)
        {
            var item = _context.Ogloszenia.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Ogloszenia.Remove(item);
            _context.SaveChanges();

            return item;
        }
    }
}