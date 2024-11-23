using Moq;
using web_app_repository;
using web_energy_domain;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace web_app_repository.Tests
{
    public class ConsumoRepositoryTests
    {
        // Teste para o método ListarConsumos
        [Fact]
        public async Task ListarConsumos_ShouldReturnListOfConsumos()
        {
            // Arrange
            var consumos = new List<Consumo>
            {
                new Consumo
                {
                    Id = new ObjectId("63a65f32e4b0d0f2304b3f7c"),
                    NomeDoEletronico = "TV",
                    EnergiaConsumida = 150,
                    PotenciaDoAparelho = 100,
                    TempoDeUso = 3
                },
                new Consumo
                {
                    Id = new ObjectId("63a65f32e4b0d0f2304b3f7d"),
                    NomeDoEletronico = "Refrigerador",
                    EnergiaConsumida = 200,
                    PotenciaDoAparelho = 150,
                    TempoDeUso = 5
                }
            };

            var mongoSettingsMock = new Mock<MongoSettings>();
            var repositoryMock = new Mock<IConsumoRepository>();
            repositoryMock.Setup(repo => repo.ListarConsumos()).ReturnsAsync(consumos);
            var consumoRepository = repositoryMock.Object;

            // Act
            var result = await consumoRepository.ListarConsumos();

            // Assert
            Assert.Equal(consumos, result);
        }

        // Teste para o método ObterConsumo
        [Fact]
        public async Task ObterConsumo_ShouldReturnConsumo_WhenConsumoExists()
        {
            // Arrange
            var id = new ObjectId("63a65f32e4b0d0f2304b3f7c").ToString();
            var consumo = new Consumo
            {
                Id = new ObjectId(id),
                NomeDoEletronico = "TV",
                EnergiaConsumida = 150,
                PotenciaDoAparelho = 100,
                TempoDeUso = 3
            };

            var mongoSettingsMock = new Mock<MongoSettings>();
            var repositoryMock = new Mock<IConsumoRepository>();
            repositoryMock.Setup(repo => repo.ObterConsumo(id)).ReturnsAsync(consumo);
            var consumoRepository = repositoryMock.Object;

            // Act
            var result = await consumoRepository.ObterConsumo(id);

            // Assert
            Assert.Equal(consumo.Id, result.Id);
        }

        // Teste para o método SalvarConsumo
        [Fact]
        public async Task SalvarConsumo_ShouldCallSalvarConsumoOnce()
        {
            // Arrange
            var consumo = new Consumo
            {
                NomeDoEletronico = "TV",
                EnergiaConsumida = 150,
                PotenciaDoAparelho = 100,
                TempoDeUso = 3
            };

            var mongoSettingsMock = new Mock<MongoSettings>();
            var repositoryMock = new Mock<IConsumoRepository>();
            repositoryMock.Setup(repo => repo.SalvarConsumo(It.IsAny<Consumo>())).Returns(Task.CompletedTask);
            var consumoRepository = repositoryMock.Object;

            // Act
            await consumoRepository.SalvarConsumo(consumo);

            // Assert
            repositoryMock.Verify(repo => repo.SalvarConsumo(It.IsAny<Consumo>()), Times.Once);
        }

        // Teste para o método AtualizarConsumo
        [Fact]
        public async Task AtualizarConsumo_ShouldCallAtualizarConsumoOnce()
        {
            // Arrange
            var id = new ObjectId("63a65f32e4b0d0f2304b3f7c").ToString();
            var consumoAtualizado = new Consumo
            {
                Id = new ObjectId(id),
                NomeDoEletronico = "Refrigerador",
                EnergiaConsumida = 180,
                PotenciaDoAparelho = 120,
                TempoDeUso = 4
            };

            var mongoSettingsMock = new Mock<MongoSettings>();
            var repositoryMock = new Mock<IConsumoRepository>();
            repositoryMock.Setup(repo => repo.AtualizarConsumo(id, consumoAtualizado)).Returns(Task.CompletedTask);
            var consumoRepository = repositoryMock.Object;

            // Act
            await consumoRepository.AtualizarConsumo(id, consumoAtualizado);

            // Assert
            repositoryMock.Verify(repo => repo.AtualizarConsumo(It.IsAny<string>(), It.IsAny<Consumo>()), Times.Once);
        }

        // Teste para o método RemoverConsumo
        [Fact]
        public async Task RemoverConsumo_ShouldCallRemoverConsumoOnce()
        {
            // Arrange
            var id = new ObjectId("63a65f32e4b0d0f2304b3f7c").ToString();

            var mongoSettingsMock = new Mock<MongoSettings>();
            var repositoryMock = new Mock<IConsumoRepository>();
            repositoryMock.Setup(repo => repo.RemoverConsumo(It.IsAny<string>())).Returns(Task.CompletedTask);
            var consumoRepository = repositoryMock.Object;

            // Act
            await consumoRepository.RemoverConsumo(id);

            // Assert
            repositoryMock.Verify(repo => repo.RemoverConsumo(It.IsAny<string>()), Times.Once);
        }
    }
}
