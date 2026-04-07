using DriveNow.API.Data;
using DriveNow.API.DTOs;
using DriveNow.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DriveNow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocacaosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLocacao()
        {
            var locacoes = await _context.Locacaos.Include(c => c.Cliente).Include(c => c.Veiculo).ToListAsync();
            return Ok(locacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoResponseDto>> GetLocacao(int id)
        {
            var locacao = await _context.Locacaos
                .Include(c => c.Cliente)
                .Include(c => c.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);

            var c = await _context.Locacaos.FindAsync(id);
            var m = await _context.Veiculos.FindAsync(id);
            var p = await _context.Clientes.FindAsync(id);

            var Test = locacao.Cliente.Nome;
            var Test2 = p.Nome;

            if (locacao == null) return NotFound("Locação não encontrada!");
            var locacaoDto = new LocacaoResponseDto
            {
                Id = locacao.Id,
                ClienteId = locacao.ClienteId,
                ClienteNome = locacao.Cliente?.Nome,
                VeiculoId = locacao.VeiculoId,
                DataRetirada = locacao.DataRetirada,
                DataDevolucao = locacao.DataDevolucao,
                ValorTotal = locacao.ValorTotal
            };
            return Ok(locacaoDto);
        }

        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(Locacao locacao)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Locacaos.Add(locacao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLocacao), new { id = locacao.Id }, locacao);
        }

        [HttpPut]
        public async Task<IActionResult> PutLocacao(Locacao locacao)
        {
            var locacaoExistente = await _context.Locacaos.FindAsync(locacao.Id);
            if (locacaoExistente == null)
            {
                return NotFound();
            }
            locacaoExistente.ClienteId = locacao.ClienteId;
            locacaoExistente.VeiculoId = locacao.VeiculoId;
            locacaoExistente.DataRetirada = locacao.DataRetirada;
            locacaoExistente.DataDevolucao = locacao.DataDevolucao;
            locacaoExistente.ValorTotal = locacao.ValorTotal;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocacao(int id)
        {
            var locacao = await _context.Locacaos.FindAsync(id);
            if (locacao == null)
            {
                return NotFound();
            }
            _context.Locacaos.Remove(locacao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
