using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;
using ProductosApi.DTOs;
using ProductosApi.Models;

namespace ProductosApi.Controllers
{

    [ApiController]
    [Route("api/proveedor")]
    public class ProveedorController: ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public ProveedorController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //obtener todos los provedores.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> GetAll()
        {
            try
            {
                var proveedores = await _context.Proveedores
                                        .ToListAsync();


                var proveedoresDTO = _mapper.Map<List<ProveedorDTO>>(proveedores);

                return proveedoresDTO;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar los proveedores, intente más tarde");
            }
        }

        //obtener proveedor
        [HttpGet("{id:int}", Name = "ObtenerProveedor")]
        public async Task<ActionResult<ProveedorDTO>> GetById(int id)
        {
            try
            {
                
                var proveedor = await _context.Proveedores
                                      .FirstOrDefaultAsync(p => p.Id == id);

                if (proveedor == null) return NotFound();
                var proveedorDTO = _mapper.Map<ProveedorDTO>(proveedor);
                return Ok(proveedorDTO);

            }
            catch (Exception ex)
            {

                return BadRequest("Imposible recuperar el proveedor, intente más tarde");
            }
        }

        //guardar
        [HttpPost]
        public async Task<ActionResult> Save([FromForm] ProveedorCrearDTO proveedorCrearDTO)
        {
            var proveedor = _mapper.Map<Proveedor>(proveedorCrearDTO);

            _context.Add(proveedor);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ProveedorDTO>(proveedor);
            return new CreatedAtRouteResult("ObtenerProveedor", new { id = proveedor.Id }, dto);

        }


        //actualizar
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] ProveedorCrearDTO proveedorCrearDTO)
        {
            bool existe = await _context.Proveedores
                                        .AnyAsync(p => p.Id == id);
            if (existe)
            {
                var proveedor = _mapper.Map<Proveedor>(proveedorCrearDTO);
                proveedor.Id = id;
              

                _context.Update(proveedor);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NotFound();
        }

        //eliminar 
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var proveedor = await _context.Proveedores.FirstOrDefaultAsync(p => p.Id == id);

            if (proveedor == null) return NotFound();

            bool tieneProductos = await _context.Productos.AnyAsync(p => p.ProveedorId == id);
            if (tieneProductos)
            {
                return BadRequest("No se puede eliminar el proveedor tiene productos asociados");
            }
            
            else
            {
                _context.Remove(proveedor);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
