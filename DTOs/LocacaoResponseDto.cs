using System.Text.Json.Serialization;
using DriveNow.API.Models;

namespace DriveNow.API.DTOs
{
    public class LocacaoResponseDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNome { get; set; }
        public int VeiculoId { get; set; }
        public string? VeiculoIdentificador { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public double ValorTotal { get; set; }
    }
}
