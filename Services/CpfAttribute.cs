using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Consultorio.Api.Service
{
    public class CpfAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Verifica se o campo é nulo ou vazio
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O CPF é obrigatório.");
            }

            // Converte o valor para string
            string cpf = value.ToString()!;

            // Remove caracteres não numéricos (pontos e traços)
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            // Validação de tamanho (11 caracteres)
            if (cpf.Length != 11)
            {
                return new ValidationResult("O CPF deve possuir 11 dígitos.");
            }

            return ValidationResult.Success;
        }
    }
}