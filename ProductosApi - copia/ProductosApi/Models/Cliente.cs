using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]

        public required string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public required string Apellido { get; set; } = "";

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$",
            ErrorMessage = "El número de teléfono debe tener exactamente 8 dígitos.")]
        public required string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Email { get; set; }

        public bool Borrado { get; set; } = false;


        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        
    }
}
