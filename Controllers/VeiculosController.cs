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
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetVeiculo()
        {
            var veiculos = await _context.Veiculos.Include(m => m.Agencia).ToListAsync();
            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoResponseDto>> GetVeiculo(int id)
        {
            var veiculo = await _context.Veiculos
                .Include(m => m.Agencia)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veiculo == null) return NotFound("Veículo não encontrado!");
            var veiculoDto = new VeiculoResponseDto
            {
                Id = veiculo.Id,
                Modelo = veiculo.Modelo,
                Placa = veiculo.Placa,
                AgenciaId = veiculo.AgenciaId,
                AgenciaNome = veiculo.Agencia != null ? veiculo.Agencia.NomeFantasia : "Agância não encontrada!"
            };
            return Ok(veiculoDto);
        }

        [HttpPost]
        public async Task<ActionResult<Veiculo>> PostVeiculo(Veiculo veiculo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVeiculo), new { id = veiculo.Id }, veiculo);
        }

        [HttpPut]
        public async Task<IActionResult> PutVeiculo(Veiculo veiculo)
        {
            var veiculoExistente = await _context.Veiculos.FindAsync(veiculo.Id);
            if (veiculoExistente == null)
            {
                return NotFound();
            }
            veiculoExistente.Modelo = veiculo.Modelo;
            veiculoExistente.Placa = veiculo.Placa;
            veiculoExistente.ValorDiaria = veiculo.ValorDiaria;
            veiculoExistente.AgenciaId = veiculo.AgenciaId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeiculo(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
