using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models
{
    public class Venta
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaVenta { get; set; }
        public int ClienteId { get; set; }

        public string Estado { get; set; } = "Pendiente";


        public Cliente? Cliente { get; set; }

   
        public List<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
    }
}
