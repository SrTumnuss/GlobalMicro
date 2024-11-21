//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using web_energy_domain;
//using web_energy_performance.Controllers;
//using web_energy_repository;
//using Xunit;

//namespace Test
//{
//    public class ConsumoControllerTest
//    {
//        private readonly Mock<IConsumoRepository> _repositoryMock;
//        private readonly ConsumoController _controller;

//        public ConsumoControllerTest()
//        {
//            _repositoryMock = new Mock<IConsumoRepository>();
//            _controller = new ConsumoController(_repositoryMock.Object);
//        }

//        [Fact]
//        public async Task HealthCheck_ShouldReturnOk()
//        {
//            var result = _controller.HealthCheck();

//            Assert.IsType<OkObjectResult>(result);
//        }

//        [Fact]
//        public async Task GetConsumos_ShouldReturnOkWithList()
//        {
//            var consumos = new List<Consumo>
//            {
//                new Consumo { Id = 1, DataHora = DateTime.Now, EnergiaConsumida = 50.5 }
//            };

//            _repositoryMock.Setup(repo => repo.ListarConsumos()).ReturnsAsync(consumos);

//            var result = await _controller.GetConsumos();

//            var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(consumos, okResult.Value);
//        }
//    }
//}
