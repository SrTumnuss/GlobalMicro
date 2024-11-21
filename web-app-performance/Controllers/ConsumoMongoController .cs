using Microsoft.AspNetCore.Mvc;
using web_energy_domain;
using web_energy_repository;

namespace web_energy_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoMongoController : ControllerBase
    {
        private readonly IConsumoRepository _repository;

        public ConsumoMongoController(IConsumoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumos()
        {
            var consumos = await _repository.ListarConsumos();

            if (consumos == null || !consumos.Any())
                return NotFound(new { mensagem = "Nenhum dado de consumo encontrado." });

            return Ok(consumos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsumo(string id)
        {
            var consumo = await _repository.ObterConsumo(id);

            if (consumo == null)
                return NotFound(new { mensagem = "Consumo não encontrado." });

            return Ok(consumo);
        }

        [HttpPost]
        public async Task<IActionResult> PostConsumo([FromBody] Consumo consumo)
        {
            if (consumo == null)
                return BadRequest(new { mensagem = "Dados inválidos." });

            await _repository.RegistrarConsumo(consumo);

            return CreatedAtAction(nameof(GetConsumo), new { id = consumo.Id }, consumo);
        }
    }
}
