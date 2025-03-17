using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Validations
{
    public enum GrupoTipoArchivo
    {
        Imagen
    }
    public class TipoArchivoValidation:ValidationAttribute
    {
        private string[] Tipos;

        public TipoArchivoValidation(string[] tiposValidos)
        {
            Tipos = tiposValidos;

        }
        public TipoArchivoValidation(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                Tipos = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
            else
            {
                Tipos = [];
            }

        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile == null) return ValidationResult.Success;
            var tipoContenido = formFile.ContentType;


            if (Tipos.Contains(tipoContenido))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("El tipo de archivo no es válido. Los tipos permitidos son: " + string.Join(", ", Tipos));


        }
    }
}
