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
    [Route("api/clientes")]
    public class ClienteController:ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;


        public ClienteController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            try
            {
                var clientes = await _context.Clientes
                                        .Where(c => !c.Borrado)
                                        .ToListAsync();
                var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);
                return clientesDTO;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar los clientes, intente más tarde");
            }
        }

        
        [HttpGet("{id:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            try
            {
                var cliente = await _context.Clientes
                                        .FirstOrDefaultAsync(c => c.Id == id && !c.Borrado);

                if (cliente == null) return NotFound();

                //var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
                return cliente;
            }
            catch (Exception)
            {

                return BadRequest("Imposible recuperar el cliente, intente más tarde");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Save([FromForm] ClienteCrearDTO clienteCrearDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteCrearDTO);

            cliente.Borrado = false;
            
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            var dto = _mapper.Map<ClienteDTO>(cliente);
            return new CreatedAtRouteResult("ObtenerCliente", new { id = cliente.Id }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] ClienteCrearDTO clienteCrearDTO)
        {
            bool existe = await _context.Clientes
                                        .AnyAsync(c => c.Id == id && !c.Borrado);
            if (existe)
            {
                var cliente = _mapper.Map<Cliente>(clienteCrearDTO);
                cliente.Id = id;
                cliente.Borrado = false;
                
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return NotFound();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);

            if (cliente == null) return NotFound();
            bool tieneVentas = await _context.Ventas.AnyAsync(v => v.ClienteId== id);
            if (tieneVentas)
            {
                cliente.Borrado = true;
            }
            else
            {
                _context.Remove(cliente);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
