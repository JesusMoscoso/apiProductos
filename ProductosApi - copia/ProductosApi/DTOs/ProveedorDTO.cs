using Microsoft.SqlServer.Server;

namespace ProductosApi.DTOs
{
    public class ProveedorDTO
    {
        public int Id { get; set; }

        public required string NombreProveedor { get; set; }

        public string? Email { get; set; }

        public required string Telefono { get; set; }
    }
}


