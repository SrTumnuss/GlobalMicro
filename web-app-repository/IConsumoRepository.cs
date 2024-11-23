using web_energy_domain;

public interface IConsumoRepository
{
    Task<IEnumerable<Consumo>> ListarConsumos();
    Task<Consumo> ObterConsumo(string id);
    Task SalvarConsumo(Consumo consumo);
    Task AtualizarConsumo(string id, Consumo consumoAtualizado);
    Task RemoverConsumo(string id);
}
