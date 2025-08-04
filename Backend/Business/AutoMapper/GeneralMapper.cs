using AutoMapper;

namespace Business.AutoMapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            //// Usuario
            //CreateMap<Usuario, UsuarioDTO>()
            //    .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src => src.Rol.Nombre))
            //    .ReverseMap();

            //// Rol
            //CreateMap<Rol, RolDTO>().ReverseMap();

            //// Cliente
            //CreateMap<Cliente, ClienteDTO>().ReverseMap();

            //// Pizza
            //CreateMap<Pizza, PizzaDTO>().ReverseMap();

            //// Pedido
            //CreateMap<Pedido, PedidoDTO>()
            //    .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
            //    .ForMember(dest => dest.PizzaNombre, opt => opt.MapFrom(src => src.Pizza.Nombre))
            //    .ReverseMap();

            //CreateMap<PedidoCreateDTO, Pedido>();
        }
    }
}
