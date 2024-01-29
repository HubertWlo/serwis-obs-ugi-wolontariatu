using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace frontend.Models
{
    public class UzytkownikInfo
    {
        [Key]
        public int Id { get; set; }

        //dane
        public String Login { get; set; }
        public String Haslo { get; set; }
        public String PESEL { get; set; }
        [ValidateNever]
        public String Rola { get; set; }
        public String Mail { get; set; }
        public int Telefon { get; set; }
    }
}