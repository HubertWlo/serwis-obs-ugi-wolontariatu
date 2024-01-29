using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace frontend.Models
{
    public class ZgloszenieInfo
    {
        [Key]
        public int Id { get; set; }

        //klucz obcy Ogloszenia
        [ValidateNever]
        public int OgloszenieId { get; set; }
        //public virtual OgloszenieInfo Ogloszenie { get; set; }

        //klucz obcy Wolontariusza
        [ValidateNever]
        public int WolontariuszId { get; set; }
        //public virtual UzytkownikInfo Wolontariusz { get; set; }
        
        //dane
        [ValidateNever]
        public int Ocena { get; set; }
        public String Tresc { get; set; }
    }
}
