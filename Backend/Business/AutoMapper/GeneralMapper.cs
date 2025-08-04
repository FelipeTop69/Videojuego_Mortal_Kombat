using AutoMapper;
using Entity.DTOs;
using Entity.DTOs.Juego;
using Entity.DTOs.Jugador;
using Entity.DTOs.JugadorCarta;
using Entity.Models;

namespace Business.AutoMapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            // Carta => CartaDTO
            CreateMap<Carta, CartaDTO>().ReverseMap();

            // Jugador => JugadorDTO
            CreateMap<Jugador, JugadorDTO>().ReverseMap();

            // CrearJugadorDTO => Jugador (solo para crear)
            CreateMap<CrearJugadorDTO, Jugador>();

            // JugadorCarta => JugadorCartaDTO
            CreateMap<JugadorCarta, JugadorCartaDTO>()
                .ForMember(dest => dest.NombreCarta, opt => opt.MapFrom(src => src.Carta.Nombre))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()))
                .ForMember(dest => dest.RondaJugado, opt => opt.MapFrom(src => src.RondaJugado));

            // CartaJugadaResumenDTO (ya lo vimos antes)
            CreateMap<JugadorCarta, CartaJugadaResumenDTO>()
                .ForMember(dest => dest.Jugador, opt => opt.MapFrom(src => src.Jugador.Nombre))
                .ForMember(dest => dest.CartaNombre, opt => opt.MapFrom(src => src.Carta.Nombre))
                .ForMember(dest => dest.Valor, opt => opt.Ignore()); 
        }
    }
}
