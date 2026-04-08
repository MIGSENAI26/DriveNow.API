using DriveNow.API.Data;
using DriveNow.API.Models;
using DriveNow.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenciasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ViaCepService _viaCepService;

        public AgenciasController(AppDbContext context, ViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgencia()
        {
            var agencias = await _context.Agencias.ToListAsync();
            return Ok(agencias);
        }

        [HttpPost]
        public async Task<ActionResult<Agencia>> PostAgencia(Agencia agencia)
        {
            var endereco = await _viaCepService.BuscarEnderecoAsync(agencia.Cep);
            if (endereco != null)
            {
                agencia.Logradouro = endereco.logradouro;
                agencia.Bairro = endereco.bairro;
                agencia.Localidade = endereco.localidade;
                agencia.Uf = endereco.uf;
            }
            else
            {
                return BadRequest("CEP não encontrado");
            }

            _context.Agencias.Add(agencia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAgencia), new { id = agencia.Id }, agencia);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agencia>> GetAgencia(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null)
            {
                return NotFound();
            }
            return agencia;
        }

        [HttpPut]
        public async Task<IActionResult> PutAgencia(Agencia agencia)
        {
            var agenciaExistente = await _context.Agencias.FindAsync(agencia.Id);
            var endereco = await _viaCepService.BuscarEnderecoAsync(agencia.Cep);
            if (endereco != null)
            {
                if (agenciaExistente == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest("CEP não encontrado");
            }

            agenciaExistente.Logradouro = endereco.logradouro;
            agenciaExistente.Bairro = endereco.bairro;
            agenciaExistente.Localidade = endereco.localidade;
            agenciaExistente.Uf = endereco.uf;
   
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgencia(int id)
        {
            var agencia = await _context.Agencias.FindAsync(id);
            if (agencia == null)
            {
                return NotFound();
            }
            _context.Agencias.Remove(agencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
