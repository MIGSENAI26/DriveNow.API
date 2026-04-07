using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveNow.API.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        [Required] public string? Modelo { get; set; }
        [Required] public string? Placa { get; set; }
        [Required] public double ValorDiaria { get; set; }
        public int AgenciaId { get; set; }
        [JsonIgnore]
        public Agencia? Agencia { get; set; }
    }
}
