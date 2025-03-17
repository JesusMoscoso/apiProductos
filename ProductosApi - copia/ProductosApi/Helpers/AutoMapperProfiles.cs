using AutoMapper;
using ProductosApi.DTOs;
using ProductosApi.Models;

namespace ProductosApi.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Producto,ProductoDTO>().ReverseMap();


            CreateMap<ProductoCrearDTO,Producto>()
                .ForMember(a => a.Foto, options => options.Ignore());


                    CreateMap<Producto, ProductoDTO>()
             .ForMember(dest => dest.NombreProveedor, opt => opt.MapFrom(src => src.Proveedor != null ? src.Proveedor.Nombre : null))
             .ReverseMap();


            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dto => dto.NombreCompleto, config => config.MapFrom(c => $"{c.Nombre} {c.Apellido}"));
            CreateMap<ClienteCrearDTO, Cliente>();

            CreateMap<Venta, VentasDTO>()
                .ForMember(dto => dto.NombreCliente, config => config.MapFrom(v => $"{v.Cliente.Nombre} {v.Cliente.Apellido}"));

            CreateMap<Venta, VentaDetalleDTO>()
                .ForMember(dto => dto.NombreCliente, config => config.MapFrom(v => $"{v.Cliente.Nombre} {v.Cliente.Apellido}"))
                .ForMember(dto => dto.Detalles, options => options.MapFrom(MapDetallesVenta));

            CreateMap<VentaCrearDTO, Venta>()
                        .ForMember(v => v.DetallesVenta, options => options.MapFrom(MapDetallesVenta));

            // este es para mostrar el nombre del proveedor en producto
            CreateMap<Producto, ProductoDTO>()
           .ForMember(dest => dest.NombreProveedor, opt => opt.MapFrom(src => src.Proveedor.Nombre));

            CreateMap<Proveedor, ProveedorDTO>()
            .ForMember(dto => dto.NombreProveedor, config => config.MapFrom(p => $"{p.Nombre}"));
            CreateMap<ProveedorCrearDTO, Proveedor>();


        }
        private List<DetalleVenta> MapDetallesVenta(VentaCrearDTO ventaCrearDTO, Venta venta)
        {
            List<DetalleVenta> resultado;
            if (venta.DetallesVenta.Count == 0)
            {
                resultado = new List<DetalleVenta>();
            }
            else
            {
                resultado = venta.DetallesVenta.ToList();
            }

            if (ventaCrearDTO.Detalles == null) return resultado;

            foreach (var item in ventaCrearDTO.Detalles)
            {
                var detalleExistente = resultado.FirstOrDefault(d => d.ProductoId == item.ProductoId);
                if (detalleExistente != null)
                {
                    detalleExistente.Cantidad = item.Cantidad;
                    
                }
                else
                {
                    resultado.Add(new DetalleVenta() { ProductoId = item.ProductoId, Cantidad = item.Cantidad });
                }
            }
            return resultado;
        }
        private List<DetalleDTO> MapDetallesVenta(Venta venta,VentaDetalleDTO ventaDetalleDTO )
        {
            var resultado = new List<DetalleDTO>();
            if (venta.DetallesVenta == null) return resultado;
            foreach (var item in venta.DetallesVenta)
            {
                resultado.Add(new DetalleDTO() { 
                    ProductoId = item.ProductoId,
                    NombreProducto=item.Producto.Nombre, 
                    Cantidad = item.Cantidad,
                    Precio=item.Producto.Precio 
                });
            }
            return resultado;
        }
    }
}
