using ProductosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs
{
    public class DetalleDTO
    {
        public int ProductoId { get; set; }

        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
