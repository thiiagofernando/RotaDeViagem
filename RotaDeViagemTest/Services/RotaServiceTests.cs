using FluentAssertions;
using RotaDeViagemApi.DTOs;
using RotaDeViagemApi.Models;
using RotaDeViagemApi.Services;
using RotaDeViagemTest;

namespace RotaDeViagem.Tests.Services
{
    public class RotaServiceTests : TestBase
    {
        [Fact]
        public async Task GetAllRotasAsync_DeveRetornarTodasAsRotas()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(GetAllRotasAsync_DeveRetornarTodasAsRotas));
            var service = new RotaService(dbContext);

            var rotasIniciais = new[]
            {
                new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 },
                new Rota { Origem = "BRC", Destino = "SCL", Valor = 5 }
            };

            await dbContext.Rotas.AddRangeAsync(rotasIniciais);
            await dbContext.SaveChangesAsync();

            // Act
            var resultado = await service.GetAllRotasAsync();

            // Assert
            resultado.Should().HaveCount(2);
            resultado.Should().Contain(r => r.Origem == "GRU" && r.Destino == "BRC" && r.Valor == 10);
            resultado.Should().Contain(r => r.Origem == "BRC" && r.Destino == "SCL" && r.Valor == 5);
        }

        [Fact]
        public async Task GetRotaByIdAsync_DeveRetornarRotaExistente()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(GetRotaByIdAsync_DeveRetornarRotaExistente));
            var service = new RotaService(dbContext);

            var rotaEsperada = new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 };
            await dbContext.Rotas.AddAsync(rotaEsperada);
            await dbContext.SaveChangesAsync();

            // Act
            var resultado = await service.GetRotaByIdAsync(rotaEsperada.Id);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(rotaEsperada.Id);
            resultado.Origem.Should().Be(rotaEsperada.Origem);
            resultado.Destino.Should().Be(rotaEsperada.Destino);
            resultado.Valor.Should().Be(rotaEsperada.Valor);
        }

        [Fact]
        public async Task CreateRotaAsync_DeveCriarNovaRota()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(CreateRotaAsync_DeveCriarNovaRota));
            var service = new RotaService(dbContext);

            var rotaDto = new RotaRequestDto
            {
                Origem = "GRU",
                Destino = "BRC",
                Valor = 10
            };

            // Act
            var resultado = await service.CreateRotaAsync(rotaDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.Origem.Should().Be(rotaDto.Origem);
            resultado.Destino.Should().Be(rotaDto.Destino);
            resultado.Valor.Should().Be(rotaDto.Valor);

            // Verificar se foi realmente salvo no banco
            var rotaSalva = await dbContext.Rotas.FindAsync(resultado.Id);
            rotaSalva.Should().NotBeNull();
            rotaSalva.Origem.Should().Be(rotaDto.Origem);
        }

        [Fact]
        public async Task UpdateRotaAsync_DeveAtualizarRotaExistente()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(UpdateRotaAsync_DeveAtualizarRotaExistente));
            var service = new RotaService(dbContext);

            var rotaExistente = new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 };
            await dbContext.Rotas.AddAsync(rotaExistente);
            await dbContext.SaveChangesAsync();

            var rotaDto = new RotaRequestDto
            {
                Origem = "GRU",
                Destino = "SCL",
                Valor = 20
            };

            // Act
            var resultado = await service.UpdateRotaAsync(rotaExistente.Id, rotaDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(rotaExistente.Id);
            resultado.Destino.Should().Be(rotaDto.Destino);
            resultado.Valor.Should().Be(rotaDto.Valor);

            // Verificar se foi realmente atualizado no banco
            var rotaAtualizada = await dbContext.Rotas.FindAsync(rotaExistente.Id);
            rotaAtualizada.Should().NotBeNull();
            rotaAtualizada.Destino.Should().Be(rotaDto.Destino);
        }

        [Fact]
        public async Task DeleteRotaAsync_DeveDeletarRotaExistente()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(DeleteRotaAsync_DeveDeletarRotaExistente));
            var service = new RotaService(dbContext);

            var rota = new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 };
            await dbContext.Rotas.AddAsync(rota);
            await dbContext.SaveChangesAsync();

            // Act
            var resultado = await service.DeleteRotaAsync(rota.Id);

            // Assert
            resultado.Should().BeTrue();

            // Verificar se foi realmente deletado do banco
            var rotaDeletada = await dbContext.Rotas.FindAsync(rota.Id);
            rotaDeletada.Should().BeNull();
        }

        [Fact]
        public async Task ConsultarMelhorRotaAsync_DeveEncontrarMelhorRota()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(ConsultarMelhorRotaAsync_DeveEncontrarMelhorRota));
            var service = new RotaService(dbContext);

            var rotas = new[]
            {
                new Rota { Origem = "GRU", Destino = "BRC", Valor = 10 },
                new Rota { Origem = "BRC", Destino = "SCL", Valor = 5 },
                new Rota { Origem = "GRU", Destino = "CDG", Valor = 75 },
                new Rota { Origem = "GRU", Destino = "SCL", Valor = 20 },
                new Rota { Origem = "GRU", Destino = "ORL", Valor = 56 },
                new Rota { Origem = "ORL", Destino = "CDG", Valor = 5 },
                new Rota { Origem = "SCL", Destino = "ORL", Valor = 20 }
            };

            await dbContext.Rotas.AddRangeAsync(rotas);
            await dbContext.SaveChangesAsync();

            var consultaDto = new ConsultaRotaRequestDto
            {
                Origem = "GRU",
                Destino = "CDG"
            };

            // Act
            var resultado = await service.ConsultarMelhorRotaAsync(consultaDto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Trajeto.Should().ContainInOrder(new[] { "GRU", "BRC", "SCL", "ORL", "CDG" });
            resultado.CustoTotal.Should().Be(40);
        }

        [Fact]
        public async Task ConsultarMelhorRotaAsync_DeveRetornarNullQuandoRotaNaoExiste()
        {
            // Arrange
            var dbContext = GetDbContext(nameof(ConsultarMelhorRotaAsync_DeveRetornarNullQuandoRotaNaoExiste));
            var service = new RotaService(dbContext);

            var consultaDto = new ConsultaRotaRequestDto
            {
                Origem = "XXX",
                Destino = "YYY"
            };

            // Act
            var resultado = await service.ConsultarMelhorRotaAsync(consultaDto);

            // Assert
            resultado.Should().BeNull();
        }
    }
}
