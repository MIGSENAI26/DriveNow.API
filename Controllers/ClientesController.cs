using DriveNow.API.Data;
using DriveNow.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCliente()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        //[HttpPost]
        //public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    _context.Clientes.Add(cliente);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        //}

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromForm] Cliente cliente)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            if (cliente.FotoUploadCliente != null)
            {
                
                string pastaImagens = Path.Combine(_webHost.WebRootPath, "imagens", "clientes");

                if (!Directory.Exists(pastaImagens))
                {
                    Directory.CreateDirectory(pastaImagens);
                }

                
                string nomeArquivo = Guid.NewGuid().ToString() + "_" + cliente.FotoUploadCliente.FileName;
                string caminhoArquivo = Path.Combine(pastaImagens, nomeArquivo);

                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await cliente.FotoUploadCliente.CopyToAsync(fileStream);
                }

                
                cliente.FotoUrlCliente = "/imagens/clientes/" + nomeArquivo;
            }

            
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(Cliente cliente, int id)
        {
            
            if (id != cliente.Id)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            
            var clienteExistente = await _context.Clientes.FindAsync(id);

            if (clienteExistente == null)
            {
                return NotFound();
            }

            clienteExistente.Nome = cliente.Nome;
            clienteExistente.Email = cliente.Email;
            clienteExistente.Cpf = cliente.Cpf;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erro de concorrência ao atualizar o cliente.");
            }

            return Ok(clienteExistente);
        }

    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var paciente = _context.Clientes.Find(id);
            if (paciente == null)
            {
                return NotFound();
            }
            _context.Clientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
