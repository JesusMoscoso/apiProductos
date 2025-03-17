using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Validations
{
    public class PesoArchivoValidation:ValidationAttribute
    {
        private readonly int pesoMaximoMB;
        public PesoArchivoValidation(int pesoMaximoMB)
        {
            this.pesoMaximoMB = pesoMaximoMB;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile;

            if (formFile is null) return ValidationResult.Success;
            //lenght en bytes
            if (formFile.Length > pesoMaximoMB * 1024 * 1024)
            {
                return new ValidationResult("El archivo excede el peso máximo");
            }
            return ValidationResult.Success;
        }
    }
}
