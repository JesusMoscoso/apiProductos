using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;
using ProductosApi.DTOs;
using ProductosApi.Models;
using ProductosApi.Services;

namespace ProductosApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController:ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManejoArchivos _manejoArchivos;
        private readonly string contenedor = "productos";

        public ProductoController(ApiDbContext context,IMapper mapper,IManejoArchivos manejoArchivos)
        {
            _context = context;
            _mapper = mapper;
            _manejoArchivos = manejoArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll() {
            try
            {
                var productos=await _context.Productos
                                        .Where(p=>!p.Borrado)
                                        .Include(p => p.Proveedor)
                                        .ToListAsync();
                var productosDto=_mapper.Map<List<ProductoDTO>>(productos);
                return productosDto;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar los productos, intente más tarde");
            }
        }

        [HttpGet("borrados")]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAllBorrados()
        {
            try
            {
                var productos = await _context.Productos
                                        .Where(p => p.Borrado)
                                         .Include(p => p.Proveedor)
                                        .ToListAsync();
                var productosDto = _mapper.Map<List<ProductoDTO>>(productos);
                return productosDto;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar los productos, intente más tarde");
            }
        }
        [HttpGet("{id:int}",Name ="ObtenerProducto")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            try
            {
                var producto = await _context.Productos
                                        .Include(p=>p.Proveedor)
                                        .FirstOrDefaultAsync(p=>p.Id==id && !p.Borrado);
                
                if (producto==null) return NotFound();

                var productoDto = _mapper.Map<ProductoDTO>(producto);
                return productoDto;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar los productos, intente más tarde");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromForm] ProductoCrearDTO productoCrearDTO) { 
            var producto=_mapper.Map<Producto>(productoCrearDTO);

            producto.Borrado=false;
            if (productoCrearDTO.Foto != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productoCrearDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(productoCrearDTO.Foto.FileName);
                    producto.Foto = await _manejoArchivos.GuardarArchivo(contenido, extension, contenedor, productoCrearDTO.Foto.ContentType);
                }
            }
            _context.Add(producto);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<ProductoDTO>(producto);

            return new CreatedAtRouteResult("ObtenerProducto", new { id = producto.Id },dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id,[FromForm] ProductoCrearDTO productoCrearDTO)
        {
            var producto = await _context.Productos
                                        .Include(p=> p.Proveedor)
                                        .FirstOrDefaultAsync(p => p.Id == id && !p.Borrado);
            if (producto!=null) {
                producto = _mapper.Map(productoCrearDTO, producto);
                producto.Id = id;
                producto.Borrado = false;
                if (productoCrearDTO.Foto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await productoCrearDTO.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(productoCrearDTO.Foto.FileName);
                        producto.Foto = await _manejoArchivos.EditarArchivo(contenido, extension, contenedor,producto.Foto, productoCrearDTO.Foto.ContentType);
                    }
                }
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            
            return NotFound();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);

            if (producto==null) return NotFound();
            bool tieneDetalles = await _context.DetallesVenta.AnyAsync(d => d.ProductoId == id);
            if (tieneDetalles)
            {
                producto.Borrado = true;
            }
            else {
                await _manejoArchivos.BorrarArchivo(producto.Foto, contenedor);
                _context.Remove(producto);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

    

    }
}
