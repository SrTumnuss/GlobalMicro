using Microsoft.AspNetCore.Mvc;
using Moq;
using web_app_performance.Controllers;
using web_app_repository;
using web_energy_domain;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MongoDB.Bson;

namespace web_app_performance.Tests
{
    public class ConsumoControllerTests
    {
        private readonly Mock<IConsumoRepository> _repositoryMock;
        private readonly ConsumoController _controller;

        public ConsumoControllerTests()
        {
            _repositoryMock = new Mock<IConsumoRepository>();
            _controller = new ConsumoController(_repositoryMock.Object);
        }

        // Teste para o método ListarConsumos
        [Fact]
        public async Task ListarConsumos_ReturnsOkResult_WhenConsumosExist()
        {
            // Arrange
            var lista = new List<Consumo>();

            // Corrigido: O tipo do ID deve ser ObjectId, não string
            var id = new ObjectId("63a65f32e4b0d0f2304b3f7c"); // Exemplo de ID MongoDB válido

            lista.Add(new Consumo { Id = id, NomeDoEletronico = "TV", EnergiaConsumida = 150, PotenciaDoAparelho = 100, TempoDeUso = 3 });
            lista.Add(new Consumo { Id = new ObjectId("63a65f32e4b0d0f2304b3f7d"), NomeDoEletronico = "Refrigerador", EnergiaConsumida = 200, PotenciaDoAparelho = 150, TempoDeUso = 5 });

            // Configura o mock para retornar a lista de consumos
            _repositoryMock.Setup(repo => repo.ListarConsumos()).ReturnsAsync(lista);

            // Act
            var result = await _controller.ListarConsumos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Consumo>>(okResult.Value);
            Assert.Equal(2, ((List<Consumo>)returnValue).Count);
        }

        // Teste para o método ObterConsumo
        [Fact]
        public async Task ObterConsumo_ReturnsNotFound_WhenConsumoDoesNotExist()
        {
            // Arrange
            var id = "nonexistent-id";
            _repositoryMock.Setup(repo => repo.ObterConsumo(id)).ReturnsAsync((Consumo)null);

            // Act
            var result = await _controller.ObterConsumo(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ObterConsumo_ReturnsOkResult_WhenConsumoExists()
        {
            // Arrange
            var id = "63a65f32e4b0d0f2304b3f7c"; // Usando ObjectId correto
            var consumo = new Consumo { Id = new ObjectId(id), NomeDoEletronico = "TV", EnergiaConsumida = 150, PotenciaDoAparelho = 100, TempoDeUso = 3 };
            _repositoryMock.Setup(repo => repo.ObterConsumo(id)).ReturnsAsync(consumo);

            // Act
            var result = await _controller.ObterConsumo(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Consumo>(okResult.Value);
            Assert.Equal(id, returnValue.Id.ToString()); // Corrigido para comparar string
        }

        // Teste para o método SalvarConsumo
        [Fact]
        public async Task SalvarConsumo_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var consumo = new Consumo { NomeDoEletronico = "TV", EnergiaConsumida = 150, PotenciaDoAparelho = 100, TempoDeUso = 3 };
            _repositoryMock.Setup(repo => repo.SalvarConsumo(consumo)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.SalvarConsumo(consumo);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("ObterConsumo", createdAtActionResult.ActionName);
            Assert.Equal(consumo.Id, createdAtActionResult.RouteValues["id"]);
        }

        // Teste para o método AtualizarConsumo
        [Fact]
        public async Task AtualizarConsumo_ReturnsNoContent_WhenConsumoExists()
        {
            // Arrange
            var id = "63a65f32e4b0d0f2304b3f7c"; // Usando ObjectId correto
            var consumoExistente = new Consumo { Id = new ObjectId(id), NomeDoEletronico = "TV", EnergiaConsumida = 150, PotenciaDoAparelho = 100, TempoDeUso = 3 };
            var consumoAtualizado = new Consumo { Id = new ObjectId(id), NomeDoEletronico = "TV", EnergiaConsumida = 160, PotenciaDoAparelho = 120, TempoDeUso = 3 };
            _repositoryMock.Setup(repo => repo.ObterConsumo(id)).ReturnsAsync(consumoExistente);
            _repositoryMock.Setup(repo => repo.AtualizarConsumo(id, consumoAtualizado)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AtualizarConsumo(id, consumoAtualizado);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        // Teste para o método RemoverConsumo
        [Fact]
        public async Task RemoverConsumo_ReturnsNoContent_WhenConsumoExists()
        {
            // Arrange
            var id = "63a65f32e4b0d0f2304b3f7c"; // Usando ObjectId correto
            var consumoExistente = new Consumo { Id = new ObjectId(id), NomeDoEletronico = "TV", EnergiaConsumida = 150, PotenciaDoAparelho = 100, TempoDeUso = 3 };
            _repositoryMock.Setup(repo => repo.ObterConsumo(id)).ReturnsAsync(consumoExistente);
            _repositoryMock.Setup(repo => repo.RemoverConsumo(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RemoverConsumo(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
