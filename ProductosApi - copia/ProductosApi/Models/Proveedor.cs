using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models
{
    public class Proveedor
    {
     
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Nombre { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$",ErrorMessage = "El número de teléfono debe tener exactamente 8 dígitos.")]
        public string Telefono { get; set; }

        public List<Producto> productos { get; set; } = [];

    }
}
