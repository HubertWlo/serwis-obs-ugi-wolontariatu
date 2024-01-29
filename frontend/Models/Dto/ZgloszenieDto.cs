using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace frontend.Models.Dto
{
    public class ZgloszenieDto
    {
        public int Id { get; set; }
        public string NazwaOgloszenia { get; set; }
        public string NazwaWolontariusza { get; set; }

        //dane
        [ValidateNever]
        public int Ocena { get; set; }
        public String Tresc { get; set; }
    }
}
