using Microsoft.AspNetCore.Mvc;
using web_app_repository;
using web_energy_domain;

namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumoController : ControllerBase
    {
        private readonly IConsumoRepository _repository;

        public ConsumoController(IConsumoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ListarConsumos()
        {
            var consumos = await _repository.ListarConsumos();
            if (consumos == null || !consumos.Any())
                return NotFound();

            return Ok(consumos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConsumo(string id)
        {
            var consumo = await _repository.ObterConsumo(id);
            if (consumo == null)
                return NotFound();

            return Ok(consumo);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarConsumo([FromBody] Consumo consumo)
        {
            await _repository.SalvarConsumo(consumo);
            return CreatedAtAction(nameof(ObterConsumo), new { id = consumo.Id }, consumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConsumo(string id, [FromBody] Consumo consumoAtualizado)
        {
            var consumoExistente = await _repository.ObterConsumo(id);
            if (consumoExistente == null)
                return NotFound();

            await _repository.AtualizarConsumo(id, consumoAtualizado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverConsumo(string id)
        {
            var consumoExistente = await _repository.ObterConsumo(id);
            if (consumoExistente == null)
                return NotFound();

            await _repository.RemoverConsumo(id);
            return NoContent();
        }
    }
}
