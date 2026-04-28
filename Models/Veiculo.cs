using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public string? FotoUrlVeiculo { get; set; } // Propriedade para armazenar a URL da foto do pet no banco de dados

        [NotMapped] // This property is not mapped to the database
        public IFormFile FotoUploadVeiculo { get; set; } // Aparece na View, mas não é armazenada no banco de dados
    }
}
