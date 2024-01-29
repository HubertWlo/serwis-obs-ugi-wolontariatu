using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using zgloszenieBackend.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace zgloszenieBackend.Models
{
    public class Zgloszenie
    {
        [Key]
        public int Id { get; set; }

        //klucz obcy Ogloszenia
        [ValidateNever]
        public int OgloszenieId { get; set; }

        //klucz obcy Wolontariusza
        public int WolontariuszId { get; set; }
        
        //dane
        [ValidateNever]
        public int Ocena { get; set; }
        public String Tresc { get; set; }
    }
}
