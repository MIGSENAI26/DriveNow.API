
namespace DriveNow.API.DTOs
{
    public class VeiculoResponseDto
    {
        public int Id { get; set; }
        public string? Modelo { get; set; }
        public string? Placa { get; set; }
        public double ValorDiaria { get; set; }
        public int AgenciaId { get; set; }
        public string? AgenciaNome { get; set; }
    }
}
