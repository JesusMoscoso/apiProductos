using ProductosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs
{
    public class VentasDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaVenta { get; set; }
        public int ClienteId { get; set; }

       
        public string Estado { get; set; } = "Pendiente";

        public string NombreCliente { get; set; }
    }
}
