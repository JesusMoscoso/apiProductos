namespace ProductosApi.DTOs
{
    public class VentaDetalleDTO:VentasDTO
    {
        public List<DetalleDTO> Detalles { get; set; } = new List<DetalleDTO>();
    }
}
