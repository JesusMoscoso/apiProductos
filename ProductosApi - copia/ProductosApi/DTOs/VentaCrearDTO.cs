using ProductosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs
{
    public class VentaCrearDTO
    {
        [DataType(DataType.Date)]
        public DateTime FechaVenta { get; set; }

        public int ClienteId { get; set; }

        public List<DetalleCrearDTO> Detalles { get; set; }
    }
}
