using AdopcionAPI.DTO;
using AdopcionAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdopcionAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CentroCreacionDTO, Centro>();//sin ReverseMap solo se convierten datos de izquierda a derecha segun el orden que hayamos usado
            CreateMap<MascotaCreacionDTO, Mascota>();
            CreateMap<UsuarioCreacionDTO, Usuario>();
            CreateMap<AdopcionCreacionDTO, Adopcion>();

            CreateMap<Mascota, MascotaDTO>().ReverseMap();//ReverseMap indica que se puede convertir en ambas direcciones
            CreateMap<Mascota, MascotaConsultaConCentroDTO>();
            CreateMap<Centro, CentroConsultaConMascotasDTO>();
            CreateMap<Adopcion, AdopcionConsultaConOtrosDTO>();
        }
    }
}
