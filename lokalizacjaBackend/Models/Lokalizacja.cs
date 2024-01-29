using System.ComponentModel.DataAnnotations;

namespace lokalizacjaBackend.Models
{
    public class Lokalizacja
    {
        [Key]
        public int Id { get; set; }

        public string Nazwa { get; set; }
        public string Miasto { get; set; }
        public string DlugoscGeo { get; set; }
        public string SzerokoscGeo { get; set; }
        public string WlascicielId { get; set; }
    }
}
