using FluentAssertions;
using RotaDeViagemApi.DTOs;
using System.ComponentModel.DataAnnotations;

namespace RotaDeViagem.Tests.DTOs
{
    public class RotaRequestDtoTests
    {
        [Theory]
        [InlineData("", "BRC", 10, false)]
        [InlineData("GRU", "", 10, false)]
        [InlineData("GRUU", "BRC", 10, false)]
        [InlineData("GRU", "BRCC", 10, false)]
        [InlineData("GRU", "BRC", 0, false)]
        [InlineData("GRU", "BRC", -1, false)]
        [InlineData("GRU", "BRC", 10, true)]
        public void RotaRequestDto_Validacao(string origem, string destino, decimal valor, bool deveSerValido)
        {
            // Arrange
            var dto = new RotaRequestDto
            {
                Origem = origem,
                Destino = destino,
                Valor = valor
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            // Assert
            isValid.Should().Be(deveSerValido);
            if (!deveSerValido)
            {
                results.Should().NotBeEmpty();
            }
        }
    }
}
