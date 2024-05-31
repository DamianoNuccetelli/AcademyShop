using AcademyShopAPI.Models;
using AutoMapper;
using DtoLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Utente, UtenteDTO>().ReverseMap();
            CreateMap<Prodotto, ProdottoDTO>().ReverseMap();
        }
    }
}
