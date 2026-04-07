using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DriveNow.API.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public Cliente? Cliente { get; set; }
        public int VeiculoId { get; set; }
        [JsonIgnore]
        public Veiculo? Veiculo { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public double ValorTotal { get; set; }
    }
}
