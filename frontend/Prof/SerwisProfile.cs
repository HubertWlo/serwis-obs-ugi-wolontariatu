using AutoMapper;
using frontend.Models;
using frontend.Models.Dto;

namespace frontend.Prof
{
    public class SerwisProfile : Profile
    {
        public SerwisProfile() 
        {
           // CreateMap<OgloszenieInfo, OgloszenieDto>()
                //.ForMember(d => d.LokalizacjaNazwa, u => u.MapFrom(c => c.Lokalizacja.Nazwa))
                //.ForMember(d => d.SzerokoscGeo, u => u.MapFrom(c => c.Lokalizacja.SzerokoscGeo))
                //.ForMember(d => d.DlugoscGeo, u => u.MapFrom(c => c.Lokalizacja.DlugoscGeo))
                //.ForMember(d => d.WolontariuszNazwa, u => u.MapFrom(c => c.Wolontariusz.Login))
                //.ForMember(d => d.OrganizatorNazwa, u => u.MapFrom(c => c.Organizator.Login));
            //CreateMap<ZgloszenieInfo, ZgloszenieDto>()
                    //.ForMember(d => d.NazwaOgloszenia, u => u.MapFrom(c => c.Ogloszenie.Nazwa))
                    //.ForMember(d => d.NazwaWolontariusza, u => u.MapFrom(c => c.Wolontariusz.Login));
        }
    }
}