using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class OgloszenieInfo
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime Data { get; set; }

        //klucz obcy Organizatora
        [ValidateNever]
        public int OrganizatorId { get; set; }
        //public virtual UzytkownikInfo Organizator { get; set; }

        //klucz obcy Lokalizacji 
        public int LokalizacjaId { get; set; }
        //public virtual LokalizacjaInfo Lokalizacja { get; set; }

        //klucz obcy Wolontariusza
        [ValidateNever]
        public int WolontariuszId { get; set; }
        //public virtual UzytkownikInfo Wolontariusz { get; set; }
    }
}
