using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs
{
    public class ProveedorCrearDTO
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public required string Nombre { get; set; }


        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [RegularExpression(@"^\d{8}$",
            ErrorMessage = "El número de teléfono debe tener exactamente 8 dígitos.")]
        public required string Telefono { get; set; }

    }
}
