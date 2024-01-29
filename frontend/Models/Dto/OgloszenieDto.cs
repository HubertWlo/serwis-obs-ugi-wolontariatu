using System;

namespace frontend.Models.Dto
{
    public class OgloszenieDto
    {
        public int Id { get; set; }

        // dane 
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public DateTime Data { get; set; }

        // organizator
        public string OrganizatorNazwa { get; set; }

        // Lokalizacja
        public string LokalizacjaNazwa { get; set; }
        public string DlugoscGeo { get; set; }
        public string SzerokoscGeo { get; set; }

        // organizator
        public string WolontariuszNazwa { get; set; }
    }
}
