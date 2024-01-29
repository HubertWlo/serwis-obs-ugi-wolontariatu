using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ogloszeniaBackend.Models;
using ogloszeniaBackend.Data;
using ogloszeniaBackend.Models;

namespace ogloszeniaBackend.Controllers
{
    public class ItemsOpiekunController : Controller
    {
        private readonly YourDbContext _context;

        public ItemsOpiekunController(YourDbContext context)
        {
            _context = context;
        }

        // GET: api/items/{opiekunID}
        [HttpGet("{opiekunID}")]
        public ActionResult<ModelListy<Ogloszenie>> GetItemByUserId(int opiekunID)
        {
            List<int> ogloszeniaId = _context.Ogloszenia
                .Where(o => o.OrganizatorId == opiekunID)
                .Select(r => r.Id).ToList();
            List<Ogloszenie> ogloszeniaAktualne = new List<Ogloszenie>();
            List<Ogloszenie> ogloszeniaZakonczone = new List<Ogloszenie>();

            Ogloszenie ogloszenie;
            foreach (int id in ogloszeniaId)
            {
                //_context.Wydarzenie.FirstOrDefault(r => r.Id == id);
                ogloszenie = _context.Ogloszenia
                    .Where(r => r.Data >= DateTime.Now)
                    .FirstOrDefault(r => r.Id == id);
                if (ogloszenie != null)
                {
                    ogloszeniaAktualne.Add(ogloszenie);
                }
                else
                {
                    ogloszenie = _context.Ogloszenia
                        .FirstOrDefault(r => r.Id == id);
                    ogloszeniaZakonczone.Add(ogloszenie);
                }
            }
            ModelListy<Ogloszenie> ogloszenia = new ModelListy<Ogloszenie>();
            ogloszenia.List1 = ogloszeniaAktualne;
            ogloszenia.List2 = ogloszeniaZakonczone;

            return ogloszenia;
        }
    }
}
