using System.ComponentModel.DataAnnotations;
using Consultorio.Api.Service;

namespace DriveNow.API.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required] public string? Nome { get; set; }
        [Required, DataType(DataType.EmailAddress)] public string Email { get; set; }

        [Cpf]  // Validação customizada para CPF
        public string Cpf { get; set; }
    }
}
