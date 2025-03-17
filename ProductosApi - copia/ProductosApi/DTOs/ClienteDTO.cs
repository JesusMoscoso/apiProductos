using ProductosApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductosApi.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public required string NombreCompleto { get; set; }
        
        public required string Telefono { get; set; }

        public string? Email { get; set; }

        
    }
}
