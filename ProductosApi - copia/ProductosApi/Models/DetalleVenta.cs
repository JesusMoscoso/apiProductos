using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar la cantidad")]
        [Range(1, 100, ErrorMessage =
            "La cantidad debe ser un valor entero entre 0 y 20")]
        public int Cantidad { get; set; }

        
        [Required(ErrorMessage = "Debe seleccionar un producto")]
        public int ProductoId { get; set; }

       
        public int VentaId { get; set; }

        
        public Venta? Venta { get; set; }

        
        public Producto? Producto { get; set; }
    }
}
