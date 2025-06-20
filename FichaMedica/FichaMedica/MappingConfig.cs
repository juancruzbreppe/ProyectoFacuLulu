using AutoMapper;
using FichaMedica.Modelos;
using FichaMedica.Modelos.DTOs;
using System.Drawing;

namespace FichaMedica
{
	public class MappingConfig : Profile
	{
        public MappingConfig()
		{
			CreateMap<Ficha, FichaDTO>().ReverseMap();
		}
    }
}
