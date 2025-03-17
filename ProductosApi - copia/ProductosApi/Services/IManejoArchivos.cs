namespace ProductosApi.Services
{
    public interface IManejoArchivos
    {
        Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType);
        Task BorrarArchivo(string ruta, string contentType);
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType);
    }
}
