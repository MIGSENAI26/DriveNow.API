using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DriveNow.Api.Service;

namespace DriveNow.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required] public string? Nome { get; set; }
        [Required, DataType(DataType.EmailAddress)] public string Email { get; set; }

        [Cpf]  // Validação customizada para CPF
        public string Cpf { get; set; }

        public string? FotoUrlCliente { get; set; } // Propriedade para armazenar a URL da foto do pet no banco de dados

        [NotMapped] // This property is not mapped to the database
        public IFormFile FotoUploadCliente { get; set; } // Aparece na View, mas não é armazenada no banco de dados
    }
}
