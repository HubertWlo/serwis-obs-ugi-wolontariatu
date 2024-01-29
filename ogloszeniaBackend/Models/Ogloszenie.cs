using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace ogloszeniaBackend.Models
{
    public class Ogloszenie
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime Data { get; set; }

        //klucz obcy Organizatora
        [ValidateNever]
        public int OrganizatorId { get; set; }

        //klucz obcy Lokalizacji 
        public int LokalizacjaId { get; set; }

        //klucz obcy Wolontariusza
        [ValidateNever]
        public int WolontariuszId { get; set; }
    }
}
